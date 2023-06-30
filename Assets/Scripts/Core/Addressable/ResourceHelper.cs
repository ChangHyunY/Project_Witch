using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anchor.Unity.Addressables;

namespace Anchor
{
    public enum SceneId
    {
        Start,
        Main,
        Stage,
        Map_01,
    }

    public class ResourceHelper
    {
        private static GameObjectBag[] s_GameObjectBags;

        public static GameObjectBag[] GameObjectBags => s_GameObjectBags;

        public static readonly string[] k_SceneNames =
        {
            "Assets/Scenes/start.unity",
            "Assets/Scenes/main.unity",
            "Assets/Scenes/stage.unity",
            "Assets/Scenes/map_01.unity",
        };

        public static void Initalize()
        {
            int length = System.Enum.GetValues(typeof(GameObjectBagId)).Length;
            s_GameObjectBags = new GameObjectBag[length];

            for(int i = 0; i < length; ++i)
            {
                bool rootGameObjectDontDestroy = true;

                switch((GameObjectBagId)i)
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

            switch(scene)
            {
                case SceneId.Start:
                    LoadStartScene(callback);
                    break;

                case SceneId.Stage:
                    LoadStageScene(callback);
                    break;
            }
        }

        private static void LoadStartScene(System.Action<bool> callback = null)
        {
            ComMain.Root.StartCoroutine(CoLoadStartAsset(callback));
        }

        private static IEnumerator CoLoadStartAsset(System.Action<bool> callback = null)
        {
            //canvas load
            var keys = Define.StartAssets;

            foreach(var asset in keys)
            {
                yield return s_GameObjectBags[(int)GameObjectBagId.Normal].CoLoad(asset, ManageType.Default);
            }

            //scene load
            bool success = false;

            yield return ResourceManager.CoLoadSceneAsync(k_SceneNames[(int)SceneId.Start], UnityEngine.SceneManagement.LoadSceneMode.Single, (result) =>
            {
                success = result;
            });

            if(!success)
            {
                Debug.LogError("scene load fail");
                yield break;
            }

            //canvas get
            foreach(var asset in keys)
            {
                yield return s_GameObjectBags[(int)GameObjectBagId.Normal].Get<RectTransform>(asset);
            }
        }

        private static void LoadStageScene(System.Action<bool> callback = null)
        {
            ComMain.Root.StartCoroutine(CoLoadStageAsset(callback));
        }

        private static IEnumerator CoLoadStageAsset(System.Action<bool> callback = null)
        {
            //assets load
            var keys = Define.StageAssets;

            foreach(var asset in keys)
            {
                yield return s_GameObjectBags[(int)GameObjectBagId.Normal].CoLoad(asset, ManageType.Default);
            }

            //scene load
            bool success = false;

            yield return ResourceManager.CoLoadSceneAsync(k_SceneNames[(int)SceneId.Stage], UnityEngine.SceneManagement.LoadSceneMode.Single, (result) =>
            {
                success = result;
            });

            if(!success)
            {
                throw new System.Exception();
            }

            //assets get
            foreach (var asset in keys)
            {
                yield return s_GameObjectBags[(int)GameObjectBagId.Normal].Get<Transform>(asset);
            }
        }
    }
}