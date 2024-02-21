//using Unity.Entities;
//using Unity.Transforms;
//using UnityEngine;

///// <summary>
///// ����������� ����� DestroySystem, ������� ����������� �� ComponentSystem. ��� ��������, ��� ����� ����� �������������� ��� �������� ������� ���������� �����������.
///// </summary>
//public class DestroySystem : ComponentSystem
//{
//    /// <summary>
//    /// ���� ����� ���������������� �� �������� ������ ComponentSystem � ���������� ������ ���� ��� ���������� �������.
//    /// </summary>
//    protected override void OnUpdate()
//    {
//        //Entities.WithAll<BulletBehaviour>(): ��� ����� ������ WithAll, ������� ��������� ������ �� ��������, ������� ����� ��������� BulletBehaviour.
//        //ForEach((Entity entity, ref Translation translation) => {: ����������� ���� �������� ���� ��������� ���������.
//        //� ���������� ����������� ��� �������� (Entity) � ������ �� ��������� Translation, ����� ����� ������ � ��� ������.
//        Entities.WithAll<BulletBehaviour>().ForEach((Entity entity, ref Translation translation) =>
//        {
//            // �������� gameObject ��������� � ���� ���������
//            GameObject gameObject = EntityManager.GetComponentObject<GameObject>(entity);
//            // ��������� ��������� �� ������ ���� ���, ���������� ��������.
//            if (gameObject == null)
//            {
//                Debug.Log("gameObject == null");
//                // ���� ����� ���������� ��������. �� ���������� PostUpdateCommands ��� ���������� ������ ����� ���������� �������, ����� �������� ����������.
//                PostUpdateCommands.DestroyEntity(entity);
//            }
//        });
//    }
//}