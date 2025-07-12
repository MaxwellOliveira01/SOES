import { Component } from '@angular/core';
import { ElectionFullModel, ElectionVoterModel } from '../api/models';
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

  selectedElection: ElectionFullModel | undefined;

  loading: boolean = false;

  constructor(private readonly electionService: ElectionService) { }

  afterSignin(data: VoterData) {
    this.voterName = data.voterName;
    this.session = data.session;
    this.elections = data.elections;
    this.step = 'list-elections';
    console.log('Voter Home after signin:', this.voterName, this.session, this.elections);
  }

  async afterSelectElection(election: ElectionVoterModel) {
    console.log('Selected election:', election);
    await this.getElection(election.id);
    this.step = 'election-details';
  }

  async getElection(electionId: string) {
    this.loading = true;
    try {
      this.selectedElection = await this.electionService.getElectionById(electionId, this.session || '');
      this.loading = false;
    } catch (error) {
      console.error('Error fetching election:', error);
      this.loading = false;
    }
  }

}
