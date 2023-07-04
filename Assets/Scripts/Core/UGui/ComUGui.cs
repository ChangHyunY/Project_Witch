namespace Anchor.Unity.UGui
{
    using UnityEngine;

    public enum Sibling
    {
        None,
        First,
        Last
    }

    public enum ManageType
    {
        Defalut,
        Pool
    }

    public enum UGuiId
    {
        Panel,
        Dialog,
    }

    public abstract class ComUGui : MonoBehaviour
    {
        [SerializeField] protected UGuiId m_UGuiType = UGuiId.Panel;
        [SerializeField] protected Sibling m_SiblingOnOpen = Sibling.None;
        [SerializeField] protected ManageType m_ManageType = ManageType.Defalut;
        [SerializeField] protected bool m_AwakeOnOpen = false;

        protected bool m_Opened;
        protected bool m_OpenCalled;

        public UGuiId UGuiType => m_UGuiType;
        public ManageType ManageType => m_ManageType;

        public bool AwakeOnOpen => m_AwakeOnOpen;
        public bool Opened => m_Opened;

        protected abstract void Awake();

        protected abstract void OnDestroy();

        protected abstract void OnClose();
        protected abstract void OnOpen();
        protected abstract void OnInit();

        public abstract int GetID();

        protected abstract void OnSetData(System.EventArgs args);
        protected abstract void OnSetBenText(string[] text);
    }
}