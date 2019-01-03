using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApis.Models;

namespace TodoApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController:ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodosItems.Count() == 0)
            {
                _context.TodosItems.Add(new TodoItem {Name = "Item1"});
                _context.SaveChanges();
            }
        }

        //GET:Api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems(){
            return await _context.TodosItems.ToListAsync();
        }

        //GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>>GetTodoItem(long id)
        {
            var TodoItem = await _context.TodosItems.FindAsync(id);

            if (TodoItem == null)
                return NotFound();
            
            return TodoItem;

        }
    }
}