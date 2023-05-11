using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor.Monster
{
    public enum State { Idle, Attack, Damage, Return }

    [System.Serializable]
    public struct Status
    {
        public int HP;
        public int ATK;
        public int DEF;
        public float SPEED;
    }

    public class BaseMonster<T> : Actor<BaseMonster<T>>
    {
        [SerializeField] protected Status m_Status;

        public Status Status
        {
            get => m_Status;
        }

        protected ComPlayer m_Player;

        public ComPlayer Player
        {
            get => m_Player;
            set => m_Player = value;
        }

        private Vector3 m_OriginPosition;
        private Vector3 m_Direction;
        [SerializeField] protected float m_DetectionRadius;
        [SerializeField] private float m_AttackDelay;
        [SerializeField] private float m_AttackRange;

        public Vector3 OriginPosition
        {
            get => m_OriginPosition;
        }

        public Vector3 Direction
        {
            get => m_Direction;
            set => m_Direction = value;
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

            m_States = new State<BaseMonster<T>>[System.Enum.GetValues(typeof(State)).Length];
            m_States[(int)State.Idle] = new Idle<T>();
            m_States[(int)State.Attack] = new Attack<T>();
            m_States[(int)State.Damage] = new Damage<T>();
            m_States[(int)State.Return] = new Return<T>();

            m_StateMachine = new StateMachine<BaseMonster<T>>();
            m_StateMachine.Setup(this, m_States[(int)State.Idle]);

            m_OriginPosition = transform.position;
        }

        protected override void Updated()
        {
            base.Updated();

            m_StateMachine.Excute();
        }
    }
}