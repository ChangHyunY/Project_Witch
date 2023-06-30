namespace Anchor.Unity.UGui
{
    using Anchor.Core.Pool;

    internal struct UGuiInfo
    {
        private ComUGui m_Component;
        private Pool<ComUGui> m_Pool;

        public ComUGui Component => m_Component;
        public Pool<ComUGui> Pool => m_Pool;

        public UGuiInfo(ComUGui component)
        {
            m_Component = component;
            m_Pool = null;
        }

        public UGuiInfo(ComUGui component, Pool<ComUGui> pool)
        {
            m_Component = component;
            m_Pool = pool;
        }
    }
}
