using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tasks.UFO
{
    class UFOTaskStartLocation : MonoBehaviour
    {
        public AudioClip taskAcceptedClip;

        UFOTask task;
        AudioSource audioSource;
        AudioClip startClip;
        bool wasClipChanged = false;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            startClip = audioSource.clip;
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
                TaskTextboxChangeEvent.Handler("Idź zbadać UFO", 7);
                UI.LogbookEvent.Handler(task.DisplayName, "Rozpoczęto zadanie.");
                task.State = TaskState.Opened;
                task.DetailedState = UFOTask.InternalState.Accepted;
                audioSource.clip = taskAcceptedClip;
                wasClipChanged = true;
                audioSource.Play();
            }
        }

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

            if (wasClipChanged)
            {
                audioSource.clip = startClip;
                wasClipChanged = false;
            }
            audioSource.Play();

            if (task.State == TaskState.Finished || task.State == TaskState.Failed)
                return;

            if (task.State == TaskState.NotStarted)
                TaskTextboxChangeEvent.Handler("Naciśnij 'E', aby rozpocząć zadanie", 20);
        }

        private void OnTriggerExit(Collider other)
        {
            if (task.State == TaskState.NotStarted)
                TaskTextboxChangeEvent.Handler("", 0);
        }
    }
}