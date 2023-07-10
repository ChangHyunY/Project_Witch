using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor
{
    public class Idle : State<Player>
    {
        public override void Enter(Player actor)
        {
            actor.Animator.Play(PlayerState.Idle.ToString());
        }

        public override void Excute(Player actor)
        {
        }

        public override void Exit(Player actor)
        {
        }
    }

    public class Walk : State<Player>
    {
        public override void Enter(Player actor)
        {
            actor.Animator.Play(PlayerState.Walk.ToString());
        }

        public override void Excute(Player actor)
        {
            actor.RigidBody2D.velocity = new Vector2(actor.Direction.x * actor.Status.MoveSpeed, actor.RigidBody2D.velocity.y);

            if(actor.Direction.x != 0)
            {
                var scale = actor.transform.localScale;
                scale.x = actor.Direction.x;
                actor.transform.localScale = scale;                
            }
        }

        public override void Exit(Player actor)
        {
            actor.RigidBody2D.velocity = new Vector2(0, actor.RigidBody2D.velocity.y);
        }
    }

    public class Jump : State<Player>
    {
        public override void Enter(Player actor)
        {
            //actor.Animator.Play(PlayerState.Jump.ToString());
        }

        public override void Excute(Player actor)
        {
        }

        public override void Exit(Player actor)
        {
        }
    }

    public class Attack : State<Player>
    {
        public override void Enter(Player actor)
        {
            actor.Animator.Play(PlayerState.Attack01.ToString());
        }

        public override void Excute(Player actor)
        {
            if(actor.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                actor.StateMachine.ReverToPreviousState();
            }
        }

        public override void Exit(Player actor)
        {
        }
    }
}