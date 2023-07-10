using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Witch.Actor
{
    public class ComPlayer : Player
    {
        [SerializeField] short m_MaxJumpCount = 2;

        private short m_CurrentJumpCount;

        private void Start()
        {
            Setup();

            m_CurrentJumpCount = 0;
        }

        protected override void Updated()
        {
            base.Updated();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Ground"))
            {
                m_CurrentJumpCount = 0;

                Debug.Log($"Hit ground {m_CurrentJumpCount}");
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Direction = context.ReadValue<Vector2>().normalized;
            
            if(Direction.x == 0)
            {
                ChangeState(PlayerState.Idle);
            }
            else
            {
                ChangeState(PlayerState.Walk);
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            Debug.Log(context.phase);

            if(context.phase == InputActionPhase.Started)
            {
                if(m_CurrentJumpCount < m_MaxJumpCount)
                {
                    m_CurrentJumpCount++;

                    RigidBody2D.velocity = new Vector2(RigidBody2D.velocity.x, 5.0f);
                }
            }
        }
    }
}