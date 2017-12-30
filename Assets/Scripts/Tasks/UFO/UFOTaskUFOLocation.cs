using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tasks.UFO
{
    class UFOTaskUFOLocation : MonoBehaviour
    {
        UFOTask task;

        private void OnTriggerEnter(Collider other)
        {
            if (task == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                var controller = player.GetComponent<TasksController>();
                task = controller.Tasks[TasksNames.UFO] as UFOTask;
            }

            if (other.tag != "Player")
                return;

            if (task.DetailedState != UFOTask.InternalState.Accepted && task.DetailedState != UFOTask.InternalState.TrackingAlien)
                return;

            TaskTextboxChangeEvent.Handler("Podążaj za śladami krwi", 15);
            UI.LogbookEvent.Handler(task.DisplayName, "Znaleziono wrak UFO.");
            task.DetailedState = UFOTask.InternalState.TrackingAlien;
            GetComponent<AudioSource>().Play();
        }
    }
}