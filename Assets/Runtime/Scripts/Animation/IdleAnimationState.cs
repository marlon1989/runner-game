using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController player = animator.transform.parent.GetComponent<PlayerController>();

        if (player != null)
        {
            player.enabled = false;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);
        }
    }

}
