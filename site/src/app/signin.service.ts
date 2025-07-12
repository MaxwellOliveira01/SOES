import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { AuthenticationRequest, AuthenticationResponse, IdentificationRequest, IdentificationResponse } from './api/models';

const apiRoute: string = 'http://localhost:5173/api';

@Injectable({
  providedIn: 'root'
})
export class SigninService {

  constructor(private readonly httpClient: HttpClient) { }

  async identify(email: string, sendOtp: boolean) {
    return firstValueFrom(
      this.httpClient.post<IdentificationResponse>(`${apiRoute}/identification`, <IdentificationRequest>{ email, sendOtp })
    );
  }

  async authenticate(otp: string, session: string) {
    return firstValueFrom(
      this.httpClient.post<AuthenticationResponse>(`${apiRoute}/authentication`, <AuthenticationRequest>{ otp, session })
    );
  }

}
