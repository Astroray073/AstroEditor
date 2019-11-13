// ***********************************************************************
// The MIT License
// Copyright (c) 2019 Astroray. All rights reserved.
// ***********************************************************************

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Astro
{
    /// <summary>
    /// <see cref="AstroGUI"/> is helper class containing extended version of unity's <see cref="EditorGUI" />.
    /// </summary>
    public static class AstroGUI
    {
        /// <summary>
        /// Draws unity's default script field.
        /// </summary>
        /// <param name="position">rect</param>
        /// <param name="obj">target <see cref="Object"/></param>
        public static void ScriptField(Rect position, Object obj)
        {
            EditorGUI.BeginDisabledGroup(true);
            MonoScript script = null;

            switch (obj) {
            case MonoBehaviour monoBehaviour:
                script = MonoScript.FromMonoBehaviour(monoBehaviour);

                break;
            case ScriptableObject scriptableObject:
                script = MonoScript.FromScriptableObject(scriptableObject);

                break;
            }

            EditorGUI.ObjectField(position, new GUIContent("Script"), script, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// Draws the box with the color.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        public static void DrawBoxWithColor(Rect rect, Color color)
        {
            var savedColor = GUI.backgroundColor;
            GUI.backgroundColor = color;
            GUI.Box(rect, GUIContent.none);
            GUI.backgroundColor = savedColor;
        }

        #region LabelWithColor
        /// <summary>
        /// Draws the label with background color.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="style">The style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void LabelWithColor(Rect position, GUIContent label, GUIContent label2, GUIStyle style, Color backgroundColor)
        {
            DrawBoxWithColor(position, backgroundColor);
            EditorGUI.LabelField(position, label, label2, style);
        }

        /// <summary>
        /// Draws the label with background color.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="style">The style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void LabelWithColor(Rect position, GUIContent label, GUIStyle style, Color backgroundColor)
        {
            DrawBoxWithColor(position, backgroundColor);
            EditorGUI.LabelField(position, label, style);
        }

        /// <summary>
        /// Draws the label with background color.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void LabelWithColor(Rect position, GUIContent label, Color backgroundColor)
        {
            LabelWithColor(position, label, EditorStyles.label, backgroundColor);
        }

        /// <summary>
        /// Draws the label with background color.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="style">The style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void LabelWithColor(Rect position, string label, string label2, GUIStyle style, Color backgroundColor)
        {
            LabelWithColor(position, new GUIContent(label), new GUIContent(label2), style, backgroundColor);
        }

        /// <summary>
        /// Draws the label with background color.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void LabelWithColor(Rect position, string label, string label2, Color backgroundColor)
        {
            LabelWithColor(position, label, label2, EditorStyles.label, backgroundColor);
        }

        /// <summary>
        /// Draws the label with background color.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void LabelWithColor(Rect position, string label, GUIStyle style, Color backgroundColor)
        {
            LabelWithColor(position, new GUIContent(label), style, backgroundColor);
        }

        /// <summary>
        /// Draws the label with background color.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void LabelWithColor(Rect position, string label, Color backgroundColor)
        {
            LabelWithColor(position, new GUIContent(label), backgroundColor);
        }
        #endregion

        #region CenteredLabelField
        /// <summary>
        /// Draws centered label field.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void CenteredLabelField(Rect position, GUIContent label, GUIContent label2, Color backgroundColor)
        {
            LabelWithColor(position, label, label2, AstroGUIStyles.midLabel, backgroundColor);
        }

        /// <summary>
        /// Draws centered label field.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void CenteredLabelField(Rect position, GUIContent label, Color backgroundColor)
        {
            LabelWithColor(position, label, AstroGUIStyles.midLabel, backgroundColor);
        }

        /// <summary>
        /// Draws centered label field.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        public static void CenteredLabelField(Rect position, GUIContent label)
        {
            CenteredLabelField(position, label, Color.clear);
        }

        /// <summary>
        /// Draws centered label field.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        public static void CenteredLabelField(Rect position, GUIContent label, GUIContent label2)
        {
            CenteredLabelField(position, label, label2, Color.clear);
        }

        /// <summary>
        /// Draws centered label field.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        public static void CenteredLabelField(Rect position, string label)
        {
            CenteredLabelField(position, new GUIContent(label), Color.clear);
        }

        /// <summary>
        /// Draws centered label field.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        public static void CenteredLabelField(Rect position, string label, string label2)
        {
            CenteredLabelField(position, new GUIContent(label), new GUIContent(label2), Color.clear);
        }

        /// <summary>
        /// Draws centered label field.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void CenteredLabelField(Rect position, string label, Color backgroundColor)
        {
            CenteredLabelField(position, new GUIContent(label), backgroundColor);
        }

        /// <summary>
        /// Draws centered label field.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void CenteredLabelField(Rect position, string label, string label2, Color backgroundColor)
        {
            CenteredLabelField(position, new GUIContent(label), new GUIContent(label2), backgroundColor);
        }
        #endregion

        #region InputNameField
        /// <summary>
        /// Draws input name field.
        /// </summary>
        /// <param name="position">rect</param>
        /// <param name="label">The label</param>
        /// <param name="inputString">input name</param>
        /// <returns>input name</returns>
        public static string InputNameField(Rect position, GUIContent label, string inputString)
        {
            int inputIndex = Array.IndexOf(AstroEditorUtility.GetInputNames(), inputString);

            inputIndex = Mathf.Clamp(inputIndex, 0, AstroEditorUtility.GetInputNames().Length);
            inputIndex = EditorGUI.Popup(position, label, inputIndex, AstroEditorUtility.inputNameGuiContents);

            return AstroEditorUtility.GetInputNames()[inputIndex];
        }

        /// <summary>
        /// Draws input name field.
        /// </summary>
        /// <param name="position">rect</param>
        /// <param name="label">The label</param>
        /// <param name="inputString">input name</param>
        /// <returns>input name</returns>
        public static string InputNameField(Rect position, string label, string inputString)
        {
            return InputNameField(position, new GUIContent(label), inputString);
        }

        /// <summary>
        /// Draws input name field.
        /// </summary>
        /// <param name="position">rect</param>
        /// <param name="label">The label</param>
        /// <param name="property">Input name property</param>
        /// <returns>input name</returns>
        public static void InputNameField(Rect position, GUIContent label, SerializedProperty property)
        {
            property.stringValue = InputNameField(position, label, property.stringValue);
        }

        /// <summary>
        /// Draws input name field.
        /// </summary>
        /// <param name="position">rect</param>
        /// <param name="property">Input name property</param>
        /// <returns>input name</returns>
        public static void InputNameField(Rect position, SerializedProperty property)
        {
            property.stringValue = InputNameField(position, new GUIContent(property.displayName), property.stringValue);
        }
        #endregion

        #region Switch
        /// <summary>
        /// The selection options
        /// </summary>
        public static readonly GUIContent[] _switchLabels = {
            EditorGUIUtility.TrTextContent("Off"),
            EditorGUIUtility.TrTextContent("On")
        };

        public static readonly GUIStyle              _defaultSwitchStyle = AstroGUIStyles.switchButton;
        public static readonly GUI.ToolbarButtonSize _defaultButtonSize  = GUI.ToolbarButtonSize.Fixed;

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="isOn">if set to <c>true</c> when button is on.</param>
        /// <param name="label">The label.</param>
        /// <param name="style">The button style.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, bool isOn, GUIContent label, GUIStyle style, GUI.ToolbarButtonSize buttonSize)
        {
            position = EditorGUI.PrefixLabel(position, label);

            return GUI.Toolbar(position, isOn ? 1 : 0, _switchLabels, style, buttonSize) == 1;
        }

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="isOn"></param>
        /// <param name="label">The label.</param>
        /// <param name="style">The button style.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, bool isOn, GUIContent label, GUIStyle style) => Switch(position, isOn, label, style, _defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="isOn"></param>
        /// <param name="label">The label.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, bool isOn, GUIContent label, GUI.ToolbarButtonSize buttonSize) => Switch(position, isOn, label, _defaultSwitchStyle, buttonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="isOn"></param>
        /// <param name="label">The label.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, bool isOn, GUIContent label) => Switch(position, isOn, label, _defaultSwitchStyle, _defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="isOn"></param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, bool isOn) => Switch(position, isOn, GUIContent.none, _defaultSwitchStyle, _defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="property">Boolean property</param>
        /// <param name="label">The label.</param>
        /// <param name="style">The button style.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, SerializedProperty property, GUIContent label, GUIStyle style, GUI.ToolbarButtonSize buttonSize)
        {
            property.boolValue = Switch(position, property.boolValue, label, style, buttonSize);

            return property.boolValue;
        }

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="property">Boolean property</param>
        /// <param name="label">The label.</param>
        /// <param name="style">The button style.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, SerializedProperty property, GUIContent label, GUIStyle style) => Switch(position, property, label, style, _defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="property">Boolean property</param>
        /// <param name="label">The label.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, SerializedProperty property, GUIContent label, GUI.ToolbarButtonSize buttonSize)
            => Switch(position, property, label, _defaultSwitchStyle, buttonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="property">Boolean property</param>
        /// <param name="label">The label.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, SerializedProperty property, GUIContent label) => Switch(position, property, label, _defaultSwitchStyle, _defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="property">Boolean property</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(Rect position, SerializedProperty property) => Switch(position, property, property.GetLabel(), _defaultSwitchStyle, _defaultButtonSize);
        #endregion

        #region Foldout
        public static readonly Color _defaultFoldoutBackgroundColor = Color.clear;

        /// <summary>
        /// Draw foldout in the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <returns><c>true</c> if is shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(Rect rect, bool foldout, string label, Color backgroundColor, bool toggleOnLabelClick = true)
        {
            DrawBoxWithColor(rect, backgroundColor);

            return EditorGUI.Foldout(rect, foldout, label, toggleOnLabelClick);
        }

        /// <summary>
        /// Draw foldout in the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="label">The label.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <returns><c>true</c> if is shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(Rect rect, bool foldout, string label, bool toggleOnLabelClick = true)
            => Foldout(rect, foldout, label, _defaultFoldoutBackgroundColor, toggleOnLabelClick);

        /// <summary>
        /// Draw foldout in the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="property">The property.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <returns><c>true</c> if shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(Rect rect, bool foldout, SerializedProperty property, bool toggleOnLabelClick = true)
        {
            var result = EditorGUI.Foldout(AstroEditorUtility.GetLabelRect(rect), foldout, GUIContent.none, toggleOnLabelClick);
            EditorGUI.PropertyField(rect, property);

            return result;
        }

        /// <summary>
        /// Draw foldout in the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="property">The property.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <returns><c>true</c> if shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(Rect rect, bool foldout, SerializedProperty property, Color backgroundColor, bool toggleOnLabelClick = true)
        {
            DrawBoxWithColor(rect, backgroundColor);
            var result = EditorGUI.Foldout(AstroEditorUtility.GetLabelRect(rect), foldout, GUIContent.none);
            rect.x     += 16.0f;
            rect.width -= 16.0f;
            EditorGUI.PropertyField(rect, property);

            return result;
        }

        /// <summary>
        /// Draw foldout in the specified rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="property">The property.</param>
        /// <param name="label">Label</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <returns><c>true</c> if shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(Rect rect, bool foldout, SerializedProperty property, GUIContent label, Color backgroundColor, bool toggleOnLabelClick = true)
        {
            DrawBoxWithColor(rect, backgroundColor);
            var result = EditorGUI.Foldout(AstroEditorUtility.GetLabelRect(rect), foldout, GUIContent.none);
            rect.x     += 16.0f;
            rect.width -= 16.0f;
            EditorGUI.PropertyField(rect, property, label);

            return result;
        }
        #endregion

        #region ProgressBar
        public static readonly Color _defaultProgressBarColor = AstroGUIStyles.Colors.pastelSkyblue;

        /// <summary>
        /// Draws progress bar.
        /// </summary>
        /// <param name="position">Rect</param>
        /// <param name="label">Label</param>
        /// <param name="curValue">Current value</param>
        /// <param name="maxValue">Max value</param>
        /// <param name="barColor">Bar color</param>
        public static void ProgressBar(Rect position, GUIContent label, float curValue, float maxValue, Color barColor)
        {
            position = EditorGUI.PrefixLabel(position, label);

            var backup = GUI.color;
            GUI.color = barColor;
            EditorGUI.ProgressBar(position, curValue <= 0.0f ? 0.0f : curValue / maxValue, $"{curValue} / {maxValue}");
            GUI.color = backup;
        }

        /// <summary>
        /// Draws progress bar.
        /// </summary>
        /// <param name="position">Rect</param>
        /// <param name="label">Label</param>
        /// <param name="curValue">Current value</param>
        /// <param name="maxValue">Max value</param>
        public static void ProgressBar(Rect position, GUIContent label, float curValue, float maxValue)
        {
            ProgressBar(position, label, curValue, maxValue, _defaultProgressBarColor);
        }

        /// <summary>
        /// Draws progress bar.
        /// </summary>
        /// <param name="position">Rect</param>
        /// <param name="label">Label</param>
        /// <param name="curValue">Current value</param>
        /// <param name="maxValue">Max value</param>
        /// <param name="barColor">Bar color</param>
        public static void ProgressBar(Rect position, string label, float curValue, float maxValue, Color barColor)
        {
            ProgressBar(position, new GUIContent(label), curValue, maxValue, barColor);
        }

        /// <summary>
        /// Draws progress bar.
        /// </summary>
        /// <param name="position">Rect</param>
        /// <param name="label">Label</param>
        /// <param name="curValue">Current value</param>
        /// <param name="maxValue">Max value</param>
        public static void ProgressBar(Rect position, string label, float curValue, float maxValue)
        {
            ProgressBar(position, label, curValue, maxValue, _defaultProgressBarColor);
        }
        #endregion

        #region PathField
        public static void PathField(Rect rect, GUIContent label, SerializedProperty property, string extension)
        {
            var buttonWidth = 32.0f;
            EditorGUI.BeginProperty(rect, label, property);
            rect = EditorGUI.IndentedRect(rect);

            using (var scope = new AstroGUILayout.HorizontalScope(rect)) {
                if (string.IsNullOrEmpty(label.text)) {
                    EditorGUI.PropertyField(scope.GetRectFromEnd(buttonWidth), property);
                } else {
                    EditorGUI.LabelField(scope.GetLabelRect(), label);
                    property.stringValue = EditorGUI.TextField(scope.GetRectFromEnd(buttonWidth), property.stringValue);
                }

                if (GUI.Button(scope.GetRemainingRect(), "...")) {
                    var directory = string.IsNullOrEmpty(property.stringValue) ? "" : Path.GetDirectoryName(property.stringValue);
                    var path      = EditorUtility.OpenFilePanel("Open File", directory, extension);

                    if (!string.IsNullOrEmpty(path)) {
                        property.stringValue = AstroEditorUtility.GetAssetPathFromAbsolutePath(path);
                    }
                }
            }

            EditorGUI.EndProperty();
        }

        public static void PathField(Rect rect, SerializedProperty property, string extension)
        {
            PathField(rect, new GUIContent(property.displayName), property, extension);
        }
        #endregion

        #region DirectoryField
        public static void DirectoryField(Rect rect, GUIContent label, SerializedProperty property)
        {
            var buttonWidth = 32.0f;
            EditorGUI.BeginProperty(rect, label, property);
            rect = EditorGUI.IndentedRect(rect);

            using (var scope = new AstroGUILayout.HorizontalScope(rect)) {
                if (string.IsNullOrEmpty(label.text)) {
                    EditorGUI.PropertyField(scope.GetRectFromEnd(buttonWidth), property);
                } else {
                    EditorGUI.LabelField(scope.GetLabelRect(), label);
                    property.stringValue = EditorGUI.TextField(scope.GetRectFromEnd(buttonWidth), property.stringValue);
                }

                if (GUI.Button(scope.GetRemainingRect(), "...")) {
                    var directory = string.IsNullOrEmpty(property.stringValue) ? "" : Path.GetDirectoryName(property.stringValue);
                    var path      = EditorUtility.OpenFolderPanel("Open File", directory, null);

                    if (!string.IsNullOrEmpty(path)) {
                        property.stringValue = AstroEditorUtility.GetAssetPathFromAbsolutePath(path);
                    }
                }
            }

            EditorGUI.EndProperty();
        }

        public static void DirectoryField(Rect rect, SerializedProperty property)
        {
            DirectoryField(rect, new GUIContent(property.displayName), property);
        }
        #endregion
    }
}