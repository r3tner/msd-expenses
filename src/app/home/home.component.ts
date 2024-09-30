import { Component } from '@angular/core';
import { AddExpenseComponent } from '../add-expense/add-expense.component';
import { ExpensesComponent } from '../expenses/expenses.component';

@Component({
  selector: 'home',
  standalone: true,
  imports: [AddExpenseComponent, ExpensesComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {}
