using System.Collections.Generic;
using CustomerTimesTask.DomainModel;

namespace CustomerTimesTask.ApplicationServices
{
    public interface ICustomTaskService
    {
        IList<CustomTask> GetList();

        CustomTask GetCustomTask(int id);

        CustomTask UpdateCustomTask(int id);

        CustomTask AddCustomTask(int id);

        void Delete(int id);
    }
}