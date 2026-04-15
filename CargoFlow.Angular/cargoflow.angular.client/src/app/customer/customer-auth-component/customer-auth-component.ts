import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AUTH_MESSAGE } from '../../constants/messages';
import { API_ENDPOINTS } from '../../constants/api-endpoints';

@Component({
  selector: 'app-customer-auth-component',
  standalone: false,
  templateUrl: './customer-auth-component.html',
  styleUrl: './customer-auth-component.scss',
})
export class CustomerAuthComponent {
  constructor(
    private router: Router
  ) { }

  public login: string = '';
  public password: string = '';

  public isLoading: boolean = false;

  public authMessage = AUTH_MESSAGE;

  private async createRequest(url: string, errorMsg: string): Promise<void> {
    this.isLoading = true;
    let response = await fetch(url, {
      method: "POST",
      headers: { "Accept": "application/json", "Content-Type": "application/json" },
      body: JSON.stringify({
        login: this.login,
        password: this.password
      })
    })

    this.isLoading = false;

    if (response.ok == true) {
      const answer = await response.json();
      if (answer.id != -1) {
        localStorage.clear();
        localStorage.setItem('userId', answer.id);
        this.router.navigate([API_ENDPOINTS.ORDER.HOME]);
      }
      else {
        alert(errorMsg);
      }
    }
  }

  public create() {
    this.createRequest(API_ENDPOINTS.AUTH.CREATE, AUTH_MESSAGE.LOGIN_EXISTS);
  }

  public check = () => this.createRequest(API_ENDPOINTS.AUTH.CHECK, AUTH_MESSAGE.INVALID_CREDENTIALS);
}
