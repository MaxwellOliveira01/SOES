import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { ElectionFullModel, VoterElectionModelRequest } from './api/models';

const apiRoute: string = 'http://localhost:5173/api/elections';

@Injectable({
  providedIn: 'root'
})
export class ElectionService {

  constructor(private readonly http: HttpClient) { }

  getElectionById(electionId: string, session: string) {
    return firstValueFrom(
      this.http.post<ElectionFullModel>(`${apiRoute}`, <VoterElectionModelRequest>{ electionId, session } )
   );
  }

}
