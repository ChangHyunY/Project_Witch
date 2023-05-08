using UnityEngine;
using UnityEngine.InputSystem;
using Anchor.Unity.UGui.Dialog;

namespace Anchor
{
    public class ComMain : MonoBehaviour
    {
        public static ComMain Root;

        [SerializeField] UnityEngine.EventSystems.EventSystem s_EventSystem;

        private void Awake()
        {
            Root = this;

            DontDestroyOnLoad(this);
            DontDestroyOnLoad(s_EventSystem);

            ProjectSetting.Initalize();
        }

        private void Start()
        {
            ResourceHelper.LoadScene(SceneId.Start);
        }

        private void Update()
        {
            if(Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                DialogManager.CloseFromNavigation();
            }
        }

        public void DebugMessage(string msg)
        {
            Debug.Log(msg);
        }
    }
}