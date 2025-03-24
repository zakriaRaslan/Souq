import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/Models/IPagination';
import { ICategory } from '../shared/Models/ICategory';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl:string = "https://localhost:44359/api/";
  constructor(private http:HttpClient) { }

  // Get Products
  getProducts(categoryId?:number , sortOption?:string , Search?:string){
    let params = new HttpParams();
    if(categoryId){
      params = params.append('categoryId', categoryId);
    }
    if(sortOption){
      params = params.append('Sort', sortOption);
    }
    if(Search){
      params = params.append('SearchingWord', Search);
    }
    return this.http.get<IPagination>(this.baseUrl + "Product/get-all", {params: params})
  }

  //Get Categories
  getCategories(){
    return this.http.get<ICategory[]>(this.baseUrl + "Category/get-all")
  }
}
