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
  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
  }

  //Get products
  products: IProduct[];
  getProducts() {
    this.shopService.getProducts(this.categoryId,this.sortOption,this.Search).subscribe({
      next: (value: IPagination) => {
        this.products = value.data;
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
  categoryId: number = null;
  onSelectCategory(categoryId: number) {
    this.categoryId = categoryId;
    this.getProducts();
  }


  //Filtering By Price
  sortOption:string;
  SortingOptions = [
    {name:'Price' , value:'Name'},
    {name:'Price:Min to Max' , value:'priceAse'},
    {name:'Price:Max to Min' , value:'priceDesc'},
  ]
  onSortChange(sort: Event) {
    this.sortOption = (sort.target as HTMLSelectElement).value;
    this.getProducts();
  }



  //Filtering By Search
  Search: string;
  onSearchChange(search: string) {
    this.Search = search;
    this.getProducts();
  }

//Reset All Filtering
@ViewChild('search') searchInput!: ElementRef<HTMLInputElement>;
@ViewChild('sortSelect') sortSelect!: ElementRef<HTMLSelectElement>;

ResetAll() {
  this.Search = '';
  this.categoryId = null;
  this.sortOption = this.SortingOptions[0].value;

  // Reset input fields
  if (this.searchInput) {
    this.searchInput.nativeElement.value = '';
  }
  if (this.sortSelect) {
    this.sortSelect.nativeElement.value = this.sortOption;
  }

  this.getProducts();
}

}
