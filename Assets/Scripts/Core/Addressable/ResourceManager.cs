namespace Anchor.Unity.Addressables
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.AddressableAssets;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class ResourceManager
    {
        private static bool s_InitCalled = false;

        private static Dictionary<string, GameObjectData> m_Values = new Dictionary<string, GameObjectData>();
        private static Dictionary<string, string> m_AssetPaths = new Dictionary<string, string>();

        public static void Initialize()
        {
            if(!s_InitCalled)
            {
                s_InitCalled = true;
            }
        }

        public static string GetAddressablePath(AssetReference assetRef)
        {
            string addressable = null;

            if(m_AssetPaths.TryGetValue(assetRef.AssetGUID, out addressable))
            {
                return addressable;
            }
            else
            {
                var opHandle = Addressables.LoadResourceLocationsAsync(assetRef);

                opHandle.WaitForCompletion();

                using (AsyncOperationDisposer locDisposer = new AsyncOperationDisposer(opHandle))
                {
                    if (opHandle.Status == AsyncOperationStatus.Succeeded &&
                        opHandle.Result != null &&
                        opHandle.Result.Count > 0)
                    {
                        m_AssetPaths.Add(assetRef.AssetGUID, opHandle.Result[0].PrimaryKey);
                        return opHandle.Result[0].PrimaryKey;
                    }
                    else
                    {
                        return null;
                    }
                }                    
            }
        }

        public static IEnumerator CoLoadSceneAsync(string addressable, UnityEngine.SceneManagement.LoadSceneMode loadMode, System.Action<bool> resultCallback)
        {
            var handle = Addressables.LoadSceneAsync(addressable, loadMode);

            yield return handle;

            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                resultCallback(true);
            }
            else
            {
                resultCallback(false);
            }
        }

        public static IEnumerator CoLoadAssetsAsync(string addressable, System.Action<bool> resultCallback)
        {


            yield return null;
        }
    }
}