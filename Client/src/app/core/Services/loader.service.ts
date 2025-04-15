import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class LoaderService {
  constructor(private spinnerService: NgxSpinnerService) {}
requestCount:number=0;
  loading() {
    this.requestCount++
    this.spinnerService.show();
  }

  hideLoader(){
    this.requestCount--
    if(this.requestCount<= 0 ){
      this.requestCount=0;
      this.spinnerService.hide()
    }
  }
}
