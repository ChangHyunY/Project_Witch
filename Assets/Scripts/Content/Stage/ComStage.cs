using System.Collections;
using System.Collections.Generic;
using Anchor;
using Anchor.Unity.Addressables;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Witch
{
    public class ComStage : MonoBehaviour
    {
        public static ComStage Root;

        public AssetReference m_PlayerRef;
        private Transform m_Player;


        private void Awake()
        {
            string player = ResourceManager.GetAddressablePath(m_PlayerRef);
            m_Player = ResourceHelper.GameObjectBags[(int)GameObjectBagId.Normal].Get<Transform>(player);
        }
    }
}