using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor
{
    public class Actor<T> : BaseActor where T : class
    {
        protected State<T>[] m_States;
        protected StateMachine<T> m_StateMachine;

        public State<T>[] States
        {
            get => m_States;
        }

        public StateMachine<T> StateMachine
        {
            get => m_StateMachine;
        }

        private Animator m_Animator;

        public Animator Animator => m_Animator;

        public override void Setup()
        {
            base.Setup();

            m_Animator = GetComponent<Animator>();
        }

        protected override void Updated()
        {

        }
    }
}