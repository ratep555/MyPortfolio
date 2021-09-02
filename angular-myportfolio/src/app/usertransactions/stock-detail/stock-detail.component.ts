import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IStock } from 'src/app/shared/models/stock';
import { StockService } from 'src/app/stock/stock.service';

@Component({
  selector: 'app-stock-detail',
  templateUrl: './stock-detail.component.html',
  styleUrls: ['./stock-detail.component.scss']
})
export class StockDetailComponent implements OnInit {
  stock: IStock;

  constructor(private stockService: StockService,
              private activatedRoute: ActivatedRoute) { }

ngOnInit(): void {
this.loadStock();
}

loadStock() {
return this.stockService.getStock(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
this.stock = response;
}, error => {
console.log(error);
});

}

}
