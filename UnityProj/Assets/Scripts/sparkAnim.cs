using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkAnim : StateMachineBehaviour
{
    float timePassed = 0;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed += Time.deltaTime;
        if (timePassed >= 0.25f)
        {
            animator.Rebind();
            animator.Update(0f);
            animator.SetBool("hit", false);
        }
    }
}
