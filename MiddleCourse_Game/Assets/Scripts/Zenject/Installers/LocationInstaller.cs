using UnityEngine;
using Zenject;

namespace ZenjectName
{
    public partial class LocationInstaller : MonoInstaller/*, IInitializable*/
    {
       [SerializeField] private Transform _startPoint;
       [SerializeField] private GameObject _heroPrefab;
       [SerializeField] private BindBullet _bindBullet;
       //[SerializeField] private EnemyMarker[] _enemyMarker;

        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindBulletHero();
            BindHero();
            //BindEnemyFactory();
        }

        private void BindHero()
        {
            MoveAbility _moveAbility = Container
                .InstantiatePrefabForComponent<MoveAbility>(_heroPrefab, _startPoint.position, Quaternion.identity, null);

            Container
                .Bind<MoveAbility>()
                .FromInstance(_moveAbility)
                .AsSingle()
                .NonLazy();
        }

        private void BindBulletHero()
        {
            Container
                .Bind<BindBullet>().FromInstance(_bindBullet)
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