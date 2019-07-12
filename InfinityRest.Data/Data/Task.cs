using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace InfinityRest.Data.Data
{
    public class Task : BaseEntity
    {
        [Required]
        public int RunId { get; set; }
        [Required]
        public int TaskTypeId { get; set; }
        public int ProcessStateId { get; set; }
        public virtual TaskType TaskTypeLink { get; set; }
        public virtual ProcessState ProcessStateLink { get; set; }

        public virtual TaskSettings TaskSettings { get; set; }

    }
}
