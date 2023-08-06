import { Component, OnInit } from '@angular/core';
import { CloudProductService } from '../cloud-product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: any[] = [];

  constructor(private productService: CloudProductService) { }

  ngOnInit(): void {
    this.productService.getProducts().subscribe((response: any) => {
      this.products = response.map((product: any) => {
        product.price *= 1.2; // Apply 20% markup
        return product;
      });
    });
  }
}
