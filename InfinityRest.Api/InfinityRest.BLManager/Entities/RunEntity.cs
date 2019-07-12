using System;
using System.Collections.Generic;

namespace InfinityRest.BLManager.Entities
{
    public class RunEntity
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public DateTime Date { get; set; }
        
        public List<TaskEntity> Tasks { get; set; }
    }
}
