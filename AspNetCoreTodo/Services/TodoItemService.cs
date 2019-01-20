using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        // private variable to reference the ApplicationDbContext
        private readonly ApplicationDbContext _dbContext;

        public TodoItemService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user)
        {
            var items = await _dbContext.Items
                .Where(x => x.IsDone == false && x.UserId == user.Id)
                .ToArrayAsync();

            return items;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user)
        {
            newItem.Id = Guid.NewGuid();
            newItem.UserId = user.Id;
            newItem.IsDone = false;
            // TODO: use datepicker for due date
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);

            _dbContext.Items.Add(newItem);

            var saveResult = await _dbContext.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }

        public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user)
        {
            var item = await _dbContext.Items
                .Where(x => x.Id == id && x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (item == null)
            {
                return false;
            }

            item.IsDone = true;

            var saveResult = await _dbContext.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}