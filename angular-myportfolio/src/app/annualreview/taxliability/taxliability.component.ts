import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IAnnualTaxLiability } from 'src/app/shared/models/annual';
import { INewSurtax, INewSurtax1 } from 'src/app/shared/models/surtax';
import { AnnualreviewService } from '../annualreview.service';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';

@Component({
  selector: 'app-taxliability',
  templateUrl: './taxliability.component.html',
  styleUrls: ['./taxliability.component.scss']
})
export class TaxliabilityComponent implements OnInit {
  annual: IAnnualTaxLiability;
  selectedOption: any;
  surtaxes: INewSurtax[];
  surtaxForm: FormGroup;

  constructor(private annualReviewService: AnnualreviewService,
              private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit(): void {
    this.surtaxForm = this.formBuilder.group({
      id: [null],
      residence: [null]
    });
    this.showSurtaxes();
    this.showAnnual();
  }

  showSurtaxes() {
    this.annualReviewService.getSurtaxes().subscribe(response => {
        this.surtaxes = response;
    }, error => {
      console.log(error);
    });
  }

  showAnnual() {
    this.annualReviewService.showAnnual().subscribe(response => {
        this.annual = response;
    }, error => {
      console.log(error);
    });
  }

  showTaxliability() {
    this.annualReviewService.showTaxLiability(this.surtaxForm.get('id').value).subscribe(response => {
        this.annual = response;
    }, error => {
      console.log(error);
    });
  }

  onSelect(event: TypeaheadMatch): void {
    const newSurtax: INewSurtax1 = event.item;
    this.surtaxForm.patchValue({
      id: newSurtax.id
    });
  }

  onSubmit() {
    this.annualReviewService.showTaxLiability(this.surtaxForm.get('id').value).subscribe(response => {
      this.showTaxliability();
   },
   error => {
     console.log(error);
   });
 }
}



