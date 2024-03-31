using UnityEditor;
using UnityEngine;

public class SettingsWindow : EditorWindow
{
    private string[] settingsList;

    [MenuItem("Window/Game Settings Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SettingsWindow));
    }

    private void OnGUI()
    {
        settingsList = AssetDatabase.FindAssets("t:settings");
        GUILayout.Label("Game Settings", EditorStyles.boldLabel);
        GUILayout.Space(10);
        GUILayout.Label(settingsList?.Length.ToString(), EditorStyles.label);
    }
}
