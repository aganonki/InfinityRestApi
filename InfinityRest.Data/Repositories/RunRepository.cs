
using System.Linq;
using InfinityRest.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace InfinityRest.Data.Repositories
{
    public class RunRepository : GenericRepository<Run>
    {
        public RunRepository(InfinityDB context) : base(context)
        {
        }


        public override Run GetByID(object id)
        {
            return DbSet.Where(x => x.Id == (int)id).Include(x => x.Tasks).ThenInclude(x => x.TaskSettings).FirstOrDefault();
        }
    }
}
