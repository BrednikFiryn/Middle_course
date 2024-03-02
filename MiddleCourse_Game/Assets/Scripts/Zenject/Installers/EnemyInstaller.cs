using Zenject;

namespace GreetingName
{
    public partial class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [Inject]
        EnemySettings settings;

        public override void InstallBindings()
        {
            BindFactory();
        }

        private void BindFactory()
        {
            Container
                .BindFactory<EnemyConsumer, EnemyConsumer.Factory>()
                .FromComponentInNewPrefab(settings.greetingConsumerPrefab);
        }
    }
}