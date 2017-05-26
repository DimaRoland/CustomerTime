using System;
using System.Collections.Generic;
using CustomerTimesTask.DomainModel;
using CustomerTimesTask.Repositories;

namespace CustomerTimesTask.ApplicationServices
{
    public class CustomTaskService : ICustomTaskService
    {
        #region fields

        private readonly ICustomTaskRepository _taskRepository;

        #endregion fields

        #region constructors

        public CustomTaskService(ICustomTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        #endregion constructors

        #region methods

        public IList<CustomTask> GetList()
        {
            return _taskRepository.GetList();
        }

        public CustomTask GetCustomTask(int id)
        {
            return _taskRepository.Get(id);
        }

        public CustomTask UpdateCustomTask(int id)
        {
            var updateCustomTask = _taskRepository.Update(GetCustomTask(id));
            return GetCustomTask(updateCustomTask);
        }

        public CustomTask AddCustomTask(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        #endregion methods

        #region helpers

        #endregion helpers
    }
}