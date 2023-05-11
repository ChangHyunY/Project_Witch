using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor.Monster
{
    public class ComSlime : BaseMonster<ComSlime>
    {
        public override void Setup()
        {
            base.Setup();

            m_Status = new Status()
            {
                HP = 30,
                ATK = 5,
                DEF = 0,
                SPEED = 3f,
            };
        }

        private void Update()
        {
            base.Updated();
        }
    }
}