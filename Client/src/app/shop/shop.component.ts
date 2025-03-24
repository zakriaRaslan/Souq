import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/Models/IProduct';
import { IPagination } from '../shared/Models/IPagination';
import { ICategory } from '../shared/Models/ICategory';

@Component({
  selector: 'app-shop',
  standalone: false,
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  categories: ICategory[];
  categoryId: number;
  sortOption:string;
  SortingOptions = [
    {name:'Price' , value:'Name'},
    {name:'Price:Min to Max' , value:'priceAse'},
    {name:'Price:Max to Min' , value:'priceDesc'},
  ]
  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
  }
  getProducts() {
    this.shopService.getProducts(this.categoryId,this.sortOption).subscribe({
      next: (value: IPagination) => {
        this.products = value.data;
      },
    });
  }
  getCategories() {
    this.shopService.getCategories().subscribe({
      next: (value: ICategory[]) => {
        this.categories = value;
      },
    });
  }

  //filter by category
  onSelectCategory(categoryId: number) {
    this.categoryId = categoryId;
    this.getProducts();
  }

  onSortChange(sort: Event) {
    this.sortOption = (sort.target as HTMLSelectElement).value;
    this.getProducts();
  }

}
