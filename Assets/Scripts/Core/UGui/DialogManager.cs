namespace Anchor.Unity.UGui
{
    using Anchor.Unity.UGui.Dialog;
    using Anchor.Unity.UGui.Panel;
    using Anchor.Core.Pool;
    using System.Collections.Generic;


    public static class DialogManager
    {
        private static bool s_InitCalled = false;

        private static Dictionary<UGuiId, List<UGuiInfo>> m_Values;
        private static Stack<IPanel> m_NavigateValues;

        public static bool InitCalled => s_InitCalled;

        public static void Initialize()
        {
            if (s_InitCalled) return;

            s_InitCalled = true;

            m_Values = new Dictionary<UGuiId, List<UGuiInfo>>()
            {
                { UGuiId.Panel, new List<UGuiInfo>() },
                { UGuiId.Dialog, new List<UGuiInfo>() },
            };

            m_NavigateValues = new Stack<IPanel>();
        }

        public static void Add(UGuiId id, ComUGui uGui)
        {
            if (!s_InitCalled) Initialize();

            uGui.gameObject.SetActive(false);

            if (!m_Values[id].Exists(element => element.Component.GetID() == uGui.GetID()))
            {
                if (uGui.ManageType == ManageType.Pool)
                {
                    //TODO
                    //ObjectPool<ComDialog> pool = new ObjectPool<ComDialog>(dialog, 8, dialog.transform.parent);
                    Pool<ComUGui> pool = new Pool<ComUGui>(null, 8, 16, 16);

                    m_Values[id].Add(new UGuiInfo(uGui, pool));
                }
                else
                {
                    m_Values[id].Add(new UGuiInfo(uGui));
                }
            }
            else
            {
                return;
            }
        }

        public static void Remove(ComUGui uGui)
        {
            uGui.gameObject.SetActive(false);

            var idx = m_Values[uGui.UGuiType].FindIndex(element => element.Component.GetID() == uGui.GetID());

            if (idx >= 0)
            {
                m_Values[uGui.UGuiType].RemoveAt(idx);
            }
        }

        public static void ReturnToPool(ComUGui uGui)
        {
            if (uGui.ManageType == ManageType.Pool)
            {
                var idx = m_Values[uGui.UGuiType].FindIndex(element => element.Component.GetID() == uGui.GetID());

                if(idx > 0)
                {
                    m_Values[uGui.UGuiType][uGui.GetID()].Pool.Return(uGui);
                }
            }
            else
            {
                uGui.gameObject.SetActive(false);
            }
        }

        public static void Open<T>(ComPanel<T> panel) where T : ComPanel<T>
        {
            if(panel != null)
            {
                panel.Open();
            }

            if (panel.Navigated)
            {
                m_NavigateValues.Push(panel);
            }
        }

        public static void DisPlay(int id, System.EventArgs dataArgs, string[] btnText,
            System.Action<int, System.EventArgs> clickCallback = null, System.EventArgs callbackArgs = null)
        {
            var com = GetInternal(id);

            if (com != null)
            {
                com.SetData(dataArgs, btnText, clickCallback, callbackArgs);
                com.Open();
            }
        }

        private static ComDialog GetInternal(int id)
        {
            ComDialog com = null;

            var idx = m_Values[UGuiId.Dialog].FindIndex(element => element.Component.GetID() == id);

            if (idx >= 0)
            {
                if (m_Values[UGuiId.Dialog][idx].Pool != null)
                {
                    com = m_Values[UGuiId.Dialog][idx].Pool.Get().GetComponent<ComDialog>();
                }
                else
                {
                    com = m_Values[UGuiId.Dialog][idx].Component as ComDialog;

                    if (com.Opened)
                    {
                        com.Close();
                    }
                }
            }

            return com;
        }

        public static void CloseFromNavigation()
        {
            if (m_NavigateValues.Count <= 0) return;

            IPanel panel = m_NavigateValues.Pop();
            panel.Close();
        }
    }
}