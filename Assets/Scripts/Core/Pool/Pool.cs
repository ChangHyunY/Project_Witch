namespace Anchor.Core.Pool
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Pool<T> : IDisposable
    {
        private Queue<T> m_InactiveObjects;
        private List<T> m_ActiveObjects;
        private Func<T> m_ObjectGenerator;
        private int m_MaxPoolSize;

        private int m_ActiveCount = 0;

        public Pool(Func<T> objectGenerator, int initGenerateCount, int initalCapacity, int maxPoolSize = 0)
        {
            m_ObjectGenerator = objectGenerator;
            m_MaxPoolSize = maxPoolSize;

            if (m_MaxPoolSize > 0)
            {
                if (initGenerateCount > m_MaxPoolSize)
                {
                    initGenerateCount = m_MaxPoolSize;
                }

                if (initalCapacity > m_MaxPoolSize)
                {
                    initalCapacity = m_MaxPoolSize;
                }
                else if (initalCapacity < initGenerateCount)
                {
                    initalCapacity = initGenerateCount;
                }
            }

            m_InactiveObjects = new Queue<T>(initalCapacity);
            m_ActiveObjects = new List<T>(initalCapacity);

            for(int i = 0; i < initGenerateCount; ++i)
            {
                m_InactiveObjects.Enqueue(objectGenerator());
            }
        }

        ~Pool()
        {
            Clear();
        }

        public T Get()
        {
            T item = default(T);

            if(m_InactiveObjects.Count > 0)
            {
                item = m_InactiveObjects.Dequeue();
            }
            else
            {
                if(m_MaxPoolSize > 0 && m_MaxPoolSize <= m_ActiveCount)
                {
                    throw new Exception("pool is fulled");
                }

                item = m_ObjectGenerator();
            }

            m_ActiveObjects.Add(item);
            m_ActiveCount++;

            return item;
        }

        public void Return(T item)
        {
            if(!m_ActiveObjects.Contains(item))
            {
                throw new Exception("not found item in active object queue");
            }

            if(m_InactiveObjects.Contains(item))
            {
                throw new Exception("found item in inactive object list");
            }

            m_ActiveObjects.Remove(item);
            m_ActiveCount--;

            m_InactiveObjects.Enqueue(item);
        }

        public void ReturnAll()
        {
            for(int i = 0, loop = m_ActiveObjects.Count; i < loop; ++i)
            {
                Return(m_ActiveObjects[i]);
            }
        }

        public void Clear()
        {
            foreach(var item in m_ActiveObjects)
            {
                m_InactiveObjects.Enqueue(item);
            }

            m_ActiveObjects.Clear();
            m_ActiveCount = 0;
        }

        public void Dispose()
        {
            Clear();
        }
    }
}