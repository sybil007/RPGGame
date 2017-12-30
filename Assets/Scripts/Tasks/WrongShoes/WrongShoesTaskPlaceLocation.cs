using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tasks.WrongShoes
{
    public class WrongShoesTaskPlaceLocation : MonoBehaviour
    {
        WrongShoes task;
        public AudioClip endDialog;

        private void OnTriggerStay(Collider other)
        {
            if (!Input.GetKey(KeyCode.E))
                return;

            if (other.tag != "Player")
                return;

            if (task.State == TaskState.NotStarted)
            {
                TaskTextboxChangeEvent.Handler("On za chwilę zrobi mi krzywdę! Gdy szłam z dostawą na targ, zaatakował mnie bandyta, musiałam zostawić buty i uciekać. Proszę, odzyskaj je.", 10);
                UI.LogbookEvent.Handler(task.DisplayName, "Rozpoczęto zadanie.");
                task.State = TaskState.Opened;
                GetComponent<AudioSource>().Play();
            }

            if (task.State == TaskState.Opened && task.HasShoe)
            {
                var marcinek = GameObject.FindGameObjectWithTag("Marcinek");
                var animator = marcinek.GetComponent<Animator>();
                animator.SetTrigger("Reset");
                animator.SetInteger(AnimatorHashes.Type, (int)IdlingType.Neutral);
                marcinek.GetComponent<AudioSource>().Stop();
                var script = marcinek.GetComponent<MarcinekController>();
                script.IsAngry = false;

                TaskTextboxChangeEvent.Handler("Dziękuję, uratowałeś mi życie!", 10);
                UI.LogbookEvent.Handler(task.DisplayName, "Ukończono zadanie.");
                task.State = TaskState.Finished;
                var audio = GetComponent<AudioSource>();
                audio.Stop();
                audio.clip = endDialog;
                audio.Play();
                TaskCompletedEvent.Handler(TaskNames.WrongShoes);
            }

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

            if (task.State == TaskState.Finished || task.State == TaskState.Failed)
                return;

            if (task.State == TaskState.NotStarted)
            {
                TaskTextboxChangeEvent.Handler("Naciśnij 'E', aby rozpocząć zadanie", 20);
            }
            else if (task.HasShoe)
            {
                TaskTextboxChangeEvent.Handler("Naciśnij 'E', aby zakończyć zadanie", 20);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (task.State == TaskState.NotStarted)
                TaskTextboxChangeEvent.Handler("", 0);
        }
    }
}