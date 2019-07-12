using InfinityRest.BLManager.Entities;
using InfinityRest.BLManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfinityRest.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class RunsController : Controller
    {
        private IService<RunEntity> _service;

        public RunsController(IService<RunEntity> unit)
        {
            _service = unit;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var items = _service.GetAll();
            if (items != null)
            {
                return Ok(items);
            }
            else
                return NotFound();
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody]RunEntity value)
        {
            return _service.Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]RunEntity value)
        {
            if (id > 0)
            {
                return _service.Update(id, value);
            }
            return false;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
