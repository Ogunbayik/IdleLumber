using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerTrigger playerTrigger;

    private const string ANIMATOR_RUN_PARAMETER = "isRun";
    private const string ANIMATOR_CARRY_PARAMETER = "isCarry";
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerTrigger = GetComponentInParent<PlayerTrigger>();
    }
    private void Start()
    {
        playerTrigger.OnCollectWood += PlayerTrigger_OnCollectWood;
    }

    private void PlayerTrigger_OnCollectWood(object sender, System.EventArgs e)
    {
        CarryAnimation(true);
    }

    public void RunAnimation(bool isRun)
    {
        animator.SetBool(ANIMATOR_RUN_PARAMETER, isRun);
    }
    public void CarryAnimation(bool isCarry)
    {
        animator.SetBool(ANIMATOR_CARRY_PARAMETER, isCarry);
    }

}
