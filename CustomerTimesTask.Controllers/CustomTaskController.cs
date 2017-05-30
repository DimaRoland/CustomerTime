using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using CustomerTimesTask.ApplicationServices;
using CustomerTimesTask.DomainModel;
using MassTransit;
using SubscriberMain;

namespace CustomerTimesTask.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomTaskController : ApiController
    {
        #region fields
        private readonly IBus _bus;
        private readonly ICustomTaskService _customTaskService;

        #endregion fields

        #region constructors

        public CustomTaskController(ICustomTaskService customTaskService, IBus bus)
        {
            _customTaskService = customTaskService;
            _bus = bus;
        }

        #endregion constructors

        #region methods


        public async Task SendMes(string actin)
        {
            var addUserEndpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/AddUser1"));
            await addUserEndpoint.Send<MyMessage>(new { Value = actin });
        }

        [HttpGet, Route("api/task")]
        public async Task<IHttpActionResult> GetCustomTasks()
        {
            SendMes("Get");
            var models = _customTaskService.GetList();

            return Ok(models);
        }

        [HttpGet, Route("api/task/{id}")]
        public IHttpActionResult GetCustomTask(int id)
        {
            var data = _customTaskService.GetCustomTask(id);

            return Ok(data);
        }

        [HttpPut, Route("api/task")]
        public IHttpActionResult UpdateCustomTask([FromBody] CustomTask customTask)
        {
            SendMes("Edit");
            var model = _customTaskService.UpdateCustomTask(customTask);

            return Ok(model);
        }

        [HttpPost, Route("api/task/post")]
        public IHttpActionResult AddCustomTask(CustomTask customTask)
        {
            SendMes("Add");
            _customTaskService.AddCustomTask(customTask);

            return Ok(customTask);
        }

        [HttpDelete, Route("api/task")]
        public void DeleteCustomTask([FromUri] int id)
        {
            SendMes("Delete");
            _customTaskService.Delete(id);
        }

        #endregion methods
    }
}