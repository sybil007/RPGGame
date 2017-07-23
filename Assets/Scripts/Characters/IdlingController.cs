using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlingController : MonoBehaviour
{
    public IdlingType Type;
    // Use this for initialization
    void Start()
    {
        var animator = GetComponent<Animator>();
        animator.SetInteger("Type", 3);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
