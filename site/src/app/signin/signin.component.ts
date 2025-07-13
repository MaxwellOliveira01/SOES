import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { SigninService } from '../signin.service';
import { NgModel } from '@angular/forms';
import { Router } from '@angular/router';
import { ElectionVoterModel } from '../api/models';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent {

  @Output() afterSignin = new EventEmitter<{ voterName: string, session: string, elections: ElectionVoterModel[] }>();

  step: 'email' | 'otp' = 'email';

  voterName: string | undefined = undefined;

  email: string = '';
  emailErrorMessage: string | undefined = '';
  @ViewChild('emailModel') emailModel!: NgModel;

  otp: number | undefined = undefined;
  otpErrorMessage: string | undefined = '';
  @ViewChild('otpModel') otpModel!: NgModel;

  session: string | undefined = undefined;

  loading: boolean = false;

  constructor(
    private readonly signinService: SigninService,
    private readonly router: Router,
  ) { }

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
      let res = await this.signinService.authenticate(this.otp?.toString() || '', this.session || '');

      if(res.success) {
        this.session = res.session;

        // We're about to be destroyed
        this.afterSignin.emit({
          voterName: this.voterName || '',
          session: this.session || '',
          elections: res.elections || []
        });

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

  async sendOtp() {
    this.loading = true;

    try {
      await this.signinService.sendOtp(this.session || '');
    } catch (error) {
      console.error('Error sending OTP:', error);
    } finally {
      this.loading = false;
    }
  }

  redirectToVoterHome() {
    this.router.navigate(['/voter-home']);
  }


}
