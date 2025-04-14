import { GetProductsParams } from './../shared/Models/GetProductsParams';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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
  ProductsParams:GetProductsParams = new GetProductsParams();
  totalCountOfProducts: number = 0;
  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
  }

  //Get products
  products: IProduct[];
  getProducts() {
    this.shopService.getProducts(this.ProductsParams).subscribe({
      next: (value: IPagination) => {
        this.products = value.data;
        this.totalCountOfProducts = value.count;
        this.ProductsParams.pageSize = value.pageSize;
      },
    });
  }
  //Get Categories
  categories: ICategory[];
  getCategories() {
    this.shopService.getCategories().subscribe({
      next: (value: ICategory[]) => {
        this.categories = value;
      },
    });
  }



  //filter by category
  onSelectCategory(categoryId: number) {
    this.ProductsParams.categoryId = categoryId;
    this.getProducts();
  }


  //Filtering By Price
  SortingOptions = [
    {name:'Price' , value:'Name'},
    {name:'Price:Min to Max' , value:'priceAse'},
    {name:'Price:Max to Min' , value:'priceDesc'},
  ]
  onSortChange(sort: Event) {
    this.ProductsParams.sortOption = (sort.target as HTMLSelectElement).value;
    this.getProducts();
  }



  //Filtering By Search
  onSearchChange(search: string) {
    this.ProductsParams.Search = search;
    this.getProducts();
  }

//Reset All Filtering
@ViewChild('search') searchInput!: ElementRef<HTMLInputElement>;
@ViewChild('sortSelect') sortSelect!: ElementRef<HTMLSelectElement>;

ResetAll() {
  this.ProductsParams.Search = '';
  this.ProductsParams.categoryId = null;
  this.ProductsParams.sortOption = this.SortingOptions[0].value;

  // Reset input fields
  if (this.searchInput) {
    this.searchInput.nativeElement.value = '';
  }
  if (this.sortSelect) {
    this.sortSelect.nativeElement.value = this.ProductsParams.sortOption;
  }

  //Reset Pagination

  this.getProducts();
}

// Pagination
onPageChanged(event:any){
this.ProductsParams.pageNumber = event.page;
this.getProducts();
}

}
