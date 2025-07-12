import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VoterHomeComponent } from './voter-home/voter-home.component';

const routes: Routes = [
  { path: 'voter-home', component: VoterHomeComponent },
  { path: '', redirectTo: '/voter-home', pathMatch: 'full' },
  { path: '**', redirectTo: '/voter-home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
