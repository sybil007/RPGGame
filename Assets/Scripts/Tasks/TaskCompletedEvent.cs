using UnityEngine;
using UnityEditor;

public class TaskCompletedEvent
{
    public delegate void TaskCompletedEventHandler(string TaskName);
    public static TaskCompletedEventHandler Handler;
}