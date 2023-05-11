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

    public class BaseMonster : Actor<BaseMonster>
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

        public Vector3 Derection
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
        }

        protected override void Updated()
        {
            base.Updated();

            m_StateMachine.Excute();
        }
    }
}