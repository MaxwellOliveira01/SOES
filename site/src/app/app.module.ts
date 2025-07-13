import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SigninComponent } from './signin/signin.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VoterHomeComponent } from './voter-home/voter-home.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { NgxLoadingModule } from 'ngx-loading';
import { HttpClientModule } from '@angular/common/http';
import { ListElectionsComponent } from './list-elections/list-elections.component';
import { ElectionDetailsComponent } from './election-details/election-details.component';
import { VoteDialogComponent } from './vote-dialog/vote-dialog.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ElectionResultDialogComponent } from './election-result-dialog/election-result-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
    VoterHomeComponent,
    ListElectionsComponent,
    ElectionDetailsComponent,
    VoteDialogComponent,
    ElectionResultDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatSlideToggleModule,
    NgxLoadingModule.forRoot({}),
    HttpClientModule,
    MatDialogModule,
    MatCheckboxModule,
    MatSnackBarModule,
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
