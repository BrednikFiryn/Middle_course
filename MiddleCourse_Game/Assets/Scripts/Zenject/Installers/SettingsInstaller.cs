using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    private int _index = 0;
    public EnemySettings[] gameSettings;

    public override void InstallBindings()
    {
        BindInterface();
    }

    private void BindInterface()
    {
        Container
            .BindInterfacesAndSelfTo<EnemySettings>()
            .FromInstance(gameSettings[_index])
            .AsSingle();
    }

    public void IndexCount()
    {
        if (_index == 0)
        {
            _index++;
        }

        else if (_index == 1)
        {
            _index--;
        }
    }

    public void Dummy()
    {
       _index = 0;
    }
}