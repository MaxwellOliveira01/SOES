import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { ElectionFullModel, OptionModel } from '../api/models';
import { KeyGenerationService } from '../key-generation.service';
import { SubmitVoteService } from '../submit-vote.service';

@Component({
  selector: 'app-vote-dialog',
  templateUrl: './vote-dialog.component.html',
  styleUrl: './vote-dialog.component.scss'
})
export class VoteDialogComponent implements OnInit {

  loading: boolean = false;
  step: 'select' | 'confirm' = 'select';

  election: ElectionFullModel | undefined;
  session: string | undefined;

  selectedOption: OptionModel | undefined;

  hasGeneratedKeys: boolean = false;

  privateKeyPem: string | undefined;
  publicKeyPem: string | undefined;

  constructor(
    public dialogRef: MatDialogRef<VoteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private readonly keyGenerationService: KeyGenerationService,
    private readonly submitVoteService: SubmitVoteService
  ) { }

  ngOnInit(): void {
    this.election = this.data.election;
    this.session = this.data.session;
  }

  select(option: OptionModel) {
    if (this.selectedOption?.id === option.id) {
      this.selectedOption = undefined;
    } else {
      this.selectedOption = option;
    }
  }

  goToConfirmation() {
    if (!this.selectedOption) {
      return;
    }
    this.step = 'confirm';
  }

  generateKeys() {
    this.loading = true;
    this.keyGenerationService.generateKeyPair(2048).then(({ privateKeyPem, publicKeyPem }) => {
      this.publicKeyPem = publicKeyPem;
      this.privateKeyPem = privateKeyPem;
      this.hasGeneratedKeys = true;
      this.downloadKey();
    }).catch(err => {
      console.error('Error generating keys:', err);
    }).finally(() => {
      this.loading = false;
    });
  }

  downloadKey() {
    if (!this.hasGeneratedKeys) {
      return;
    }

    const blob = new Blob([this.privateKeyPem as string], { type: 'text/plain' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'private_key.pem';
    a.click();
    window.URL.revokeObjectURL(url);
  }

  async submitVote() {
    if (!this.hasGeneratedKeys) {
      return;
    }

    let signature = this.keyGenerationService.signData(this.privateKeyPem as string, this.selectedOption?.id || '');

    this.loading = true;

    try {

      await this.submitVoteService.submitVote(
        this.session as string,
        this.election?.id as string,
        this.selectedOption?.id as string,
        this.publicKeyPem as string,
        signature
      );

      this.closeDialog();

    } catch (error) {
      console.error('Error submitting vote:', error);
    } finally {
      this.loading = false;
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }

}
