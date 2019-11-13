// ***********************************************************************
// The MIT License
// Copyright (c) 2019 Astroray. All rights reserved.
// ***********************************************************************

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Astro
{
    /// <summary>
    /// <see cref="AstroGUILayout"/> is helper class containing extended version of unity's <see cref="EditorGUILayout" />.
    /// </summary>
    public static class AstroGUILayout
    {
        private static Rect GetControlRect(params GUILayoutOption[] layoutOptions) => EditorGUILayout.GetControlRect(layoutOptions);

        #region Scopes
        /// <summary>
        /// Disposable helper class for GUI Layout.
        /// </summary>
        /// <seealso cref="System.IDisposable" />
        public sealed class HorizontalScope : IDisposable
        {
            /// <summary>
            /// The scope
            /// </summary>
            private Rect _scope;
            /// <summary>
            /// The last rect
            /// </summary>
            private Rect _lastRect;
            /// <summary>
            /// The remaining width
            /// </summary>
            private float _remainingWidth;

            /// <summary>
            /// Initializes a new instance of the <see cref="HorizontalScope"/> class.
            /// </summary>
            /// <param name="scope">The scope.</param>
            public HorizontalScope(Rect scope)
            {
                _scope          = scope;
                _lastRect       = _scope;
                _lastRect.width = 0.0f;
                _remainingWidth = _scope.width;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="HorizontalScope"/> class.
            /// </summary>
            /// <param name="scope">The scope.</param>
            /// <param name="style">The style.</param>
            public HorizontalScope(Rect scope, GUIStyle style) : this(style.margin.Remove(scope))
            {
                if (Event.current.type == EventType.Repaint) {
                    style.Draw(scope, false, false, true, false);
                }
            }

            /// <summary>
            /// Gets the scope.
            /// </summary>
            /// <returns>Rect.</returns>
            public Rect GetScope()
            {
                return _scope;
            }

            /// <summary>
            /// Gets the remaining rect.
            /// </summary>
            /// <returns>Rect.</returns>
            public Rect GetRemainingRect()
            {
                return GetRect(_remainingWidth);
            }

            /// <summary>
            /// Gets the rect.
            /// </summary>
            /// <param name="width">The width.</param>
            /// <returns>Rect.</returns>
            public Rect GetRect(float width)
            {
                var current = new Rect(_lastRect.x + _lastRect.width, _scope.y, width, _scope.height);
                _lastRect       =  current;
                _remainingWidth -= width;

                return current;
            }

            /// <summary>
            /// Gets the rect ratio.
            /// </summary>
            /// <param name="ratio">The ratio.</param>
            /// <returns>Rect.</returns>
            public Rect GetRectRatio(float ratio)
            {
                return GetRect(_scope.width * ratio);
            }

            /// <summary>
            /// Gets the last rect.
            /// </summary>
            /// <returns>Rect.</returns>
            public Rect GetLastRect()
            {
                return _lastRect;
            }

            /// <summary>
            /// Gets the label rect.
            /// </summary>
            /// <returns>Rect.</returns>
            public Rect GetLabelRect()
            {
                return GetRect(EditorGUIUtility.labelWidth - (EditorGUI.indentLevel * AstroEditorUtility.widthPerIndent));
            }

            /// <summary>
            /// Gets the label rect.
            /// </summary>
            /// <param name="indentLevel">The indent level.</param>
            /// <returns>Rect.</returns>
            public Rect GetLabelRect(int indentLevel)
            {
                return GetRect(EditorGUIUtility.labelWidth - (indentLevel * AstroEditorUtility.widthPerIndent));
            }

            /// <summary>
            /// Gets the rect even.
            /// </summary>
            /// <param name="count">The count.</param>
            /// <returns>Rect.</returns>
            public Rect GetRectEven(int count)
            {
                return GetRect(_remainingWidth / count);
            }

            /// <summary>
            /// Gets the rect from end.
            /// </summary>
            /// <param name="width">The width.</param>
            /// <returns>Rect.</returns>
            public Rect GetRectFromEnd(float width)
            {
                return GetRect(_remainingWidth - width);
            }

            /// <summary>
            /// Not so meaningful to do this. Just get better the readability when writing GUI layout code.
            /// </summary>
            public void Dispose()
            {
            }
        }

        /// <summary>
        /// Disposable helper class for GUI Layout.
        /// </summary>
        /// <seealso cref="System.IDisposable" />
        public sealed class VerticalScope : IDisposable
        {
            /// <summary>
            /// The scope
            /// </summary>
            private Rect _scope;
            /// <summary>
            /// The last rect
            /// </summary>
            private Rect _lastRect;
            /// <summary>
            /// The remaining height
            /// </summary>
            private float _remainingHeight;

            /// <summary>
            /// Initializes a new instance of the <see cref="VerticalScope"/> class.
            /// </summary>
            /// <param name="scope">The scope.</param>
            public VerticalScope(Rect scope)
            {
                _scope           = scope;
                _lastRect        = _scope;
                _lastRect.height = 0.0f;
                _remainingHeight = _scope.height;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="VerticalScope"/> class.
            /// </summary>
            /// <param name="scope">The scope.</param>
            /// <param name="style">The style.</param>
            public VerticalScope(Rect scope, GUIStyle style) : this(style.margin.Remove(scope))
            {
                if (Event.current.type == EventType.Repaint) {
                    style.Draw(scope, false, false, true, false);
                }
            }

            /// <summary>
            /// Gets the scope.
            /// </summary>
            /// <returns>Rect.</returns>
            public Rect GetScope()
            {
                return _scope;
            }

            /// <summary>
            /// Gets the remaining rect.
            /// </summary>
            /// <returns>Rect.</returns>
            public Rect GetRemainingRect()
            {
                return GetRect(_remainingHeight);
            }

            /// <summary>
            /// Gets the rect.
            /// </summary>
            /// <param name="height">The height.</param>
            /// <returns>Rect.</returns>
            public Rect GetRect(float height)
            {
                var current = new Rect(_scope.x, _lastRect.y + _lastRect.height, _scope.width, height);
                _lastRect        =  current;
                _remainingHeight -= height;

                return current;
            }

            /// <summary>
            /// Gets the single line rect.
            /// </summary>
            /// <returns>Rect.</returns>
            public Rect GetSingleLineRect()
            {
                return GetRect(EditorGUIUtility.singleLineHeight);
            }

            /// <summary>
            /// Gets the rect ratio.
            /// </summary>
            /// <param name="ratio">The ratio.</param>
            /// <returns>Rect.</returns>
            public Rect GetRectRatio(float ratio)
            {
                return GetRect(_scope.height * ratio);
            }

            /// <summary>
            /// Gets the last rect.
            /// </summary>
            /// <returns>Rect.</returns>
            public Rect GetLastRect()
            {
                return _lastRect;
            }

            /// <summary>
            /// Gets the rect even.
            /// </summary>
            /// <param name="count">The count.</param>
            /// <returns>Rect.</returns>
            public Rect GetRectEven(int count)
            {
                return GetRect(_remainingHeight / count);
            }

            /// <summary>
            /// Gets the rect from end.
            /// </summary>
            /// <param name="height">The height.</param>
            /// <returns>Rect.</returns>
            public Rect GetRectFromEnd(float height)
            {
                GetRect(_remainingHeight - height);

                return GetRect(height);
            }

            /// <summary>
            /// Not so meaningful to do this. Just get better the readability when writing GUI layout code.
            /// </summary>
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }

            public void Space(float height)
            {
                GetRect(height);
            }
        }
        #endregion

        public static void ScriptField(Object script, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.ScriptField(GetControlRect(layoutOptions), script);
        }

        #region LabelWithColor
        /// <summary>
        /// Labels the color of the field with background.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="style">The style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="options">The layoutOptions.</param>
        public static void LabelWithColor(GUIContent label, GUIContent label2, GUIStyle style, Color backgroundColor, params GUILayoutOption[] options)
        {
            AstroGUI.LabelWithColor(EditorGUILayout.GetControlRect(options), label, label2, style, backgroundColor);
        }

        /// <summary>
        /// Labels the color of the field with background.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="style">The style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void LabelWithColor(GUIContent label, GUIStyle style, Color backgroundColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.LabelWithColor(EditorGUILayout.GetControlRect(layoutOptions), label, style, backgroundColor);
        }

        /// <summary>
        /// Labels the color of the field with background.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="options">The layoutOptions.</param>
        public static void LabelWithColor(GUIContent label, Color backgroundColor, params GUILayoutOption[] options)
        {
            AstroGUI.LabelWithColor(EditorGUILayout.GetControlRect(options), label, backgroundColor);
        }

        /// <summary>
        /// Labels the color of the field with background.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="style">The style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="options">The layoutOptions.</param>
        public static void LabelWithColor(string label, string label2, GUIStyle style, Color backgroundColor, params GUILayoutOption[] options)
        {
            AstroGUI.LabelWithColor(EditorGUILayout.GetControlRect(options), label, label2, style, backgroundColor);
        }

        /// <summary>
        /// Labels the color of the field with background.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void LabelWithColor(string label, string label2, Color backgroundColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.LabelWithColor(EditorGUILayout.GetControlRect(layoutOptions), label, label2, backgroundColor);
        }

        /// <summary>
        /// Labels the color of the field with background.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void LabelWithColor(string label, Color backgroundColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.LabelWithColor(EditorGUILayout.GetControlRect(layoutOptions), label, backgroundColor);
        }

        /// <summary>
        /// Labels the color of the field with background.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="style">The style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void LabelWithColor(string label, GUIStyle style, Color backgroundColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.LabelWithColor(EditorGUILayout.GetControlRect(layoutOptions), label, style, backgroundColor);
        }
        #endregion

        #region CenteredLabelField
        /// <summary>
        /// Draws centered label field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void CenteredLabelField(GUIContent label, GUIContent label2, Color backgroundColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.CenteredLabelField(GetControlRect(layoutOptions), label, label2, backgroundColor);
        }

        /// <summary>
        /// Draws centered the label field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void CenteredLabelField(GUIContent label, Color backgroundColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.CenteredLabelField(GetControlRect(layoutOptions), label, backgroundColor);
        }

        /// <summary>
        /// Draws centered the label field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void CenteredLabelField(GUIContent label, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.CenteredLabelField(GetControlRect(layoutOptions), label, Color.clear);
        }

        /// <summary>
        /// Draws centered the label field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void CenteredLabelField(GUIContent label, GUIContent label2, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.CenteredLabelField(GetControlRect(layoutOptions), label, label2, Color.clear);
        }

        /// <summary>
        /// Draws centered the label field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void CenteredLabelField(string label, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.CenteredLabelField(GetControlRect(layoutOptions), label);
        }

        /// <summary>
        /// Draws centered the label field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void CenteredLabelField(string label, string label2, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.CenteredLabelField(GetControlRect(layoutOptions), label, label2, Color.clear);
        }

        /// <summary>
        /// Draws centered the label field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void CenteredLabelField(string label, Color backgroundColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.CenteredLabelField(GetControlRect(layoutOptions), label, backgroundColor);
        }

        /// <summary>
        /// Draws centered the label field.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="label2">The label2.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        public static void CenteredLabelField(string label, string label2, Color backgroundColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.CenteredLabelField(GetControlRect(layoutOptions), new GUIContent(label), new GUIContent(label2), backgroundColor);
        }
        #endregion

        #region InputNameField
        /// <summary>
        /// Draws input name field.
        /// </summary>
        /// <param name="label">The label</param>
        /// <param name="inputString">input name</param>
        /// <param name="layoutOptions">Layout options</param>
        /// <returns></returns>
        public static string InputNameField(GUIContent label, string inputString, params GUILayoutOption[] layoutOptions)
        {
            return AstroGUI.InputNameField(GetControlRect(layoutOptions), label, inputString);
        }

        /// <summary>
        /// Draws input name field.
        /// </summary>
        /// <param name="label">The label</param>
        /// <param name="inputString">input name</param>
        /// <param name="layoutOptions">Layout options</param>
        /// <returns></returns>
        public static string InputNameField(string label, string inputString, params GUILayoutOption[] layoutOptions)
        {
            return AstroGUI.InputNameField(GetControlRect(layoutOptions), new GUIContent(label), inputString);
        }

        /// <summary>
        /// Draws input name field.
        /// </summary>
        /// <param name="label">The label</param>
        /// <param name="inputString">input name</param>
        /// <param name="layoutOptions">Layout options</param>
        /// <returns></returns>
        public static void InputNameField(GUIContent label, SerializedProperty property, params GUILayoutOption[] layoutOptions)
        {
            property.stringValue = AstroGUI.InputNameField(GetControlRect(layoutOptions), label, property.stringValue);
        }

        /// <summary>
        /// Draws input name field.
        /// </summary>
        /// <param name="property">Input name property</param>
        /// <param name="layoutOptions">Layout options</param>
        /// <returns></returns>
        public static void InputNameField(SerializedProperty property, params GUILayoutOption[] layoutOptions)
        {
            property.stringValue = AstroGUI.InputNameField(GetControlRect(layoutOptions), new GUIContent(property.displayName), property.stringValue);
        }
        #endregion

        #region Switch
        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="isOn">if set to <c>true</c> when button is on.</param>
        /// <param name="label">The label.</param>
        /// <param name="style">The button style.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(bool isOn, GUIContent label, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Switch(GetControlRect(layoutOptions), isOn, label, style, buttonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="label">The label.</param>
        /// <param name="style">The button style.</param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(bool isOn, GUIContent label, GUIStyle style, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Switch(GetControlRect(layoutOptions), isOn, label, style, AstroGUI._defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="label">The label.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(bool isOn, GUIContent label, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Switch(GetControlRect(layoutOptions), isOn, label, AstroGUI._defaultSwitchStyle, buttonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="label">The label.</param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(bool isOn, GUIContent label, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Switch(GetControlRect(layoutOptions), isOn, label, AstroGUI._defaultSwitchStyle, AstroGUI._defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="isOn"></param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(bool isOn, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Switch(GetControlRect(layoutOptions), isOn, GUIContent.none, AstroGUI._defaultSwitchStyle, AstroGUI._defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="property">Boolean property</param>
        /// <param name="label">The label.</param>
        /// <param name="style">The button style.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(SerializedProperty property, GUIContent label, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Switch(GetControlRect(layoutOptions), property, label, style, buttonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="property">Boolean property</param>
        /// <param name="label">The label.</param>
        /// <param name="style">The button style.</param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(SerializedProperty property, GUIContent label, GUIStyle style, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Switch(GetControlRect(layoutOptions), property, label, style, AstroGUI._defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="property">Boolean property</param>
        /// <param name="label">The label.</param>
        /// <param name="buttonSize">The button size.</param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(SerializedProperty property, GUIContent label, GUI.ToolbarButtonSize buttonSize)
            => Switch(property, label, AstroGUI._defaultSwitchStyle, buttonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="property">Boolean property</param>
        /// <param name="label">The label.</param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(SerializedProperty property, GUIContent label, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Switch(GetControlRect(layoutOptions), property, label, AstroGUI._defaultSwitchStyle, AstroGUI._defaultButtonSize);

        /// <summary>
        /// Draws switch buttons.
        /// </summary>
        /// <param name="property">Boolean property.</param>
        /// <param name="layoutOptions">Layout options.</param>
        /// <returns><c>true</c> if on, <c>false</c> otherwise.</returns>
        public static bool Switch(SerializedProperty property, params GUILayoutOption[] layoutOptions) => AstroGUI.Switch(GetControlRect(layoutOptions), property);
        #endregion

        #region Foldout
        /// <summary>
        /// Foldouts the specified foldout.
        /// </summary>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        /// <returns><c>true</c> if shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(bool foldout, string label, Color backgroundColor, bool toggleOnLabelClick = true, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Foldout(EditorGUILayout.GetControlRect(layoutOptions), foldout, label, backgroundColor, toggleOnLabelClick);

        /// <summary>
        /// Foldouts the specified foldout.
        /// </summary>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="label">The label.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <param name="layoutOptions">The layoutOptions.</param>
        /// <returns><c>true</c> if shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(bool foldout, string label, bool toggleOnLabelClick = true, params GUILayoutOption[] layoutOptions)
            => AstroGUI.Foldout(EditorGUILayout.GetControlRect(layoutOptions), foldout, label, toggleOnLabelClick);

        /// <summary>
        /// Foldouts the specified foldout.
        /// </summary>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="property">The property.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <param name="options">The layoutOptions.</param>
        /// <returns><c>true</c> if shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(bool foldout, SerializedProperty property, bool toggleOnLabelClick = true, params GUILayoutOption[] options)
            => AstroGUI.Foldout(EditorGUILayout.GetControlRect(options), foldout, property, toggleOnLabelClick);

        /// <summary>
        /// Foldouts the specified foldout.
        /// </summary>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="property">The property.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <param name="options">The layoutOptions.</param>
        /// <returns><c>true</c> if shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(bool foldout, SerializedProperty property, Color backgroundColor, bool toggleOnLabelClick = true, params GUILayoutOption[] options)
            => AstroGUI.Foldout(EditorGUILayout.GetControlRect(options), foldout, property, backgroundColor, toggleOnLabelClick);

        /// <summary>
        /// Foldouts the specified foldout.
        /// </summary>
        /// <param name="foldout">if set to <c>true</c> [foldout].</param>
        /// <param name="property">The property.</param>
        /// <param name="label">Label</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="toggleOnLabelClick">if set to <c>true</c> [toggle on label click].</param>
        /// <param name="options">The layoutOptions.</param>
        /// <returns><c>true</c> if shown, <c>false</c> otherwise.</returns>
        public static bool Foldout(bool foldout, SerializedProperty property, GUIContent label, Color backgroundColor, bool toggleOnLabelClick = true,
                                   params GUILayoutOption[] options) => AstroGUI.Foldout(EditorGUILayout.GetControlRect(options), foldout, property, label, backgroundColor,
            toggleOnLabelClick);
        #endregion

        #region ProgressBar
        /// <summary>
        /// Draws progress bar.
        /// </summary>
        /// <param name="label">Label</param>
        /// <param name="curValue">Current value</param>
        /// <param name="maxValue">Max value</param>
        /// <param name="barColor">Bar color</param>
        public static void ProgressBar(GUIContent label, float curValue, float maxValue, Color barColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.ProgressBar(GetControlRect(layoutOptions), label, curValue, maxValue, barColor);
        }

        /// <summary>
        /// Draws progress bar.
        /// </summary>
        /// <param name="label">Label</param>
        /// <param name="curValue">Current value</param>
        /// <param name="maxValue">Max value</param>
        public static void ProgressBar(GUIContent label, float curValue, float maxValue, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.ProgressBar(GetControlRect(layoutOptions), label, curValue, maxValue);
        }

        /// <summary>
        /// Draws progress bar.
        /// </summary>
        /// <param name="label">Label</param>
        /// <param name="curValue">Current value</param>
        /// <param name="maxValue">Max value</param>
        /// <param name="barColor">Bar color</param>
        public static void ProgressBar(string label, float curValue, float maxValue, Color barColor, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.ProgressBar(GetControlRect(layoutOptions), label, curValue, maxValue, barColor);
        }

        /// <summary>
        /// Draws progress bar.
        /// </summary>
        /// <param name="label">Label</param>
        /// <param name="curValue">Current value</param>
        /// <param name="maxValue">Max value</param>
        /// <param name="layoutOptions"></param>
        public static void ProgressBar(string label, float curValue, float maxValue, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.ProgressBar(GetControlRect(layoutOptions), label, curValue, maxValue);
        }
        #endregion

        #region PathField
        public static void PathField(SerializedProperty property, GUIContent label, string extension, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.PathField(EditorGUILayout.GetControlRect(layoutOptions), label, property, extension);
        }

        public static void PathField(SerializedProperty property, string extension, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.PathField(EditorGUILayout.GetControlRect(layoutOptions), property.GetLabel(), property, extension);
        }
        #endregion

        #region DirectoryField
        public static void DirectoryField(SerializedProperty property, GUIContent label, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.DirectoryField(EditorGUILayout.GetControlRect(layoutOptions), label, property);
        }

        public static void DirectoryField(SerializedProperty property, params GUILayoutOption[] layoutOptions)
        {
            AstroGUI.DirectoryField(EditorGUILayout.GetControlRect(layoutOptions), property);
        }
        #endregion
    }
}