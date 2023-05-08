using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor.Monster
{
    public enum SlimeState { Idle, Attack, Damage, Return }

    [System.Serializable]
    public struct SlimeStatus
    {
        public int HP;
        public int ATK;
        public int DEF;
        public float SPEED;
    }

    public class ComSlime : Actor<ComSlime>
    {
        [SerializeField] private SlimeStatus m_Status;

        public SlimeStatus Status
        {
            get => m_Status;
        }

        private ComPlayer m_Player;

        public ComPlayer Player
        {
            get => m_Player;
            set => m_Player = value;
        }

        private Vector3 m_OriginPosition;
        private float m_DetectionRadius = 10f;
        [SerializeField] private float m_AttackDelay = 1f;
        [SerializeField] private float m_AttackRange = 1f;

        public Vector3 OriginPosition
        {
            get => m_OriginPosition;
        }

        public float DetectionRadius
        {
            get => m_DetectionRadius;
        }

        public float AttackDelay
        {
            get => m_AttackDelay;
        }

        public float AttackRange
        {
            get => m_AttackRange;
        }

        public override void Setup()
        {
            base.Setup();

            m_Status = new SlimeStatus()
            {
                HP = 30,
                ATK = 5,
                DEF = 0,
                SPEED = 1f,
            };

            m_States = new State<ComSlime>[System.Enum.GetValues(typeof(SlimeState)).Length];
            m_States[(int)SlimeState.Idle] = new Monster.Idle();
            m_States[(int)SlimeState.Attack] = new Monster.Attack();
            m_States[(int)SlimeState.Damage] = new Monster.Damage();
            m_States[(int)SlimeState.Return] = new Monster.Return();

            m_StateMachine = new StateMachine<ComSlime>();
            m_StateMachine.Setup(this, m_States[(int)SlimeState.Idle]);

            m_OriginPosition = transform.position;
        }

        private void Update()
        {
            base.Updated();

            m_StateMachine.Excute();
            //OnSearch();
        }

        private void OnSearch()
        {
            if (m_Player == null)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, m_DetectionRadius);

                foreach (var collider in colliders)
                {
                    if (collider.CompareTag("Player"))
                    {
                        Debug.Log($"Player Detected, Length : {Vector3.Distance(collider.transform.position, transform.position)}");
                        m_Player = collider.GetComponent<ComPlayer>();
                        break;
                    }
                }
            }
            else
            {
                if (Vector3.Distance(m_Player.transform.position, m_OriginPosition) > m_DetectionRadius)
                {
                    Debug.Log($"Player so far");
                    m_Player = null;

                    //TODO
                    //ToOrigin
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_DetectionRadius);
        }
    }
}