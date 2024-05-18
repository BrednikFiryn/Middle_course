namespace DefaultNamespace
{
    public interface moveAbility
    {
        void Execute(float speed);
        void Stop();
    }

    public interface IAbility
    {
        void Execute();
    }
}
