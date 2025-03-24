import { Component, Input } from '@angular/core';
import { IProduct } from '../../shared/Models/IProduct';

@Component({
  selector: 'app-product-card',
  standalone: false,
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss'
})
export class ProductCardComponent {
@Input() product: IProduct;
item: any;
}
