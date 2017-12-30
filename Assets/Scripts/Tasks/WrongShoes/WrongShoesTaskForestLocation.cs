using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tasks.WrongShoes
{
    public class WrongShoesTaskForestLocation : MonoBehaviour
    {

        WrongShoes task;
        BootStealerController thiefController;
        // Use this for initialization
        void Start()
        {
            var thief = GameObject.Find("/Tasks/TaskWrongShoes/Złodziej");
            thiefController = thief.GetComponent<BootStealerController>();
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
            {
                GetComponent<AudioSource>().Play();
                TaskTextboxChangeEvent.Handler("To chyba bandyta, o którym mówiła sprzedawczyni. Kliknij LPM, aby zaatakować go i odzyskać towary!", 20);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "Player")
                return;
            TaskTextboxChangeEvent.Handler("", 0);
        }
    }
}
