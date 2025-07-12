import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SubmitVoteRequest } from './api/models';
import { firstValueFrom } from 'rxjs';

const apiRoute: string = 'http://localhost:5173/api/submit-vote';

@Injectable({
  providedIn: 'root'
})
export class SubmitVoteService {

  constructor(private readonly httpClient: HttpClient) { }

  async submitVote(session: string, electionId: string, optionId: string, publicKeyPem: string, signature: string) {

    let data: SubmitVoteRequest = {
      session: session,
      electionId: electionId,
      optionId: optionId,
      publicKeyPem: publicKeyPem,
      signature: signature
    };

    return firstValueFrom(
      this.httpClient.post(`${apiRoute}`, data)
    );
  }

}
