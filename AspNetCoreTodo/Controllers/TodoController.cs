using System;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers
{
    /* The controller that handles actions routed to /Todo/* */
    public class TodoController : Controller
    {
        // private variable to reference the ITodoItemService
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // Actions go here
        public async Task<IActionResult> Index()
        {
            // Get to-do items from db
            var items = await _todoItemService.GetIncompleteItemsAsync();

            // Put items into the view model
            var model = new TodoViewModel()
            {
                Items = items
            };

            // Render view using the model
            return View(model);
        }
    }
}