using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace YuoTools
{
    public class YuoStateMachineBehaviour : StateMachineBehaviour
    {
        public UnityEvent<Animator, AnimatorStateInfo, int> onStateEnter;
        public UnityEvent<Animator, AnimatorStateInfo, int> onStateExit;
        public UnityEvent<Animator, AnimatorStateInfo, int> onStateMove;
        public UnityEvent<Animator, AnimatorStateInfo, int> onStateUpdate;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            onStateEnter?.Invoke(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            onStateExit?.Invoke(animator, stateInfo, layerIndex);
        }

        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateMove(animator, stateInfo, layerIndex);
            onStateMove?.Invoke(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            onStateUpdate?.Invoke(animator, stateInfo, layerIndex);
        }
    }
}