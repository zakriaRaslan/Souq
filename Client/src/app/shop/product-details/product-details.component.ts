import { Component, OnInit } from '@angular/core';
import { IProduct } from '../../shared/Models/IProduct';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  standalone: false,
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  selectedQuantity:number
  product:IProduct
  constructor(private productService:ShopService , private route:ActivatedRoute) { }
  ngOnInit(): void {
    this.getProductDetails(parseInt(this.route.snapshot.paramMap.get('id')))
  }


  // Get Product Details
  getProductDetails(id:number){
    if(id!==0 || id!==null){
    this.productService.getProductById(id).subscribe({
      next: (product:IProduct) => {
        this.product = product
      },
      error: error => {
        console.log(error)
      }
    })
  }
}

setMainImage(imageUrl: string) {
  this.product.images = [{
    imageName: imageUrl,
    productId: this.product.id
  }, ...this.product.images.filter(img => img.imageName !== imageUrl)];
}


// Component methods for quantity adjustment
increaseQuantity() {
  this.selectedQuantity++;
}

decreaseQuantity() {
  if (this.selectedQuantity > 1) {
    this.selectedQuantity--;
  }
}


}

