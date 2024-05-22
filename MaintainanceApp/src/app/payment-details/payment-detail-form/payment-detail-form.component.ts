import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PaymentDatailService } from 'src/app/shared/payment-datail.service';
import { PaymentDetail } from 'src/app/shared/payment-detail.model';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-payment-detail-form',
  templateUrl: './payment-detail-form.component.html',
  //styleUrls: ['./payment-detail-form.component.css']
})
export class PaymentDetailFormComponent {

   constructor(public service: PaymentDatailService, private toastr: ToastrService) {}

 
  
  onSubmit(form: NgForm) {
    debugger;
    this.service.formSubmitted = true
    if (form.valid) {
      if (this.service.formData.paymentDetailId == 0)
        this.insertRecord(form)
      else
        this.updateRecord(form)
    }
  }

  insertRecord(form: NgForm) {
    debugger;
    this.service.postPaymentDetail()
      .subscribe({
        next: res => {
          this.service.list = res as PaymentDetail[]
          this.service.resetForm(form)
          this.toastr.success('Inserted successfully', 'Payment Detail Register')
        },
        error: err => { console.log(err) }
      })
  }

    
  updateRecord(form: NgForm) {
    this.service.putPaymentDetail()
      .subscribe({
        next: res => {
          this.service.list = res as PaymentDetail[]
          this.service.resetForm(form)
          this.toastr.info('Updated successfully', 'Payment Detail Register')
        },
        error: err => { console.log(err) }
      })
   }


}
