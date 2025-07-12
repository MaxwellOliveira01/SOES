import { Component } from '@angular/core';
import { SigninService } from '../services/signin.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent {
  
  step: 'email' | 'otp' = 'email';
  email: string = '';

  loading: boolean = false;

  constructor(private readonly signinService: SigninService) {

  }

  async identify() {
    this.loading = true;
    console.log(this.email);
  }




}
