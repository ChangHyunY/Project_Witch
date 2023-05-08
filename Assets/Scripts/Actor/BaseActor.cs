using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Witch.Actor
{
    public abstract class BaseActor : MonoBehaviour
    {
        private static int s_NextValidID = 0;

        private int m_ID;
        public int ID
        {
            set
            {
                m_ID = value;
                s_NextValidID++;
            }
            get => m_ID;
        }

        public virtual void Setup()
        {
            ID = s_NextValidID;
        }

        protected abstract void Updated();
    }
}