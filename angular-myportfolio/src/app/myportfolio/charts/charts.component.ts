import { Component, OnInit } from '@angular/core';
import {
  ChartErrorEvent,
  ChartMouseLeaveEvent,
  ChartMouseOverEvent,
  ChartSelectionChangedEvent,
  ChartType,
  Column,
  GoogleChartComponent
} from 'angular-google-charts';
import { MyportfolioService } from '../myportfolio.service';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.scss']
})
export class ChartsComponent implements OnInit {
  title = '';
  type = ChartType.PieChart;
  data = [];
  columnNames: Column[] = ['Hospital', 'Private'];
  options = {
    width: 595,
    height: 300,
    backgroundColor: '#809fff',
    hAxis: { title: '' },
  };
  width = 777;
  height = 300;

  constructor(private myportfolioService: MyportfolioService) { }

  ngOnInit(): void {
    this.showNumberOfPatients();
  }

  showNumberOfPatients() {
    this.myportfolioService.showGraphForClientPortfolio().subscribe(
      result => {
        this.data = [];
        this.title = 'Client portfolio';
        this.type = ChartType.PieChart;
        console.log(result.list);
        for (const data in result.list) {
          if (data) {
            this.data.push([result.list[data].symbol.toString(),
              result.list[data].countForSymbol]);
          }
        }
      },
      error => {
        console.log(error);
      }
    );
  }

}
