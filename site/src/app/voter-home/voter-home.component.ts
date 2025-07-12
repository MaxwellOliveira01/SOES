import { Component } from '@angular/core';
import { ElectionVoterModel } from '../api/models';
import { VoterData } from '../classes/classes';
import { ElectionService } from '../election.service';

@Component({
  selector: 'app-voter-home',
  templateUrl: './voter-home.component.html',
  styleUrl: './voter-home.component.scss'
})
export class VoterHomeComponent {

  step: 'signin' | 'list-elections' | 'election-details' = 'signin';

  voterName: string | undefined = undefined;
  session: string | undefined = undefined;
  elections: ElectionVoterModel[] = [];

  selectedElectionId: string = '';

  loading: boolean = false;

  constructor(private readonly electionService: ElectionService) { }

  afterSignin(data: VoterData) {
    this.voterName = data.voterName;
    this.session = data.session;
    this.elections = data.elections;
    this.step = 'list-elections';
  }

  afterSelectElection(election: ElectionVoterModel) {
    this.selectedElectionId = election.id;
    this.step = 'election-details';
  }

  onBackFromDetails() {
    this.selectedElectionId = '';
    this.step = 'list-elections';
  }

}
