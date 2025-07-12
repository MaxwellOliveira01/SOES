import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ElectionVoterModel } from '../api/models';

@Component({
  selector: 'app-list-elections',
  templateUrl: './list-elections.component.html',
  styleUrl: './list-elections.component.scss'
})
export class ListElectionsComponent {

  @Input() elections: ElectionVoterModel[] = [];
  @Output() selectElection = new EventEmitter<ElectionVoterModel>();

  constructor() { }

  onSelectElection(election: ElectionVoterModel) {
    this.selectElection.emit(election);
  }

}
