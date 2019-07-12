using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InfinityRest.Data.Data;

namespace InfinityRest.Data.Data
{
    public class Run
    {
        public Run()
        {
            this.Tasks = new HashSet<Task>();
        }
        public int Id { get; set; }
        public int Priority { get; set; }
        public DateTime Date { get; set; }
       
        [Required]
        public virtual ICollection<Task> Tasks { get; set; }

    }
}
