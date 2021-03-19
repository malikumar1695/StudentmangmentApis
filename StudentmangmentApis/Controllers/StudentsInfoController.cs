using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMangment.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentmangmentApis.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentsInfoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<StudentViewModel> students = new()
            {
                new StudentViewModel() { AddmissionDate = DateTime.Now, Class = "1", Name = "Ahsan", RollNo = 2 },
                new StudentViewModel() { AddmissionDate = DateTime.Now, Class = "4", Name = "Ali", RollNo = 23 },
                new StudentViewModel() { AddmissionDate = DateTime.Now, Class = "7", Name = "Tariq", RollNo = 53 },
                new StudentViewModel() { AddmissionDate = DateTime.Now, Class = "2", Name = "Toheed", RollNo = 66 },
            };

            return Ok(students);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]object name)
        {
            return Ok($"data posted by {name}");
        }
        
    }
}
