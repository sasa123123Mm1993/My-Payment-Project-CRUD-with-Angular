import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { PaymentDetail } from './payment-detail.model';
import { environment } from 'src/environments/environment';
import { NgForm } from "@angular/forms";


@Injectable({
  providedIn: 'root'
})
export class PaymentDatailService {
  
  url: string = environment.apiBaseUrl + '/PaymentDetail'
  list: PaymentDetail[] = [];
  formData: PaymentDetail = new PaymentDetail()
  formSubmitted: boolean = false;
  constructor(private http: HttpClient) { }

  refreshList() {
    this.http.get(this.url)
      .subscribe({
        next: res => {
          this.list = res as PaymentDetail[]
        },
        error: err => { console.log(err) }
      })
  }


  postPaymentDetail() {
    debugger;
    return this.http.post(this.url, this.formData)
  }

  putPaymentDetail() {
    return this.http.put(this.url + '/' + this.formData.paymentDetailId, this.formData)
  }


  deletePaymentDetail(id: number) {
    return this.http.delete(this.url + '/' + id)
  }


  resetForm(form: NgForm) {
    form.form.reset()
    this.formData = new PaymentDetail()
    this.formSubmitted = false
  }




}
