namespace Assets.ECS_2.interfaces
{
    public interface IBehaviour
    {
        public static float damage = 0.1f;

        float Evaluate();
        void Behave();
    }
}