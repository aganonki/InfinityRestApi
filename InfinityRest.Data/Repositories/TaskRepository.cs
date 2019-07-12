using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfinityRest.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace InfinityRest.Data.Repositories
{
    public class TaskRepository : GenericRepository<Task>
    {
        public TaskRepository(InfinityDB context) : base(context)
        {
        }

        public override Task GetByID(object id)
        {
            return DbSet.Where(x => x.Id == (int)id).Include(x => x.TaskSettings).AsNoTracking().FirstOrDefault();
        }
    }
}
