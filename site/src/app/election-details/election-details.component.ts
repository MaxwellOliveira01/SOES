import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ElectionFullModel } from '../api/models';
import { ElectionService } from '../election.service';
import { MatDialog } from '@angular/material/dialog';
import { VoteDialogComponent } from '../vote-dialog/vote-dialog.component';

@Component({
  selector: 'app-election-details',
  templateUrl: './election-details.component.html',
  styleUrl: './election-details.component.scss'
})
export class ElectionDetailsComponent implements OnInit {

  @Input() electionId: string = '';
  @Input() session: string | undefined = '';

  @Output() onBack: EventEmitter<null> = new EventEmitter<null>();

  loading: boolean = true;

  election: ElectionFullModel | undefined;

  constructor(
    private readonly electionService: ElectionService,
    private readonly dialog: MatDialog
  ) {

  }

  async ngOnInit(): Promise<void> {
    await this.getElection(this.electionId);
  }

  async getElection(electionId: string) {
    this.loading = true;
    try {
      this.election = await this.electionService.getElectionById(electionId, this.session || '');
      this.loading = false;
    } catch (error) {
      console.error('Error fetching election:', error);
      this.loading = false;
    }
  }

  back() {
    this.onBack.emit();
  }

  vote() {
    this.dialog.open(VoteDialogComponent, {
      width: '400px',
      data: { election: this.election, session: this.session },
      disableClose: true,
      panelClass: 'custom-dialog-container'
    }).afterClosed().subscribe(r => {
      this.getElection(this.electionId);
    });
  }

}
