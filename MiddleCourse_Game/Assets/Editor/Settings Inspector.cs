using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SettingsWarrior))]
public class SettingsInspector : Editor
{
    private SerializedProperty Health;
    private bool setHealth;

    private void OnEnable()
    {
        Health = serializedObject.FindProperty("health");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(Health);
        GUILayout.Label(Health.floatValue.ToString());
        setHealth = GUILayout.Button("God's Treatment");
        if (setHealth)
        {
            Health.floatValue = 1f;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
