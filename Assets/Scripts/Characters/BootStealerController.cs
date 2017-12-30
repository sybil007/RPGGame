using UnityEngine;
using Assets.Scripts.Tasks.WrongShoes;
using Assets.Scripts.Tasks;

public class BootStealerController : MonoBehaviour {

    Animator animator;
    public bool IsAlive;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger(AnimatorHashes.Type, (int)IdlingType.GrabFromGround);
        IsAlive = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!IsAlive)
            return;

        if (other.tag != "PlayerWeapon")
            return;

        var player = GameObject.FindGameObjectWithTag("Player");
        var playerScript = player.GetComponent<PlayerController>();

        if (!playerScript.IsAttacking)
            return;

        GameObject.Find("/Tasks/TaskWrongShoes/ForestTrigger").GetComponent<AudioSource>().Stop();

        IsAlive = false;
        GetComponent<AudioSource>().Play();
        animator.SetTrigger(AnimatorHashes.Reset);
        animator.SetInteger(AnimatorHashes.Type, (int)IdlingType.Death);

        var controller = player.GetComponent<TasksController>();
        var task = controller.Tasks[TasksNames.WrongShoes] as WrongShoes;
        task.HasShoe = true;

        TaskTextboxChangeEvent.Handler("Brawo, odzyskałeś buty! Wróć do sprzedawczyni i oddaj je jej.", 10);
        Assets.Scripts.UI.LogbookEvent.Handler(task.DisplayName, "Zabito złodzieja i odzyskano buty.");
    }
}
