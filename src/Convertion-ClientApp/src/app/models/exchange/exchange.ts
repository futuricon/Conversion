import { Currency } from '../currency/currency';

export interface Exchange {
    exchangeId?: string;
    incomeAmount: number;
    outcomeAmount: number;
    date?: Date;
    fromCurrency?: Currency; 
    toCurrency?: Currency;
}
