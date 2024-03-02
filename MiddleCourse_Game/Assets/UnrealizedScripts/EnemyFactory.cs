//using UnityEngine;
//using static ZenjectName.LocationInstaller;

//namespace ZenjectName
//{
//    public class EnemyFactory : IEnemyFactory
//    {
//        //private const string _zombi = "Zombi";
//        //private const string _zombiWarrior = "ZombiWarrior";
//        private const string _zombiTestZenject = "ZombiTestZenject";
//        //private Object _zombiEnemyPrefab;
//        //private Object _zombiWarriorEnemyPrefab;
//        private Object _zombiTestZenjectPrefab;

//        public void Load()
//        {
//            //_zombiEnemyPrefab = Resources.Load(_zombi);
//            //_zombiWarriorEnemyPrefab = Resources.Load(_zombiWarrior);
//            _zombiTestZenjectPrefab = Resources.Load(_zombiTestZenject);
//        }

//        public void Create(EnemyType enemyType, Vector3 at)
//        {
//            Object prefab = null;
//            switch (enemyType)
//            {
//                //case EnemyType.Zombi:
//                //    prefab = _zombiEnemyPrefab;
//                //    break;
//                //case EnemyType.ZombiWarrior:
//                //    prefab = _zombiWarriorEnemyPrefab;
//                //    break;
//                case EnemyType.ZombiTestZenject:
//                    prefab = _zombiTestZenjectPrefab;
//                    break;
//            }

//            if (prefab != null)
//            {
//                var newEnemy = GameObject.Instantiate(prefab, at, Quaternion.identity);
//            }
//            else return;
//        }
//    }
//}
