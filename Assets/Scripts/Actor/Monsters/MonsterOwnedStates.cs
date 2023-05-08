using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor.Monster
{
    public class Idle : State<ComSlime>
    {
        public override void Enter(ComSlime actor)
        {
            actor.Animator.Play(SlimeState.Idle.ToString());
        }

        public override void Excute(ComSlime actor)
        {
            if (actor.Player == null)
            {
                Collider[] colliders = Physics.OverlapSphere(actor.transform.position, actor.DetectionRadius);

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Player"))
                    {
                        Debug.Log($"Detected Player");
                        actor.Player = collider.GetComponent<ComPlayer>();
                        actor.StateMachine.ChangeState(actor.States[(int)SlimeState.Attack]);
                        break;
                    }
                }
            }
        }

        public override void Exit(ComSlime actor)
        {
        }
    }

    public class Attack : State<ComSlime>
    {
        private float timer;

        public override void Enter(ComSlime actor)
        {
            timer = 0f;
        }

        public override void Excute(ComSlime actor)
        {
            float distance = Vector3.Distance(actor.Player.transform.position, actor.OriginPosition);
            Debug.Log(distance);

            if (distance > actor.DetectionRadius)
            {
                Debug.Log($"Player So Far");
                actor.StateMachine.ChangeState(actor.States[(int)SlimeState.Return]);
            }
            else if(distance > actor.AttackRange)
            {
                //TODO Move To Player
                Vector3 toTarget = actor.transform.position;
                toTarget.y = 0f;

                actor.transform.position = Vector3.MoveTowards(actor.transform.position, toTarget, actor.Status.SPEED * Time.deltaTime);
            }
            else
            {
                timer += Time.deltaTime;

                if(timer >= actor.AttackDelay)
                {
                    //TODO Attack
                    actor.Animator.Play(SlimeState.Attack.ToString());

                    timer -= timer;
                }
            }
        }

        public override void Exit(ComSlime actor)
        {
            actor.Player = null;
        }
    }

    public class Damage : State<ComSlime>
    {
        public override void Enter(ComSlime actor)
        {
            actor.Animator.Play(SlimeState.Damage.ToString());
        }

        public override void Excute(ComSlime actor)
        {
            
        }

        public override void Exit(ComSlime actor)
        {
        }
    }

    public class Return : State<ComSlime>
    {
        public override void Enter(ComSlime actor)
        {
            actor.Animator.Play(SlimeState.Idle.ToString());
        }

        public override void Excute(ComSlime actor)
        {
            if (Vector3.Distance(actor.transform.position, actor.OriginPosition) > 1.0f)
            {
                Debug.Log($"{actor.transform.position}, {actor.OriginPosition}");

                actor.transform.position = Vector3.MoveTowards(actor.transform.position, actor.OriginPosition, actor.Status.SPEED * Time.deltaTime);
            }
            else
            {
                actor.StateMachine.ChangeState(actor.States[(int)SlimeState.Idle]);
            }
        }

        public override void Exit(ComSlime actor)
        {
        }
    }
}
