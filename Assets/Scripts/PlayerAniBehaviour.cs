using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniBehaviour : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.IsName("Run"))
        {
            PlayerFire pf = animator.GetComponentInParent<PlayerFire>();
            pf?.SetMoveSpeedByAnimationEvent();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        if (stateInfo.IsName("Reload"))
        {
            PlayerFire pf = animator.GetComponentInParent<PlayerFire>();
            pf?.OnReloadComplete();
        }
    }
}
