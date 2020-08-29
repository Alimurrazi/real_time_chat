import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AuthGurdService } from './gurds/auth-gurd.service';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
    import('./auth/auth.module').then(
      (m) => m.AuthModule
    )
  },
  {
    path: 'dashboard',
    loadChildren: () =>
    import('./dashboard/dashboard.module').then(
      (m) => m.DashboardModule
    ),
    canActivate: [AuthGurdService]
  }
];
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
 }
