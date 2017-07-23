using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksController : MonoBehaviour {

    public Dictionary<string, ITask> Tasks { get; set; }

	// Use this for initialization
	void Start ()
    {
        Tasks = new Dictionary<string, ITask>();
        Tasks.Add(TasksNames.WrongShoes, new WrongShoes());
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
}
