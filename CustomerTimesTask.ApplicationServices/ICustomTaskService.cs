using System.Collections.Generic;
using CustomerTimesTask.DomainModel;

namespace CustomerTimesTask.ApplicationServices
{
    public interface ICustomTaskService
    {
        IList<CustomTask> GetList();

        CustomTask GetCustomTask(int id);

        CustomTask UpdateCustomTask(CustomTask customTask);

        CustomTask AddCustomTask(CustomTask customTask);

        void Delete(int id);
    }
}