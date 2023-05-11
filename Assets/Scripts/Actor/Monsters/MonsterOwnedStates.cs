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
        private float currentToPlayerDistance;
        private float currentToOriginDistance;

        public override void Enter(ComSlime actor)
        {
            timer = 0f;
        }

        public override void Excute(ComSlime actor)
        {
            currentToOriginDistance = Vector3.Distance(actor.transform.position, actor.OriginPosition);
            currentToPlayerDistance = Vector3.Distance(actor.transform.position, actor.Player.transform.position);

            if(currentToOriginDistance > actor.DetectionRadius)
            {
                Debug.Log("Player So Far");
                actor.StateMachine.ChangeState(actor.States[(int)SlimeState.Return]);
            }
            else if(currentToPlayerDistance > actor.AttackRange)
            {
                Vector3 toTarget = actor.Player.transform.position;
                toTarget.y = actor.transform.position.y;

                actor.Direction = (toTarget - actor.transform.position).normalized;
                actor.transform.position += actor.Status.SPEED * Time.deltaTime * actor.Direction;
                actor.transform.rotation = Quaternion.LookRotation(actor.Direction, Vector3.up);
            }
            else
            {
                timer += Time.deltaTime;

                if(actor.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    actor.Animator.Play(SlimeState.Idle.ToString());
                }

                if(timer > actor.AttackDelay)
                {
                    actor.Animator.Play(SlimeState.Attack.ToString());

                    timer -= actor.AttackDelay;
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
        float currentToOriginDistance;

        public override void Enter(ComSlime actor)
        {
            actor.Animator.Play(SlimeState.Idle.ToString());
        }

        public override void Excute(ComSlime actor)
        {
            currentToOriginDistance = Vector3.Distance(actor.transform.position, actor.OriginPosition);

            if(currentToOriginDistance >= 0.5f)
            {
                actor.Direction = (actor.OriginPosition - actor.transform.position).normalized;
                actor.transform.position += Time.deltaTime * actor.Status.SPEED * actor.Direction;
                actor.transform.rotation = Quaternion.LookRotation(actor.Direction, Vector3.up);
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
