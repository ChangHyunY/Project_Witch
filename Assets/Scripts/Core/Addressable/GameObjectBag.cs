namespace Anchor.Unity.Addressables
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public enum ManageType
    {
        Default,
        Pool,
    }

    public class GameObjectBag
    {
        private int m_Id;
        private Dictionary<string, GameObjectData> m_Items = new Dictionary<string, GameObjectData>();

        private string m_RootGameObjectName = null;
        private GameObject m_RootGameObject = null;
        private bool m_RootGameOBjectDontDestroy;

        public int Id => m_Id;

        public GameObjectBag(int id, string rootGameObjectName, bool rootGameObjectDontDestroy)
        {
            m_Id = id;
            m_RootGameObjectName = rootGameObjectName;
            m_RootGameOBjectDontDestroy = rootGameObjectDontDestroy;
        }

        public bool IsLoaded(string addressable)
        {
            return m_Items.ContainsKey(addressable);
        }

        public void Load(string addressable, ManageType manageType, System.Action<bool> callback = null)
        {
            Anchor.ComMain.Root.StartCoroutine(CoLoad(addressable, manageType, callback));
        }

        public IEnumerator CoLoad(string addressable, ManageType manageType, System.Action<bool> callback = null)
        {
            if(IsLoaded(addressable))
            {
                callback?.Invoke(true);
                yield break;
            }

            var handle = Addressables.LoadAssetAsync<GameObject>(addressable);

            yield return handle;

            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObjectData pData = null;

                if(!m_Items.TryGetValue(addressable, out pData))
                {
                    var asset = handle.Result as GameObject;

                    pData = new GameObjectData();
                    pData.SetData(handle, asset, manageType);
                    m_Items.Add(addressable, pData);
                }
                else
                {
                    m_Items.Add(addressable, pData);
                    callback?.Invoke(true);
                }
            }
            else
            {
                if(handle.IsValid())
                {
                    Addressables.Release(handle);
                }

                callback?.Invoke(false);
            }
        }

        //public T Get<T>(AssetReferenceGameObject gameObjectRef) where T : Component
        //{
        //    return this.Get<T>(ResourceManager)
        //}

        public T Get<T>(string addressable) where T : Component
        {
            if(m_Items.TryGetValue(addressable, out GameObjectData bagData))
            {
                if(bagData.ManageType == ManageType.Default)
                {
                    var cloneGO = GameObject.Instantiate(bagData.Asset) as GameObject;

                    return cloneGO.GetComponent<T>();
                }
                else
                {
                    if(!bagData.Poolable)
                    {
                        var asset = bagData.Asset;

                        if(m_RootGameObject == null)
                        {
                            m_RootGameObject = new GameObject(m_RootGameObjectName);
                        }

                        bagData.Poolable = true;
                    }
                }
            }

            throw new System.Exception("Asset isn't loaded");
        }
    }
}