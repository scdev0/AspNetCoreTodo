using System;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTodo.Controllers
{
    /* The controller that handles actions routed to /Todo/* */
    public class TodoController : Controller
    {
        // Actions go here
        public IActionResult Index()
        {
            // Get to-do items from db

            // Put items into a model

            // Render view using the model
        }
    }
}