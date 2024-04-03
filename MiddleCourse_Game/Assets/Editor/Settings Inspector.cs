using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SettingsWarrior))]
public class SettingsInspector : Editor
{
    private SerializedProperty _health;
    private HealthBar _healthBar;
    private bool _setHealth;

    private void OnEnable()
    {
        _health = serializedObject.FindProperty("health");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_health);
        GUILayout.Label(_health.floatValue.ToString());
        _setHealth = GUILayout.Button("God's Treatment");
        if (_setHealth)
        {
            _healthBar = FindObjectOfType<HealthBar>();
            _health.floatValue = 1f;
            _healthBar.HealthCheck();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
