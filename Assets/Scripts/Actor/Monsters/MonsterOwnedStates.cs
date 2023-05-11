using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor.Monster
{
    public class Idle<T> : State<BaseMonster<T>>
    {
        public override void Enter(BaseMonster<T> actor)
        {
            actor.Animator.Play(State.Idle.ToString());
        }

        public override void Excute(BaseMonster<T> actor)
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
                        actor.StateMachine.ChangeState(actor.States[(int)State.Attack]);
                        break;
                    }
                }
            }
        }

        public override void Exit(BaseMonster<T> actor)
        {
        }
    }

    public class Attack<T> : State<BaseMonster<T>>
    {
        private float timer;
        private float currentToPlayerDistance;
        private float currentToOriginDistance;

        public override void Enter(BaseMonster<T> actor)
        {
            timer = 0f;
        }

        public override void Excute(BaseMonster<T> actor)
        {
            currentToOriginDistance = Vector3.Distance(actor.transform.position, actor.OriginPosition);
            currentToPlayerDistance = Vector3.Distance(actor.transform.position, actor.Player.transform.position);

            if(currentToOriginDistance > actor.DetectionRadius)
            {
                Debug.Log("Player So Far");
                actor.StateMachine.ChangeState(actor.States[(int)State.Return]);
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
                    actor.Animator.Play(State.Idle.ToString());
                }

                if(timer > actor.AttackDelay)
                {
                    actor.Animator.Play(State.Attack.ToString());

                    timer -= actor.AttackDelay;
                }
            }
        }

        public override void Exit(BaseMonster<T> actor)
        {
            actor.Player = null;
        }
    }

    public class Damage<T> : State<BaseMonster<T>>
    {
        public override void Enter(BaseMonster<T> actor)
        {
            actor.Animator.Play(State.Damage.ToString());
        }

        public override void Excute(BaseMonster<T> actor)
        {
            
        }

        public override void Exit(BaseMonster<T> actor)
        {
        }
    }

    public class Return<T> : State<BaseMonster<T>>
    {
        float currentToOriginDistance;

        public override void Enter(BaseMonster<T> actor)
        {
            actor.Animator.Play(State.Idle.ToString());
        }

        public override void Excute(BaseMonster<T> actor)
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
                actor.StateMachine.ChangeState(actor.States[(int)State.Idle]);
            }
        }

        public override void Exit(BaseMonster<T> actor)
        {
        }
    }
}
