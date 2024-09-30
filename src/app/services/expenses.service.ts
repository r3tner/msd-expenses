import { Injectable } from '@angular/core';
import { Expense } from '../models/expense.model';

@Injectable({
  providedIn: 'root', // singleton
})
export class ExpensesService {
  private expenses: Expense[] = [];
  private idCounter = 1;

  addExpense(expense: Expense): void {
    // add new expense with incremented id
    this.expenses.push({ ...expense, id: this.idCounter++ });
    console.log(this.expenses);
  }

  getExpenses(): Expense[] {
    return this.expenses;
  }
}
