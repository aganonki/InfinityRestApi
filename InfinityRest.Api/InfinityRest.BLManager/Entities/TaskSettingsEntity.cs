using System.Collections.Generic;

namespace InfinityRest.BLManager.Entities
{
    public class TaskSettingsEntity
    {
        public int Id { get; set; }

        public int Integer { get; set; }
        public bool ExampleBool { get; set; }

        public string FolderName { get; set; }

        public int TypeId { get; set; }
        public TaskRunTypeEntity RunType { get; set; }
    }
}
