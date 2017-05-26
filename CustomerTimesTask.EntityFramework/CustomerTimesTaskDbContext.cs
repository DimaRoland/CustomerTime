
using System.Data.Entity;
using System.Threading.Tasks;
using CustomerTimesTask.DomainModel;

namespace CustomerTimesTask.EntityFramework
{
    public class CustomerTimesTaskDbContext : DbContext
    {
        public CustomerTimesTaskDbContext() : base("TaskDbContext")  
        {
        }

        public DbSet<CustomTask> Task { get; set; }
    }
}