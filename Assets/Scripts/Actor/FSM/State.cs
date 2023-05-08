
namespace Witch.Actor
{
    public abstract class State<T> where T : class
    {
        public abstract void Enter(T actor);

        public abstract void Excute(T actor);

        public abstract void Exit(T actor);
    }
}