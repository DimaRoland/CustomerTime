using System.Collections.Generic;
using CustomerTimesTask.DomainModel;

namespace CustomerTimesTask.Repositories
{
    public interface ICustomTaskRepository
    {
        IList<CustomTask> GetList();

        CustomTask Get(int id);

        int Update(CustomTask task);

        CustomTask Add(CustomTask customTask);

        void Delete(int id);
    }
}