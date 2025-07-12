import { Component, ViewChild } from '@angular/core';
import { SigninService } from '../signin.service';
import { NgModel } from '@angular/forms';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent {

  step: 'email' | 'otp' = 'email';

  voterName: string | undefined = undefined;
  session: string = "";

  email: string = '';
  emailErrorMessage: string | undefined = '';
  @ViewChild('emailModel') emailModel!: NgModel;

  otp: number = 0;
  otpErrorMessage: string | undefined = '';
  @ViewChild('otpModel') otpModel!: NgModel;

  loading: boolean = false;

  constructor(private readonly signinService: SigninService) {

  }

  async identify(sendOtp: boolean) {
    this.loading = true;

    try {
      let res = await this.signinService.identify(this.email, sendOtp);

      if(res.success) {
        this.step = 'otp';
        this.voterName = res.name;
        this.session = res.session;
      } else {
        console.error('Identification failed:', res.errorMessage);
        this.emailModel.control.setErrors({ invalid: true });
        this.emailErrorMessage = res.errorMessage || 'Erro ao identificar usuário';
      }

    } catch (error) {
      console.error('Error identifying user:', error);
    } finally {
      this.loading = false;
    }
  }

  async authenticate() {
    this.loading = true;

    try {
      let res = await this.signinService.authenticate(this.otp.toString(), this.session);

      if(res.success) {

      } else {
        console.error('Authentication failed:', res.errorMessage);
        this.otpModel.control.setErrors({ invalid: true });
        this.otpErrorMessage = res.errorMessage || 'Erro ao autenticar usuário';
      }

    } catch (error) {
      console.error('Error authenticating user:', error);
    } finally {
      this.loading = false;
    }
  }


}
