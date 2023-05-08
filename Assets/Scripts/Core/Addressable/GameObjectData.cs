namespace Anchor.Unity.Addressables
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public class GameObjectData
    {
        private AsyncOperationHandle m_Handle;
        private GameObject m_Asset;
        private ManageType m_ManageType;
        private bool m_Poolable = false;

        public GameObject Asset => m_Asset;

        public ManageType ManageType => m_ManageType;

        public bool Poolable
        {
            get => m_Poolable;
            set => m_Poolable = value;
        }

        public void SetData(AsyncOperationHandle handle, GameObject asset, ManageType manageType)
        {
            m_Handle = handle;
            m_Asset = asset;
            m_ManageType = manageType;
        }
    }
}