public interface ITask
{
    TaskState State { get; set; }
    string DisplayName { get; }
}