import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TokenProviderService } from './services/token-provider.service';


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    TokenProviderService
  ]
})
export class SharedModule { }
