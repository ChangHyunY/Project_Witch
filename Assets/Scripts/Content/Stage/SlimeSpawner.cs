using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anchor;
using Anchor.Unity.Addressables;
using Witch.Actor.Monster;
using UnityEngine.AddressableAssets;

namespace Witch
{
    public class SlimeSpawner : MonoBehaviour, ISpawner
    {
        public AssetReference m_Slime;
        private Queue<ComSlime> m_Slimes;

        //private short m_Max = 10;
        private short m_Max = 1;
        private short m_Count = 0;

        private void Awake()
        {
            m_Slimes = new Queue<ComSlime>(m_Max);

            for(int i = 0; i < m_Max; ++i)
            {
                string assetPath = ResourceManager.GetAddressablePath(m_Slime);
                var slime = ResourceHelper.GameObjectBags[(int)GameObjectBagId.Normal].Get<ComSlime>(assetPath);
                m_Slimes.Enqueue(slime);
                slime.transform.SetParent(this.transform);
                slime.transform.position = RandomPointOnCircle(this.transform.position, 5f);
                slime.Setup();
                ++m_Count;
            }
        }

        private void Update()
        {
            Spawn();
        }

        public void Spawn()
        {
            if(m_Count < m_Max)
            {

            }
        }

        private Vector3 RandomPointOnCircle(Vector3 center, float radius)
        {
            float angle = Random.Range(0f, 360f);
            float radian = angle * Mathf.Deg2Rad;

            float x = center.x + Random.Range(0f, radius) * Mathf.Cos(radian);
            float z = center.z + Random.Range(0f, radius) * Mathf.Sin(radian);
            float y = center.y;

            return new Vector3(x, y, z);
        }
    }
}