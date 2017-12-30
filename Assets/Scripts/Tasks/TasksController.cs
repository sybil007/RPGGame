using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tasks.UFO;
using Assets.Scripts.Tasks.WrongShoes;

public class TasksController : MonoBehaviour {

    public Dictionary<string, ITask> Tasks { get; set; }

	// Use this for initialization
	void Start ()
    {
        Tasks = new Dictionary<string, ITask>();
        Tasks.Add(TasksNames.WrongShoes, new WrongShoes());
        Tasks.Add(TasksNames.UFO, new UFOTask());
	}
}
