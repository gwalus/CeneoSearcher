using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebEngine.Controllers
{
    public class CeneoController : BaseApiController
    {
        [HttpGet]
        public List<Person> GetPeople()
        {
            return new List<Person>() { new Person() { Id = 1, Name = "Hugo" }, new Person() { Id = 2, Name = "Kopter" } };
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
