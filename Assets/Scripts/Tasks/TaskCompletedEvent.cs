using UnityEngine;

public class TaskCompletedEvent
{
    public delegate void TaskCompletedEventHandler(string TaskName);
    public static TaskCompletedEventHandler Handler;
}