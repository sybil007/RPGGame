using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskOneLocationPlace : MonoBehaviour {

    WrongShoes task;
    public UnityEngine.UI.Text textbox;
	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	private void OnTriggerStay(Collider other)
    {
        if (!Input.GetKey(KeyCode.E))
            return;

        if (other.tag != "Player")
            return;

        if (task.State == TaskState.NotStarted)
        {
            textbox.text = "On za chwilę zrobi mi krzywdę! Gdy szłam z dostawą na targ, zaatakował mnie bandyta, musiałam zostawić buty i uciekać. Proszę, odzyskaj je.";
            task.State = TaskState.Opened;
        }

        if (task.State == TaskState.Opened && task.HasShoe)
        {
            var marcinek = GameObject.FindGameObjectWithTag("Marcinek");
            var animator = marcinek.GetComponent<Animator>();
            animator.SetTrigger("Reset");
            animator.SetInteger(AnimatorHashes.Type, (int)IdlingType.Neutral);
            var audioSource = marcinek.GetComponent<AudioSource>();
            audioSource.Stop();
            var script = marcinek.GetComponent<MarcinekController>();
            script.IsAngry = false;
            textbox.text = "Dziękuję, uratowałeś mi życie!";
            task.State = TaskState.Finished;
            TaskCompletedEvent.Handler(TaskNames.WrongShoes);
        }
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if(task == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var controller = player.GetComponent<TasksController>();
            task = controller.Tasks[TasksNames.WrongShoes] as WrongShoes;   
        }

        if (task.State == TaskState.Finished || task.State == TaskState.Failed)
            return;

        if (!task.HasShoe)
        {
            textbox.text = "Naciśnij 'E', aby rozpocząć zadanie";
        }
        else
        {
            textbox.text = "Naciśnij 'E', aby zakończyć zadanie";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        textbox.text = "";
    }
}
