using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMillAnimation : MonoBehaviour
{
    private const string ANIMATOR_WORK_PARAMETER = "isWork";

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void WorkAnimation(bool isWork)
    {
        animator.SetBool(ANIMATOR_WORK_PARAMETER, isWork);
    }
}
