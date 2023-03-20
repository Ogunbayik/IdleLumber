using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string ANIMATOR_RUN_PARAMETER = "isRun";

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void RunAnimation(bool isRun)
    {
        animator.SetBool(ANIMATOR_RUN_PARAMETER, isRun);
    }

}
