using Microsoft.AspNetCore.Mvc;

[ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private static List<Expense> _expenses = [];
        private static int _idCounter = 1;

        [HttpGet]
        public ActionResult<List<Expense>> GetExpenses()
        {
            return Ok(_expenses);
        }

        [HttpPost]
        public ActionResult<Expense> AddExpense([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("Expense cannot be null.");
            }

            expense.Id = _idCounter++;
            _expenses.Add(expense);
            return CreatedAtAction(nameof(GetExpenses), new { id = expense.Id }, expense);
        }
    }