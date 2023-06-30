namespace Anchor.Unity.UGui.Dialog
{
    using UnityEngine;

    public abstract class ComDialog : ComUGui
    {
        [SerializeField] DialogId m_Id;

        public int Id => (int)m_Id;

        protected override void Awake()
        {
            DialogManager.Add(UGuiId.Dialog, this);
        }

        protected override void OnDestroy()
        {
            DialogManager.Remove(this);
        }

        public override int GetID()
        {
            return (int)m_Id;
        }

        public void Open()
        {
            if (m_Opened) return;

            m_Opened = true;

            gameObject.SetActive(true);

            if (m_SiblingOnOpen == Sibling.First)
            {
                gameObject.transform.SetAsFirstSibling();
            }
            else if (m_SiblingOnOpen == Sibling.Last)
            {
                gameObject.transform.SetAsLastSibling();
            }

            if (!m_OpenCalled)
            {
                m_OpenCalled = true;
                OnInit();
            }

            OnOpen();
        }

        public void Close()
        {
            OnClose();

            m_Opened = false;
            DialogManager.ReturnToPool(this);
        }

        internal void SetData(System.EventArgs dataArgs, string[] btnText,
            System.Action<int, System.EventArgs> cliclCallback = null, System.EventArgs callbackArgs = null)
        {
            OnSetData(dataArgs);
        }
    }
}