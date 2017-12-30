using UnityEngine;

namespace Assets.Scripts.Tasks.UFO
{
    public class UFOTaskAlienController : MonoBehaviour
    {

        Animator animator;
        private bool IsAlive;
        private PlayerController playerScript;
        private GameObject player;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetInteger(AnimatorHashes.Type, (int)IdlingType.Neutral);
            IsAlive = true;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsAlive)
                return;

            if (other.tag != "PlayerWeapon")
                return;

            if (playerScript == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                playerScript = player.GetComponent<PlayerController>();
            }

            if (!playerScript.IsAttacking)
                return;

            IsAlive = false;
            animator.SetTrigger(AnimatorHashes.Reset);
            animator.SetInteger(AnimatorHashes.Type, (int)IdlingType.Death);
            GetComponent<AudioSource>().loop = false;

            var controller = player.GetComponent<TasksController>();
            var task = controller.Tasks[TasksNames.UFO] as UFOTask;
            task.State = TaskState.Finished;
            task.DetailedState = UFOTask.InternalState.AlienKilled;

            TaskTextboxChangeEvent.Handler("Ukończyłeś misję zabijając obcego.", 7);
            UI.LogbookEvent.Handler(task.DisplayName, "Ukończono zadanie zabijając obcego.");
        }
    }
}