using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Record.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Record.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : Controller
    {
        private readonly RecordContext _context;

        public RecordController(RecordContext context)
        {
            _context = context;

            if (_context.Records.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Records.Add(new Models.Record { F1 = "Record1-Field1", F2 = "Record1-Field2", F3= "Record1-Field3" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Models.Record>> GetAll()
        {
            return _context.Records.ToList();
        }

        [HttpGet("{id}", Name = "GetRecord")]
        public ActionResult<Models.Record> GetById(long id)
        {
            var item = _context.Records.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(Models.Record record)
        {
            _context.Records.Add(record);
            _context.SaveChanges();

            return CreatedAtRoute("GetRecord", new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Models.Record record)
        {
            var recordToUpdate = _context.Records.Find(id);
            if (recordToUpdate == null)
            {
                return NotFound();
            }

            recordToUpdate.F1 = record.F1;
            recordToUpdate.F2 = record.F2;
            recordToUpdate.F3 = record.F3;

            _context.Records.Update(recordToUpdate);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
