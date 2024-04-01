using UnityEngine;
using Zenject;

namespace ZenjectName
{
    public partial class LocationInstaller : MonoInstaller, IStatsHero
    {
       [SerializeField] private Transform startPoint;
       [SerializeField] private BindBullet bindBullet;
       [SerializeField] private SettingsWarrior flamethrower;
       [SerializeField] private SettingsWarrior gunner;

        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindBulletHero();

            if (IStatsHero.activeHero)
            {
                BindFlamethrower();
            }

            else
            {
                BindGunner();
            }
        }

        private void BindFlamethrower()
        {
            MoveAbility _moveAbility = Container
          .InstantiatePrefabForComponent<MoveAbility>(flamethrower.hero, startPoint.position, Quaternion.identity, null);

            Container
                .Bind<MoveAbility>()
                .FromInstance(_moveAbility)
                .AsSingle()
                .NonLazy();
        }

        private void BindGunner()
        {
            MoveAbility _moveAbility = Container
          .InstantiatePrefabForComponent<MoveAbility>(gunner.hero, startPoint.position, Quaternion.identity, null);

            Container
                .Bind<MoveAbility>()
                .FromInstance(_moveAbility)
                .AsSingle()
                .NonLazy();
        }

        private void BindBulletHero()
        {
            Container
                .Bind<BindBullet>().FromInstance(bindBullet)
                .AsSingle()
                .NonLazy();
        }

        private void BindInstallerInterfaces()
        {
            Container
                .BindInterfacesTo<LocationInstaller>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }
    }
}