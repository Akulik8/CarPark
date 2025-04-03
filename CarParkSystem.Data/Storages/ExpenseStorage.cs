using CarParkSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.Data.Storages
{
    class ExpenseStorage
    {
        //private readonly CarParkSystemDbContext _carParkSystemDbContext;

        //public ExpenseStorage(CarParkSystemDbContext carParkSystemDbContext)
        //{
        //    _carParkSystemDbContext = carParkSystemDbContext;
        //}

        //public async Task AddExpenseAsync(Expense expense)
        //{
        //    await _carParkSystemDbContext.Expenses.AddAsync(expense);
        //    await _carParkSystemDbContext.SaveChangesAsync();
        //}

        //public async Task<Expense> GetExpenseByIdAsync(Guid id)
        //{
        //    return await _carParkSystemDbContext.Expenses.FindAsync(id);
        //}

        //public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        //{
        //    return await _carParkSystemDbContext.Expenses.ToListAsync();
        //}

        //public async Task<List<Expense>> GetAllExpensesByFilterAsync(int pageSize, int pageNumber, Expression<Func<Expense, bool>>? filter)
        //{
        //    var query = _carParkSystemDbContext.Expenses.AsQueryable();

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    query = query
        //        .OrderBy(x => x.ExpenseDate)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize);

        //    return await query.ToListAsync();
        //}

        //public async Task UpdateExpenseAsync(Guid id, Expense newExpense)
        //{
        //    var expense = await _carParkSystemDbContext.Expenses
        //               .FirstOrDefaultAsync(a => a.ExpenseID == id);

        //    if (expense != null)
        //    {
        //        expense.VehicleID = newExpense.VehicleID;
        //        expense.ExpenseDate = newExpense.ExpenseDate;
        //        expense.ExpenseType = newExpense.ExpenseType;
        //        expense.Amount = newExpense.Amount;
        //        expense.Description = newExpense.Description;

        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}

        //public async Task DeleteExpenseAsync(Guid id)
        //{
        //    var expense = await _carParkSystemDbContext.Expenses
        //                .FirstOrDefaultAsync(a => a.ExpenseID == id);
        //    if (expense != null)
        //    {
        //        _carParkSystemDbContext.Expenses.Remove(expense);
        //        await _carParkSystemDbContext.SaveChangesAsync();
        //    }
        //}
    }
}