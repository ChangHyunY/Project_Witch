using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor.Monster
{
    public class ComSlimeKing : BaseMonster<ComSlimeKing>
    {
        public override void Setup()
        {
            base.Setup();

            m_Status = new Status()
            {
                HP = 300,
                ATK = 15,
                DEF = 0,
                SPEED = 1.5f,
            };
        }

        private void Update()
        {
            base.Updated();
        }
    }
}