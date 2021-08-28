import { Component, OnInit } from '@angular/core';
import { AnnualreviewService } from '../annualreview.service';
import {
  ChartErrorEvent,
  ChartMouseLeaveEvent,
  ChartMouseOverEvent,
  ChartSelectionChangedEvent,
  ChartType,
  Column,
  GoogleChartComponent
} from 'angular-google-charts';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.scss']
})
export class ChartsComponent implements OnInit {
  title = '';
  type = ChartType.ColumnChart;
  data = [];
  columnNames: Column[] = ['Year', 'Profit/Loss'];
  options = {
    width: 595,
    height: 300,
    backgroundColor: '#ffff00',
    hAxis: { title: '' },
  };
  width = 777;
  height = 300;

  constructor(private annualReviewService: AnnualreviewService) { }

  ngOnInit() {
    this.annualReviewService.getChartWithProfitAndLoss().subscribe(
      result => {
        this.data = [];
        this.title = 'Graphical Anual Review of Profit and Loss';
        this.type = ChartType.ColumnChart;
        console.log(result.list);
        for (const data in result.list) {
          this.data.push([result.list[data].year.toString(), result.list[data].amount]);
        }
      },
      error => {
        console.log(error);
      }
    );

  }
}
