namespace Anchor.Core.Pool
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PoolManager<T> : IPool<T> where T : Component
    {
        private const short k_Capacity = 16;

        private static bool s_Initialized = false;

        private static Dictionary<string, Queue<T>> s_Pools;

        public static void Initalize()
        {
            s_Pools = new Dictionary<string, Queue<T>>();

            s_Initialized = true;
        }

        public static void Create(string name, T obj, short count = 1)
        {
            Create(name, obj, null, count);
        }

        internal static void Create(string name, T obj, Transform parent = null, short count = 1)
        {
            if(!s_Initialized)
            {
                Initalize();
            }

            if(s_Pools.ContainsKey(name))
            {
                for(int i = 0; i < count; ++i)
                {
                    s_Pools[name].Enqueue(GameObject.Instantiate(obj, parent));
                }
            }
            else
            {
                Queue<T> pools = new Queue<T>(k_Capacity);

                for(int i = 0; i < count; ++i)
                {
                    pools.Enqueue(GameObject.Instantiate(obj, parent));
                }

                s_Pools.Add(name, pools);
            }
        }

        public T Get(string name)
        {
            if(s_Pools.ContainsKey(name))
            {
                return s_Pools[name].Dequeue();
            }

            throw new System.NotImplementedException();
        }

        public void ReturnObject(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}