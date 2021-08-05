import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { Currency } from '../models/currency/currency';
import { Exchange } from '../models/exchange/exchange';

@Component({
  selector: 'app-exchange',
  templateUrl: './exchange.component.html', 
})

export class ExchangeComponent implements OnInit {
  currencies!: Currency[]; //list of actual currency
  exchange: Exchange = {incomeAmount: 0, outcomeAmount: 0}; //exchange history object
  private headers: HttpHeaders;
  constructor(private http: HttpClient) { 
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
  }

  ngOnInit() {
    this.getValues();
  }

  inDefaultValue = 840;
  outDefaultValue = 643;
  inAmountDefaulValue = 0;
  outAmountDefaulValue = 0;

  //new form group instance
  form = new FormGroup({
    inCurrency: new FormControl(''), //select in Currency
    outCurrency: new FormControl(''), //select out Currency
    inAmount: new FormControl(''), //input with in Amount 
    outAmount: new FormControl('') //input with out Amount 
  });

  //xz
  get f(){
    return this.form.controls;
  }
   
  //solve exchange when press submit button
  submit(){
    let amount = this.form.value['inAmount'];
    let inCurr = this.currencies.filter(x => x.code == this.form.value['inCurrency'])[0];
    let outCurr = this.currencies.filter(x => x.code == this.form.value['outCurrency'])[0];
    let xresult = amount * inCurr.rate / outCurr.rate;
    console.log(xresult);
    this.form.patchValue({
      outAmount: xresult
    });
    this.addExchange(amount, xresult, inCurr, outCurr);
  }
  
  //get actual currency on page load
  getValues() {
    this.http.get('http://localhost:46076/api/Convertion/GetCurrencies').subscribe(response => {
      this.currencies = response as Currency[];
      this.currencies.sort((a,b) => a.ccy.localeCompare(b.ccy));
    });
    console.log('new data loaded');
  }

  //In Currency on selection change
  changeCurrency(e:any) {
    this.form.patchValue({
      outAmount: 0
    });
  }

  //post exchange history
  addExchange(inAmount: number, outAmount: number, fromCurr: Currency, toCurr: Currency){
    this.exchange.incomeAmount = inAmount;
    this.exchange.outcomeAmount = outAmount;
    this.exchange.date = new Date;
    this.exchange.fromCurrency = fromCurr;
    this.exchange.toCurrency = toCurr;
    this.exchange.date.setHours(0,0,0,0);
    this.http.post('http://localhost:46076/api/Convertion/CreateExchange', JSON.stringify(this.exchange), {headers: this.headers})
    .subscribe(result => { console.log("Posted" + JSON.stringify(result)); }, error => console.error(error));
    this.getValues();
  }
}
