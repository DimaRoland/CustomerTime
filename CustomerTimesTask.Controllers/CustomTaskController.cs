using System.Web.Http;
using System.Web.Http.Cors;
using CustomerTimesTask.ApplicationServices;
using CustomerTimesTask.DomainModel;
//using static CustomerTimesTask.BusInitializer;

namespace CustomerTimesTask.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomTaskController : ApiController
    {
        #region fields

        private readonly ICustomTaskService _customTaskService;

        #endregion fields

        #region constructors

        public CustomTaskController(ICustomTaskService customTaskService)
        {
            _customTaskService = customTaskService;
        }

        #endregion constructors

        #region methods

        [HttpGet, Route("api/task")]
        public IHttpActionResult GetCustomTasks()
        {
            //var bus = CreateBus("CustomerTimesTask.Controllers", x => { });
            //var message = new SomethingHappenedMessage() { What = "Test", };
            //bus.Publish<SomethingHappened>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.Persistent); });
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
            var model = _customTaskService.UpdateCustomTask(customTask);

            return Ok(model);
        }

        [HttpPost, Route("api/task/post")]
        public IHttpActionResult AddCustomTask(CustomTask customTask)
        {
            _customTaskService.AddCustomTask(customTask);

            return Ok(customTask);
        }

        [HttpDelete, Route("api/task")]
        public void DeleteCustomTask([FromUri] int id)
        {
            _customTaskService.Delete(id);
        }

        #endregion methods
    }
}