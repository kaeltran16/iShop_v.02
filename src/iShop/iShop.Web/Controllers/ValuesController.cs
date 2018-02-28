using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Repo.Data.Implementations;
using iShop.Repo.Data.Interfaces;
using iShop.Repo.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var repo = _unitOfWork.GetRepository<IProductRepository>();
            var category = await repo.GetAllAsync();
            return Ok(category);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
