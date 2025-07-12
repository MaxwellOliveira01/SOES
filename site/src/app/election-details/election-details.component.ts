import { Component, Input, OnInit } from '@angular/core';
import { ElectionFullModel } from '../api/models';

@Component({
  selector: 'app-election-details',
  templateUrl: './election-details.component.html',
  styleUrl: './election-details.component.scss'
})
export class ElectionDetailsComponent implements OnInit {

  @Input() election: ElectionFullModel | undefined;

  constructor() {

  }

  ngOnInit(): void {
    if (this.election) {
      console.log('Election Details:', this.election);
    } else {
      console.warn('No election data provided');
    }
  }

}
