using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AnimationEvent : StateMachineBehaviour
{
    IAnimationInterface inter;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        base.OnStateEnter(animator, stateInfo, layerIndex);
        GetInter(animator).OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        GetInter(animator).OnStateExit(animator, stateInfo, layerIndex);
    }

    IAnimationInterface GetInter(Animator animator)
    {
        if(inter == null)
        {
            inter = animator.GetComponentInParent<IAnimationInterface>();
        }
        return inter;
    }
}
