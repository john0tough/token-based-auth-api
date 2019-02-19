using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext todoContest) { 
            this._context = todoContest;
            if(this._context.TodoItems.Count() < 1){
                this._context.TodoItems.Add( new TodoItem { Name= "Item 1" });
                this._context.SaveChanges();
            }

        }

        // GET api/todo
        [HttpGet("")]
        public ActionResult<IEnumerable<string>> Gets()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/todo/5
        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            return "value" + id;
        }

        // POST api/todo
        [HttpPost("")]
        public void Post([FromBody] string value) { }

        // PUT api/todo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/todo/5
        [HttpDelete("{id}")]
        public void DeleteById(int id) { }
    }
}