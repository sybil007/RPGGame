using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tasks.UFO
{
    class UFOTaskMedicineLocation : MonoBehaviour
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
            if (task.DetailedState != UFOTask.InternalState.AlienFound)
                return;

            TaskTextboxChangeEvent.Handler("Naciśnij 'E', aby wziąć lekarstwa", 20);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag != "Player")
                return;
            if (task.DetailedState != UFOTask.InternalState.AlienFound)
                return;

            if (Input.GetKey(KeyCode.E))
            {
                GetComponent<AudioSource>().Play();
                task.DetailedState = UFOTask.InternalState.MedicineTaken;
                TaskTextboxChangeEvent.Handler("Wróć do obcego, aby mu pomóc", 15);
                UI.LogbookEvent.Handler(task.DisplayName, "Wzięto lekarstwo dla obcego.");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "Player")
                return;
            if (task.DetailedState == UFOTask.InternalState.AlienFound)
                TaskTextboxChangeEvent.Handler("", 0);
        }
    }
}