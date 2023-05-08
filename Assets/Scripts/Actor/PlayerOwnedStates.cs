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
        }

        public override void Exit(Player actor)
        {
        }
    }

    public class Run : State<Player>
    {
        public override void Enter(Player actor)
        {
            actor.Animator.Play(PlayerState.Run.ToString());
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