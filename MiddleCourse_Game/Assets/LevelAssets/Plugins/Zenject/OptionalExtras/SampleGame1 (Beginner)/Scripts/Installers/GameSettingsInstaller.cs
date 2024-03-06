using System;

namespace Zenject.Asteroids
{
    // Мы предпочитаем использовать ScriptableObjectInstaller для инсталляторов, содержащих настройки игры
    // Однако нет причин, по которым вы не могли бы использовать моноустановщик здесь вместо этого
    // использование ScriptableObjectInstaller здесь имеет преимущества, которые делают его удобным для настроек:
    //
    // 1) Вы можете изменять эти значения во время выполнения и сохранять эти изменения во время воспроизведения
    // сеансов. Если бы это был моноустановщик, то любые изменения были бы потеряны, когда вы нажмете стоп
    // 2) Вы можете легко создать несколько экземпляров ScriptableObject этого установщика для тестирования
    // различные настройки параметров. Например, у вас могут быть разные экземпляры
    // для каждого режима сложности вашей игры, такого как "Легкий", "сложный" и т.д.
    // 3) Если ваши настройки связаны с корнем композиции игрового объекта, то с помощью
    // ScriptableObjectInstaller может быть проще, поскольку всегда будет только один окончательный
    // экземпляр для каждой настройки. В противном случае вам пришлось бы изменять настройки для каждой игры
    // корень композиции объектов отдельно во время выполнения
    //
    // Раскомментируйте, если вы хотите добавить альтернативные настройки игры
    //[CreateAssetMenu(имя меню = "Астероиды/Настройки игры")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public ShipSettings Ship;
        public AsteroidSettings Asteroid;
        public AudioHandler.Settings AudioHandler;
        public GameInstaller.Settings GameInstaller;

        // Здесь мы используем вложенные классы для группировки связанных настроек вместе
        [Serializable]
        public class ShipSettings
        {
            public ShipStateMoving.Settings StateMoving;
            public ShipStateDead.Settings StateDead;
            public ShipStateWaitingToStart.Settings StateStarting;
        }

        [Serializable]
        public class AsteroidSettings
        {
            public AsteroidManager.Settings Spawner;
            public Asteroid.Settings General;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Ship.StateMoving);
            Container.BindInstance(Ship.StateDead);
            Container.BindInstance(Ship.StateStarting);
            Container.BindInstance(Asteroid.Spawner);
            Container.BindInstance(Asteroid.General);
            Container.BindInstance(AudioHandler);
            Container.BindInstance(GameInstaller);
        }
    }
}

