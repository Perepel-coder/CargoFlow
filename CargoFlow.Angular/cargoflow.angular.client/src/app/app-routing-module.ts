import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerAuthComponent } from './customer/customer-auth-component/customer-auth-component';
import { OrderHomeComponent } from './order/order-home-component/order-home-component';

const routes: Routes = [
  { path: '', component: CustomerAuthComponent },
  { path: 'order/home', component: OrderHomeComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
