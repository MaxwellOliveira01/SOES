import { Component, Inject, OnInit } from '@angular/core';
import { VoteDialogComponent } from '../vote-dialog/vote-dialog.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ElectionService } from '../election.service';
import { ElectionResult } from '../api/models';

@Component({
  selector: 'app-election-result-dialog',
  templateUrl: './election-result-dialog.component.html',
  styleUrl: './election-result-dialog.component.scss'
})
export class ElectionResultDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<VoteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private readonly electionService: ElectionService,
  ) { }

  results!: ElectionResult;
  loading: boolean = true;

  ngOnInit(): void {
    this.fetchElectionResults();
  }

  async fetchElectionResults() {
    this.loading = true;
    try {
      this.results = await this.electionService.getResult(this.data.electionId);
    } finally {
      this.loading = false;
    }
  }

}
