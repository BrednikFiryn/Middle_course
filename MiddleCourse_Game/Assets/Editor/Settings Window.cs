using UnityEditor;
using UnityEngine;

public class SettingsWindow : EditorWindow, IStatsHero
{
    private string[] _heroSettingsList;
    private bool _flamethrowerGame;
    private bool _gunnerGame;

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

        _flamethrowerGame = GUILayout.Button("Activates the Flamethrower in the game");
        _gunnerGame = GUILayout.Button("Activates the Gunner in the game");
        if (_flamethrowerGame)
        {
            IStatsHero.activeHero = true;
            Debug.Log("Activates the Flamethrower in the game");
        }

        if (_gunnerGame)
        {
            IStatsHero.activeHero = false;
            Debug.Log("Activates the Gunner in the game");
        }
    }
}