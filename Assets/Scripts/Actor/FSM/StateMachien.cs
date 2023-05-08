using Anchor;

namespace Witch.Actor
{
    public class StateMachine<T> where T : class
    {
        private T m_Actor;
        private State<T> m_CurrentState;
        private State<T> m_PreviousState;
        private State<T> m_GlobalState;

        public void Setup(T owner, State<T> entryState)
        {
            m_Actor = owner;
            m_CurrentState = null;
            m_PreviousState = null;
            m_GlobalState = null;

            ChangeState(entryState);
        }

        public void Excute()
        {
            m_GlobalState?.Excute(m_Actor);
            m_CurrentState?.Excute(m_Actor);
        }

        public void ChangeState(State<T> newState)
        {
            if (newState == null) return;

            if(m_CurrentState != null)
            {
                m_PreviousState = m_CurrentState;
                m_CurrentState.Exit(m_Actor);
            }

            m_CurrentState = newState;
            m_CurrentState.Enter(m_Actor);
        }

        public void SetGlobalState(State<T> newState)
        {
            m_GlobalState = newState;
        }

        public void ReverToPreviousState()
        {
            ComMain.Root.DebugMessage($"RevertToPreviousState, {m_PreviousState}");
            ChangeState(m_PreviousState);
        }
    }
}