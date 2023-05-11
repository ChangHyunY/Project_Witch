using Anchor.Unity.Addressables;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Witch
{
    public class ComStage : MonoBehaviour
    {
        public static ComStage Root;

        [SerializeField] private AssetReference m_PlayerRef;
        private string m_PlayerPath;

        public string PlayerPath
        {
            get => m_PlayerPath;
        }

        private void Awake()
        {
            Root = this;
            m_PlayerPath = ResourceManager.GetAddressablePath(m_PlayerRef);
        }
    }
}