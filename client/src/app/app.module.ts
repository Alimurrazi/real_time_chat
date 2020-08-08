import { AuthGurdService } from './gurds/auth-gurd.service';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { AuthInterceptor } from './interceptor/auth-interceptor';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS, /* other http imports */ } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AuthModule } from './auth/auth.module';
import { JwtModule } from '@auth0/angular-jwt';

export function TokenGetter() {
  return localStorage.getItem('real_time_chat_token');
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AuthModule,
    BrowserAnimationsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: TokenGetter,
        allowedDomains : ['localhost: 44381']
      }
    })
  ],
  providers: [AuthGurdService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule {
 }
