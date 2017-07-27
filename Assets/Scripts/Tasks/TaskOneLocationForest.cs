using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskOneLocationForest : MonoBehaviour {

    public UnityEngine.UI.Text textbox;
    WrongShoes task;
    BootStealerController thiefController;
	// Use this for initialization
	void Start ()
    {
        var thief = GameObject.Find("/Tasks/TaskWrongShoes/Złodziej");
        thiefController = thief.GetComponent<BootStealerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (task == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var controller = player.GetComponent<TasksController>();
            task = controller.Tasks[TasksNames.WrongShoes] as WrongShoes;
        }

        if (task.State != TaskState.Opened)
            return;


        if (thiefController.IsAlive)
            textbox.text = "To chyba bandyta, o którym mówiła sprzedawczyni. Kliknij LPM, aby zaatakować go i odzyskać towary!";
    }

    private void OnTriggerExit(Collider other)
    {
        textbox.text = "";
    }
}
