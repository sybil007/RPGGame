using UnityEngine;
using System.Collections;

public class WrongShoes : ITask
{
    public TaskState State { get; set; }
    public string DisplayText { get; set; }
    public bool HasShoe { get; set; }

    public WrongShoes()
    {
        DisplayText = "Pomóż kobiecie koło placu";
    }

    void Update()
    {
        if (State != TaskState.Opened)
            return;
    }
}
