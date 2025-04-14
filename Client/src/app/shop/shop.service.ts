import { GetProductsParams } from './../shared/Models/GetProductsParams';
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
  getProducts(ProductsParams:GetProductsParams){
    let params = new HttpParams();
    if(ProductsParams.categoryId){
      params = params.append('categoryId', ProductsParams.categoryId);
    }
    if(ProductsParams.sortOption){
      params = params.append('Sort', ProductsParams.sortOption);
    }
    if(ProductsParams.Search){
      params = params.append('SearchingWord', ProductsParams.Search);
    }
    params = params.append('pageNumber', ProductsParams.pageNumber);
    params = params.append('pageSize', ProductsParams.pageSize);
    return this.http.get<IPagination>(this.baseUrl + "Product/get-all", {params: params})
  }

  //Get Categories
  getCategories(){
    return this.http.get<ICategory[]>(this.baseUrl + "Category/get-all")
  }

  //Get Product By Id
  getProductById(id:number){
    return this.http.get(this.baseUrl + "Product/get/" + id)
  }
}
