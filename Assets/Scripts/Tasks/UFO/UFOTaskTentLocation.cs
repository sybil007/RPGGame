using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tasks.UFO
{
    class UFOTaskTentLocation : MonoBehaviour
    {
        UFOTask task;
        GameObject alien;
        AudioSource alienAudioSource;

        public AudioClip HelpAlienSound;

        private void Start()
        {
            alien = GameObject.FindGameObjectWithTag("UFOTaskAlien");
            alienAudioSource = alien.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        private void OnTriggerStay(Collider other)
        {
            if (task.DetailedState != UFOTask.InternalState.MedicineTaken || !Input.GetKey(KeyCode.E))
                return;
            if (other.tag != "Player")
                return;

            TaskTextboxChangeEvent.Handler("Ukończyłeś misję pomagając obcemu.", 7);
            UI.LogbookEvent.Handler(task.DisplayName, "Ukończono zadanie pomagając obcemu.");
            task.State = TaskState.Finished;
            task.DetailedState = UFOTask.InternalState.AlienCured;
            alienAudioSource.Stop();

            var audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            audioSource.clip = HelpAlienSound;
            audioSource.Play();
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

            if (task.State == TaskState.Finished || task.State == TaskState.Failed)
                return;

            alienAudioSource.Play();

            if (task.DetailedState == UFOTask.InternalState.TrackingAlien)
            {
                task.DetailedState = UFOTask.InternalState.AlienFound;
                UI.LogbookEvent.Handler(task.DisplayName, "Odnaleziono obcego. Można go zabić lub opatrzyć przynosząc lekarstwa z domu.");
            }

            if (task.DetailedState != UFOTask.InternalState.AlienFound && task.DetailedState != UFOTask.InternalState.MedicineTaken)
                return;

            GetComponent<AudioSource>().Play();
            if (task.DetailedState == UFOTask.InternalState.AlienFound)
                TaskTextboxChangeEvent.Handler("Pomóż obcemu przynosząc lekarstwo ze swojego domu lub zabij go.", 20);
            else if (task.DetailedState == UFOTask.InternalState.MedicineTaken)
                TaskTextboxChangeEvent.Handler("Naciśnij 'E', aby opatrzyć obcego lub zabij go", 20);

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "Player")
                return;

            alienAudioSource.Stop();
            if (task.DetailedState == UFOTask.InternalState.MedicineTaken)
                TaskTextboxChangeEvent.Handler("", 0);
        }
    }
}