using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tasks.UFO
{
    public class UFOTaskTrackingMiddleLocation : MonoBehaviour
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

            if (task.DetailedState != UFOTask.InternalState.TrackingAlien)
                return;

            var audioSource = transform.GetComponent<AudioSource>();
            audioSource.Play();
        }
    }
}