using UnityEditor;
using UnityEngine;

public class SettingsWindow : EditorWindow, IStatsHero
{
    private string[] _heroSettingsList;
    private bool _flamethrower;
    private bool _gunner;

    [MenuItem("Window/Game Settings Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SettingsWindow));
    }

    private void OnGUI()
    {
        _heroSettingsList = AssetDatabase.FindAssets("t:SettingsWarrior");
        GUILayout.Label("Game Settings", EditorStyles.boldLabel);
        GUILayout.Space(10);

        foreach (var file in _heroSettingsList)
        {
            GUILayout.Label(AssetDatabase.GUIDToAssetPath(file), EditorStyles.label);
        }

        _flamethrower = GUILayout.Button("Flamethrower");
        _gunner = GUILayout.Button("Gunner");

        if (_flamethrower)
        {
            IStatsHero.activeHero = true;
            Debug.Log(IStatsHero.activeHero);
        }

        if (_gunner)
        {
            IStatsHero.activeHero = false;
            Debug.Log(IStatsHero.activeHero);
        }
    }
}