using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anchor.Unity.Addressables;

namespace Anchor.Unity
{
    public enum SceneId
    {
        Main,
        Login,
        Start,
    }

    public class ResourceHelper
    {
        private static GameObjectBag[] s_GameObjectBags;

        public static GameObjectBag[] GameObjectBags => s_GameObjectBags;

        public static readonly string[] k_SceneNames =
        {
            "Assets/Scenes/Main.unity",
            "Assets/Scenes/Login.unity",
            "Assets/Scenes/Start.unity",
        };

        public static void Initalize()
        {
            int length = System.Enum.GetValues(typeof(GameObjectBagId)).Length;
            s_GameObjectBags = new GameObjectBag[length];

            for (int i = 0; i < length; ++i)
            {
                bool rootGameObjectDontDestroy = true;

                switch ((GameObjectBagId)i)
                {
                    case GameObjectBagId.Normal:
                        rootGameObjectDontDestroy = false;
                        break;
                }

                s_GameObjectBags[i] = new GameObjectBag(i, ((GameObjectBagId)i).ToString(), rootGameObjectDontDestroy);
            }
        }

        public static void LoadScene(SceneId scene, System.Action resultCallback = null)
        {
            System.Action<bool> callback = (success) =>
            {
                if (!success)
                {
                    Debug.LogError(success.ToString());
                }

                resultCallback?.Invoke();
            };

            switch (scene)
            {
                case SceneId.Login:
                    LoadDefaultScene(Define.LoginAssets, SceneId.Login, callback);
                    break;

                case SceneId.Start:
                    LoadDefaultScene(Define.StartAssets, SceneId.Start, callback);
                    break;
            }
        }

        private static void LoadDefaultScene(string[] keys, SceneId id, System.Action<bool> callback = null)
        {
            ComMain.Root.StartCoroutine(CoLoadDefaultScene(keys, id, callback));
        }

        private static IEnumerator CoLoadDefaultScene(string[] keys, SceneId id, System.Action<bool> callback = null)
        {
            //canvas load
            foreach (var asset in keys)
            {
                yield return s_GameObjectBags[(int)GameObjectBagId.Normal].CoLoad(asset, ManageType.Default);
            }

            //scene load
            bool success = false;

            yield return ResourceManager.CoLoadSceneAsync(k_SceneNames[(int)id], UnityEngine.SceneManagement.LoadSceneMode.Single, (result) =>
            {
                success = result;
            });

            if (!success)
            {
                Debug.LogError("scene load fail");
                yield break;
            }

            //canvas get
            foreach (var asset in keys)
            {
                yield return s_GameObjectBags[(int)GameObjectBagId.Normal].Get<RectTransform>(asset);
            }
        }
    }
}