import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductCardComponent } from './product-card/product-card.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgxImageZoomModule } from 'ngx-image-zoom';
import { ShopRoutingModule } from './shop-routing.module';



@NgModule({
  declarations: [
    ShopComponent,
    ProductCardComponent,
    ProductDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    FormsModule,
    NgxImageZoomModule,
    ShopRoutingModule
  ],
  exports:[

  ]
})
export class ShopModule { }
