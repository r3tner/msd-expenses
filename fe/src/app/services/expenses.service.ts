import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Expense } from '../models/expense.model';

@Injectable({
  providedIn: 'root', // singleton
})
export class ExpensesService {
  private socket!: WebSocket;
  // private expenses: Expense[] = [];
  // private expensesSubject = new Subject<Expense>();
  // private idCounter = 1;

  constructor() {
    this.connect();
  }

  private connect(): void {
    this.socket = new WebSocket('ws://localhost:5001/expenses');

    this.socket.onerror = (event) => {
      console.error('WebSocket error:', event);
    };
  }

  addExpense(expense: Expense): void {
    if (this.socket.readyState === WebSocket.OPEN) {
      this.socket.send(JSON.stringify(expense));
    } else {
      console.error('WebSocket not open.');
    }
  }

  getExpenses(): Observable<Expense[]> {
    return new Observable((observer) => {
      this.socket.onmessage = (event) => {
        const expenses = JSON.parse(event.data) as Expense[];
        observer.next(expenses);
      };

      this.socket.onerror = (event) => {
        observer.error(event);
      };

      this.socket.onclose = () => {
        observer.complete();
      };

      return () => {
        this.socket.close();
      };
    });
  }
}
