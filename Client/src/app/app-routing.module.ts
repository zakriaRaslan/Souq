import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path:'' , component:HomeComponent},
  {path:'home' , component:HomeComponent},
  {path:'shop' , loadChildren:() => import('./shop/shop.module').then(m=>m.ShopModule)},
  {path:'**' , redirectTo:"/home" , pathMatch:'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
