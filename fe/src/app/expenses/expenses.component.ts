import { Component } from '@angular/core';
import { ExpensesService } from '../services/expenses.service';
import { Observable } from 'rxjs';
import { Expense } from '../models/expense.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'expenses',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './expenses.component.html',
  styleUrl: './expenses.component.css',
})
export class ExpensesComponent {
  expenses$!: Observable<Expense[]>;

  constructor(private expensesService: ExpensesService) {}

  ngOnInit() {
    this.expenses$ = this.expensesService.getExpenses();
  }
}
