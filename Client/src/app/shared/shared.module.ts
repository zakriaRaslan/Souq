import { PaginationComponent } from 'ngx-bootstrap/pagination';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationsComponent } from './Components/paginations/paginations.component';



@NgModule({
  declarations: [
    PaginationsComponent
  ],
  imports: [
    CommonModule,
    PaginationComponent
  ],exports: [
    PaginationComponent,
    PaginationsComponent
  ]
})
export class SharedModule { }
