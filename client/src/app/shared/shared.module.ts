import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TokenProviderService } from './services/token-provider.service';
import { SnackbarService } from './services/snackbar.service';
import {MatSnackBarModule} from '@angular/material/snack-bar';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatSnackBarModule
  ],
  providers: [
    TokenProviderService,
    SnackbarService
  ]
})
export class SharedModule { }
