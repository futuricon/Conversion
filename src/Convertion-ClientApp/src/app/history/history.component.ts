import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Exchange } from '../models/exchange/exchange';
import { Currency } from '../models/currency/currency';
import { History } from '../models/history/history';
import {NgbDateStruct, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormControl} from '@angular/forms';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html' 
})
export class HistoryComponent implements OnInit {
 
  model: NgbDateStruct;
  currencies!: Currency[]; //list of currency
  exchange!: Exchange[]; //list of exchange
  history: History={date: new Date, fromCode: 840, toCode: 643};
  inDefaultValue = 840;
  outDefaultValue = 643;
  private headers: HttpHeaders;

  constructor(calendar: NgbCalendar,
    private http: HttpClient) {
      this.model = calendar.getToday();
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
  }

  //new form group instance
  form = new FormGroup({
    fromCurrency: new FormControl(''), //select in Currency
    toCurrency: new FormControl(''), //select out Currency
  });

  ngOnInit() {
    this.getValues();
  }
  
  getValues() {
    this.http.get('http://localhost:46076/api/Convertion/GetCurrencies').subscribe(response => {
      this.currencies = response as Currency[];
      this.currencies.sort((a,b) => a.ccy.localeCompare(b.ccy));
    });
    this.getExchange(this.history);   
    //get currency list page load
    
    console.log('data loaded');
  }

  onSearch(){
    this.history.date.setFullYear(this.model.year);
    this.history.date.setMonth(this.model.month-1);
    this.history.date.setDate(this.model.day);
    console.log("onSearch");
    this.history.fromCode = this.form.value['fromCurrency'];
    this.history.toCode = this.form.value['toCurrency'];
    this.getExchange(this.history);    
  }

  getExchange(history: History){
    
    console.log("exMethos");
    console.log(history);
    this.http.post('http://localhost:46076/api/Convertion/GetExchanges', JSON.stringify(history), {headers: this.headers})
      .subscribe(response => {
      this.exchange = response as Exchange[];
      this.exchange.sort((a,b) => a.incomeAmount - b.incomeAmount);
    });
  }
}
