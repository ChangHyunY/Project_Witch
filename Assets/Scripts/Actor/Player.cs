using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor
{
    public enum PlayerState { Idle, Walk, Jump, Attack01 }

    public struct PlayerStatus
    {
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public float MoveSpeed { get; set; }
    }

    public class Player : Actor<Player>
    {
        private Rigidbody2D m_RigidBody2D;
        private Collider2D[] m_Collider2Ds;

        public Rigidbody2D RigidBody2D => m_RigidBody2D;
        public Collider2D[] Collider2Ds => m_Collider2Ds;


        private PlayerStatus m_Status;

        public PlayerStatus Status => m_Status;
        
        public Vector2 Direction { get; set; }

        public override void Setup()
        {
            base.Setup();

            m_RigidBody2D = GetComponent<Rigidbody2D>();
            m_Collider2Ds = GetComponentsInChildren<Collider2D>();

            m_Status = new PlayerStatus()
            {
                HP = 100,
                ATK = 10,
                DEF = 0,
                MoveSpeed = 5.0f,
            };

            m_States = new State<Player>[System.Enum.GetValues(typeof(PlayerState)).Length];
            m_States[(int)PlayerState.Idle] = new Idle();
            m_States[(int)PlayerState.Walk] = new Walk();
            m_States[(int)PlayerState.Jump] = new Jump();
            m_States[(int)PlayerState.Attack01] = new Attack();

            m_StateMachine = new StateMachine<Player>();
            m_StateMachine.Setup(this, m_States[(int)PlayerState.Idle]);
        }

        private void Update()
        {
            Updated();
        }

        protected override void Updated()
        {
            m_StateMachine.Excute();
        }

        public void ChangeState(PlayerState newState)
        {
            m_StateMachine.ChangeState(m_States[(int)newState]);
        }
    }
}