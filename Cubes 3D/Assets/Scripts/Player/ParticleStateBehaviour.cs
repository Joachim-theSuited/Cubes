using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStateBehaviour : StateMachineBehaviour {

    public int particleCount;
    public bool onEnter;
    public bool onExit;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(onEnter)
            animator.GetComponent<ParticleSystem>().Emit(particleCount);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(onExit)
            animator.GetComponent<ParticleSystem>().Emit(particleCount);
    }
}
