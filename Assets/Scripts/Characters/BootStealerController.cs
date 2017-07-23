using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStealerController : MonoBehaviour {

    Animator animator;
    public bool IsAlive;
    public UnityEngine.UI.Text textBox;

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

        IsAlive = false;
        animator.SetTrigger(AnimatorHashes.Reset);
        animator.SetInteger(AnimatorHashes.Type, (int)IdlingType.Death);

        var controller = player.GetComponent<TasksController>();
        var task = controller.Tasks[TasksNames.WrongShoes] as WrongShoes;
        task.HasShoe = true;

        textBox.text = "Brawo, odzyskałeś buta! Wróć do sprzedawczyni i oddaj go jej.";
    }
}
