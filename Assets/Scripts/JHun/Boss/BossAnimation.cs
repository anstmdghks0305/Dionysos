using UnityEngine;

namespace Boss
{
    public class BossAnimation : MonoBehaviour
    {
        private BossController controller;
        private Animator animator;
        private BossState state;

        private void Start()
        {
            controller = this.transform.GetComponentInParent<BossController>();
            animator = controller.transform.GetChild(1).GetComponent<Animator>();
        }

        void Update()
        {
            if (state == controller.State)
                return;

            if (controller.State != BossState.Idle)
                animator.SetBool(controller.State.ToString(), true);

            if (state != BossState.Idle)
                animator.SetBool(state.ToString(), false);

            state = controller.State;
        }
    }
}

