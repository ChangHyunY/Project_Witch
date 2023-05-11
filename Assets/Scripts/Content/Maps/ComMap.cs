using Anchor;
using Anchor.Unity.Addressables;
using UnityEngine;

namespace Witch
{
    public class ComMap : MonoBehaviour
    {
        public static ComMap Root;

        private void Awake()
        {
            Root = this;

            ResourceHelper.GameObjectBags[(int)GameObjectBagId.Normal].Get<Transform>(ComStage.Root.PlayerPath);
        }
    }
}