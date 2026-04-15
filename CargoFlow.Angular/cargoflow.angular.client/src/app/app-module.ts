import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { CustomerAuthComponent } from './customer/customer-auth-component/customer-auth-component';
import { OrderHomeComponent } from './order/order-home-component/order-home-component';

@NgModule({
  declarations: [App, CustomerAuthComponent, CustomerAuthComponent, OrderHomeComponent],
  imports: [BrowserModule, FormsModule, AppRoutingModule],
  providers: [provideBrowserGlobalErrorListeners(), provideHttpClient()],
  bootstrap: [App],
})
export class AppModule {}
