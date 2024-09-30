import { Component } from '@angular/core';
import { ExpensesService } from '../services/expenses.service';

@Component({
  selector: 'expenses',
  standalone: true,
  imports: [],
  templateUrl: './expenses.component.html',
  styleUrl: './expenses.component.css',
})
export class ExpensesComponent {
  constructor(private expensesService: ExpensesService) {}

  get expenses() {
    return this.expensesService.getExpenses();
  }
}
