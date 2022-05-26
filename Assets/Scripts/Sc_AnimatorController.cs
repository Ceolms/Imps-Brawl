using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_AnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetWalking(bool walking)
    {
        animator.SetBool("b_Walking", walking);
    }
}
