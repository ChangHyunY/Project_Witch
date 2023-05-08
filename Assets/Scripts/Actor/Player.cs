using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor
{
    public enum PlayerState { Idle, Walk, Run, Attack01 }

    public struct PlayerStatus
    {
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
    }

    public class Player : Actor<Player>
    {
        private PlayerStatus m_Status;

        public override void Setup()
        {
            base.Setup();

            m_Status = new PlayerStatus()
            {
                HP = 100,
                ATK = 10,
                DEF = 0,
            };

            m_States = new State<Player>[System.Enum.GetValues(typeof(PlayerState)).Length];
            m_States[(int)PlayerState.Idle] = new Idle();
            m_States[(int)PlayerState.Walk] = new Walk();
            m_States[(int)PlayerState.Run] = new Run();
            m_States[(int)PlayerState.Attack01] = new Attack();

            m_StateMachine = new StateMachine<Player>();
            m_StateMachine.Setup(this, m_States[(int)PlayerState.Idle]);
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