namespace InfinityRest.BLManager.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TaskType { get; set; }
        public int ProcessStateId { get; set; }

        public virtual TaskSettingsEntity TaskSettings { get; set; }
    }
}
