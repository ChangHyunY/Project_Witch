using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Witch.Actor
{
    public class ComPlayer : Player
    {
        private bool m_IsMove;

        public bool IsMove
        {
            get => m_IsMove;
            set
            {
                m_IsMove = value;

                if (m_IsMove) ChangeState(PlayerState.Walk);
                else ChangeState(PlayerState.Idle);
            }
        }

        private void Awake()
        {
            Setup();
        }

        private void Update()
        {
            Updated();
        }

        public override void Setup()
        {
            base.Setup();
        }

        protected override void Updated()
        {
            base.Updated();
        }
    }
}