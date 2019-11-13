using Astro;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SampleMono))]
public class SampleMonoEditor : Editor
{
    private SerializedProperty _myBoolSwitchProperty;
    private SerializedProperty _myRefProperty;
    private SerializedProperty _myPathProperty;
    private SerializedProperty _myDirectoryProperty;

    private SerializedProperty _myIntListProperty;
    private SerializedProperty _myComplexListProperty;

    private AstroReorderableList _myIntList;
    private AstroReorderableList _myComplexList;

    private void OnEnable()
    {
        _myBoolSwitchProperty = serializedObject.FindProperty(nameof(SampleMono._myBoolSwitch));
        _myRefProperty        = serializedObject.FindProperty(nameof(SampleMono._myRef));
        _myPathProperty       = serializedObject.FindProperty(nameof(SampleMono._myPath));
        _myDirectoryProperty  = serializedObject.FindProperty(nameof(SampleMono._myDirectory));

        _myIntListProperty = serializedObject.FindProperty(nameof(SampleMono._myIntList));
        _myIntList         = new AstroReorderableList(serializedObject, _myIntListProperty, Color.clear);

        _myComplexListProperty = serializedObject.FindProperty(nameof(SampleMono._myComplexList));
        _myComplexList         = new AstroReorderableList(serializedObject, _myComplexListProperty, typeof(MyStruct));
    }

    private bool _showFoldout = true;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(nameof(AstroGUILayout.LabelWithColor));
        AstroGUILayout.LabelWithColor("Label With Color", AstroGUIStyles.Colors.transparentGrey);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(nameof(AstroGUILayout.ScriptField));
        AstroGUILayout.ScriptField(target);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(nameof(AstroGUILayout.InputNameField));
        AstroGUILayout.InputNameField("Input name", "Horizontal");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(nameof(AstroGUILayout.Switch));
        AstroGUILayout.Switch(_myBoolSwitchProperty);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(nameof(AstroGUILayout.Foldout));

        _showFoldout = AstroGUILayout.Foldout(_showFoldout, "Foldout");

        if (_showFoldout) {
            AstroGUILayout.LabelWithColor("Label With Color", AstroGUIStyles.Colors.transparentGrey);
            AstroGUILayout.ScriptField(target);
            AstroGUILayout.InputNameField("Input name", "Horizontal");
            AstroGUILayout.Switch(_myBoolSwitchProperty);
        }

        _myRefProperty.isExpanded = AstroGUILayout.Foldout(_myRefProperty.isExpanded, _myRefProperty);

        if (_myRefProperty.isExpanded) {
            AstroGUILayout.LabelWithColor("Label With Color", AstroGUIStyles.Colors.transparentGrey);
            AstroGUILayout.ScriptField(target);
            AstroGUILayout.InputNameField("Input name", "Horizontal");
            AstroGUILayout.Switch(_myBoolSwitchProperty);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(nameof(AstroGUILayout.ProgressBar));
        AstroGUILayout.ProgressBar("HP", 87.0f, 100.0f, AstroGUIStyles.Colors.pastelGreen);
        AstroGUILayout.ProgressBar("MP", 24.0f, 700.0f, AstroGUIStyles.Colors.pastelBlue);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(nameof(AstroGUILayout.PathField));
        AstroGUILayout.PathField(_myPathProperty, "txt,bat");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField(nameof(AstroGUILayout.ProgressBar));
        AstroGUILayout.DirectoryField(_myDirectoryProperty);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Simple ReorderableList");
        _myIntList.DoLayoutList();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Complex ReorderableList");
        _myComplexList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}