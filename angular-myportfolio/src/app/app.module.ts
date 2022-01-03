import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { SocialLoginModule, SocialAuthServiceConfig } from 'angularx-social-login';
import { GoogleLoginProvider, FacebookLoginProvider } from 'angularx-social-login';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { StockModule } from './stock/stock.module';
import { HomeModule } from './home/home.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    HomeModule,
    SocialLoginModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '212991792393-ilvmgoq66trrfrb6mr0mgdiktgh0ha7o.apps.googleusercontent.com'
            )
          },
        ],
      } as SocialAuthServiceConfig
    }

  ],  bootstrap: [AppComponent]
})
export class AppModule { }
