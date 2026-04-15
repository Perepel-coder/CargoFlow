import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { API_ENDPOINTS } from '../../constants/api-endpoints';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-order-home-component',
  standalone: false,
  templateUrl: './order-home-component.html',
  styleUrl: './order-home-component.scss',
})
export class OrderHomeComponent implements OnInit {
  private _customerId: number | null = null;

  public orderId: number | null = null;
  public sender_city: string | null = null;
  public sender_address: string | null = null;
  public recipient_city: string | null = null;
  public recipient_address: string | null = null;
  public weight: number | null = null;
  public date_cargo: string | null = null;

  public showModal: boolean = false;
  public isFormReadonly: boolean = true;
  public orders: any[] = [];
  public minDate: string = '';

  constructor(private cdr: ChangeDetectorRef) {
    const today = new Date();
    this.minDate = today.toISOString().split('T')[0];
  }

  public async ngOnInit(): Promise<void> {
    await this.loadOrders();
  }

  public async loadOrders() {
    this._customerId = this._customerId ?? Number(localStorage.getItem('userId'));
    const params = new URLSearchParams({ customerId: this._customerId.toString() });

    const response = await fetch(`${API_ENDPOINTS.ORDER.GET_BY_CUSTOMER}?${params.toString()}`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    });

    if (response.ok) {
      const data = await response.json();
      this.orders = data.orders || [];
    }

    this.cdr.detectChanges();
  }

  public openCreateOrderForm() {
    this.isFormReadonly = false;
    this.clearForm();
    this.showModal = true;
  }

  public async openOrderDetails(order: any) {
    const response = await fetch(`${API_ENDPOINTS.ORDER.GET_BY_ID}?id=${order.id}`, {
      method: 'GET',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
    });

    if (response.ok) {
      const data = await response.json();
      this.orderId = data.id;
      this.sender_city = data.senderCity;
      this.sender_address = data.senderAddress;
      this.recipient_city = data.recipientCity;
      this.recipient_address = data.recipientAddress;
      this.weight = data.weight;
      this.date_cargo = formatDate(data.dateCargo, 'yyyy-MM-dd', 'en-US');
    }

    this.isFormReadonly = true;
    this.showModal = true;
  }

  public async creatNewOrder() {
    if (
      !this.sender_city?.trim() ||
      !this.sender_address?.trim() ||
      !this.recipient_city?.trim() ||
      !this.recipient_address?.trim() ||
      !this.weight ||
      !this.date_cargo
    ) {
      alert('Пожалуйста, заполните все обязательные поля');
      return;
    }

    const orderData = {
      senderCity: this.sender_city.trim(),
      senderAddress: this.sender_address.trim(),
      recipientCity: this.recipient_city.trim(),
      recipientAddress: this.recipient_address.trim(),
      weight: Number(this.weight),
      dateCargo: this.date_cargo,
      customerId: this._customerId,
    };

    const response = await fetch(API_ENDPOINTS.ORDER.CREATE, {
      method: 'POST',
      headers: { Accept: 'application/json', 'Content-Type': 'application/json' },
      body: JSON.stringify(orderData),
    });

    if (response.ok) {
      const newOrder = await response.json();
      this.orders = [...this.orders, newOrder]; // Создаем новый массив
      this.closeModal();
      this.cdr.detectChanges();
      alert('Заказ успешно создан!');
      console.log('Order created successfully:', newOrder);
    } else {
      alert('Ошибка при создании заказа');
    }
  }

  private clearForm() {
    this.orderId = null;
    this.sender_city = null;
    this.sender_address = null;
    this.recipient_city = null;
    this.recipient_address = null;
    this.weight = null;
    this.date_cargo = null;
  }

  public closeModal() {
    this.showModal = false;
    this.clearForm();
    this.isFormReadonly = true;
  }

  public formatDate(date: string): string {
    if (!date) return '';
    return new Date(date).toLocaleString('ru');
  }
}
