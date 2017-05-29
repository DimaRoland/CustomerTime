using System.Collections.Generic;
using System.Linq;
using CustomerTimesTask.DomainModel;
using CustomerTimesTask.EntityFramework;

namespace CustomerTimesTask.Repositories
{
    public class CustomTaskRepository : ICustomTaskRepository
    {
        #region fields

        private readonly CustomerTimesTaskDbContext _customerTimesTaskDbContext;

        #endregion fields

        #region constructors

        public CustomTaskRepository(CustomerTimesTaskDbContext customerTimesTaskDbContext)
        {
            _customerTimesTaskDbContext = customerTimesTaskDbContext;
        }

        #endregion constructors

        #region methods

        public IList<CustomTask> GetList()
        {
            return _customerTimesTaskDbContext.Task.ToList();
        }

        public CustomTask Get(int id)
        {
            return _customerTimesTaskDbContext.Task.Find(id);
        }

        public int Update(CustomTask task)
        {
            var customTask = _customerTimesTaskDbContext.Task.Find(task.Id);
            customTask.TaskName = task.TaskName;
            customTask.IsTaskEnd = task.IsTaskEnd;
            customTask.IsTaskEnd = task.IsTaskEnd;
            _customerTimesTaskDbContext.SaveChanges();
            return _customerTimesTaskDbContext.SaveChanges();
        }

        public CustomTask Add(CustomTask customTask)
        {
            _customerTimesTaskDbContext.Task.Add(customTask);
            _customerTimesTaskDbContext.SaveChanges();
            return customTask;
        }

        public void Delete(int id)
        {
            _customerTimesTaskDbContext.Task.Remove(Get(id));
            _customerTimesTaskDbContext.SaveChanges();
        }

        #endregion methods
    }
}