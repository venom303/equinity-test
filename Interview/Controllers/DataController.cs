using Interview.Domain;
using Interview.Infrastructure;
using System;
using System.Linq;
using System.Web.Http;

namespace Interview.Controllers
{
    public class DataController : ApiController
    {
        private readonly IDataRepository _dataRepository;

        public DataController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IHttpActionResult Get()
        {
            try
            {
                return Json(_dataRepository.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Get(Guid id)
        {
            try
            {
                var item = _dataRepository.GetOne(id);

                if (item == null)
                    return NotFound();

                return Json(item);
            }
            catch
            {
                return BadRequest();
            }
        }

        public IHttpActionResult Post([FromBody]Data data)
        {
            try
            {
                _dataRepository.Add(data);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        [TokenAuthentication]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                _dataRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}