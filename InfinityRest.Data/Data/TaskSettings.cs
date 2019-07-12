using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InfinityRest.Data.Data
{
    public class TaskSettings
    {
        public int Id { get; set; }

        public int Integer { get; set; }
        public bool ExampleBool { get; set; }

        public string FolderName { get; set; }

        public int TypeId { get; set; }
        public TaskRunType RunType { get; set; }

    }
}
