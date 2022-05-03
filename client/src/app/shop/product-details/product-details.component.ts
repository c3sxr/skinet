import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    //+  makes it an integer
    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(
      {
        next: product => {
          this.product = product;
        },
        error: e =>{
          console.log(e);
        }
      }
    )
  }

}
