public interface ITask
{
    string DisplayText { get; set; }
    TaskState State { get; set; }
}