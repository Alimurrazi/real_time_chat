import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

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
    )
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
export class AppRoutingModule { }
