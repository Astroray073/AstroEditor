// ***********************************************************************
// The MIT License
// Copyright (c) 2019 Astroray. All rights reserved.
// ***********************************************************************

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Astro
{
    /// <summary>
    /// Easy to user <see cref="ReorderableList"/>.
    /// This class wraps <see cref="ReorderableList"/> and automatically fills callbacks.
    /// </summary>
    public class AstroReorderableList
    {
        public static float headerHeight  { get; } = 18.0f;
        public static float elementHeight { get; } = 24.0f;
        public static float footerHeight  { get; } = 13.0f;

        public static readonly Color[] _headerColors = {
            AstroGUIStyles.Colors.pastelScarlet,
            AstroGUIStyles.Colors.pastelGreen,
            AstroGUIStyles.Colors.pastelBlue,
            AstroGUIStyles.Colors.pastelYellow,
            AstroGUIStyles.Colors.pastelOrange,
            AstroGUIStyles.Colors.pastelPurple,
            AstroGUIStyles.Colors.pastelMagenta,
            AstroGUIStyles.Colors.transparentGrey
        };

        private ReorderableList _list;
        private Type            _elementType;
        private GUIContent      _label;
        private Color           _headerColor;
        private FieldInfo[]     _fieldInfos;
        private GUIContent[]    _headerContents;

        /// <summary>
        /// Creates simple reorderable list.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="elements"></param>
        /// <param name="headerColor"></param>
        /// <param name="customHeader"></param>
        /// <param name="draggable"></param>
        /// <param name="displayHeader"></param>
        /// <param name="displayAddButton"></param>
        /// <param name="displayRemoveButton"></param>
        public AstroReorderableList(SerializedObject serializedObject, SerializedProperty elements, Color headerColor, GUIContent customHeader = null,
                                    bool draggable = true, bool displayHeader = true, bool displayAddButton = true, bool displayRemoveButton = true)
        {
            InitList(serializedObject, elements, null, headerColor, customHeader, draggable, displayHeader, displayAddButton, displayRemoveButton);
        }

        /// <summary>
        /// Creates complex reorderable list for given element type.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="elements"></param>
        /// <param name="elementType"></param>
        /// <param name="customHeader"></param>
        /// <param name="draggable"></param>
        /// <param name="displayHeader"></param>
        /// <param name="displayAddButton"></param>
        /// <param name="displayRemoveButton"></param>
        public AstroReorderableList(SerializedObject serializedObject, SerializedProperty elements, Type elementType, GUIContent customHeader = null,
                                    bool draggable = true, bool displayHeader = true, bool displayAddButton = true, bool displayRemoveButton = true)
        {
            InitList(serializedObject, elements, elementType, AstroGUIStyles.Colors.transparentGrey, customHeader, draggable, displayHeader, displayAddButton,
                displayRemoveButton);
        }

        private void InitList(SerializedObject serializedObject, SerializedProperty elements, Type elementType, Color headerColor, GUIContent customHeader,
                              bool draggable, bool displayHeader, bool displayAddButton, bool displayRemoveButton)
        {
            _elementType = elementType;
            _label       = customHeader ?? elements.GetLabel();
            _headerColor = headerColor;

            _list = new ReorderableList(serializedObject, elements,
                draggable, displayHeader, displayAddButton, displayRemoveButton);

            if (_elementType == null) {
                InitSimpleReorderableList(serializedObject, elements, draggable);
            } else {
                InitComplexReorderableList(serializedObject, elements, draggable);
            }
        }

        private void InitSimpleReorderableList(SerializedObject serializedObject, SerializedProperty elements, bool draggable)
        {
            _list.drawHeaderCallback    += DoSimpleListHeader;
            _list.elementHeightCallback += GetSimpleElementHeight;
            _list.drawElementCallback   += DoSimpleElement;
        }

        private void DoSimpleListHeader(Rect rect)
        {
            AstroGUI.CenteredLabelField(AstroReorderableListUtility.AdjustRlHeaderRect(rect), _label, _headerColor);
        }

        private float GetSimpleElementHeight(int index)
        {
            return EditorGUI.GetPropertyHeight(_list.serializedProperty.GetArrayElementAtIndex(index), GUIContent.none) + 4.0f;
        }

        private void DoSimpleElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.PropertyField(AstroReorderableListUtility.AdjustRlElementRect(rect), _list.serializedProperty.GetArrayElementAtIndex(index), GUIContent.none);
        }

        private void InitComplexReorderableList(SerializedObject serializedObject, SerializedProperty elements, bool draggable)
        {
            _fieldInfos = _elementType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                      .Where(info => info.IsPublic || info.HasAttribute<SerializeField>())
                                      .ToArray();

            _headerContents = _fieldInfos.Select(info => EditorGUIUtility.TrTextContent(info.Name.ToDisplayName())).ToArray();

            _list.drawHeaderCallback  += DoComplexHeader;
            _list.drawElementCallback += DoComplexElement;
        }

        private void DoComplexHeader(Rect rect)
        {
            rect = AstroReorderableListUtility.AdjustRlHeaderRect(rect);

            using (var scope = new AstroGUILayout.HorizontalScope(rect)) {
                var width = rect.width / _headerContents.Length;

                for (int i = 0; i < _headerContents.Length; ++i) {
                    var colorIndex = Repeat(i, _headerColors.Length);
                    var color      = _headerColors[colorIndex];

                    AstroGUI.CenteredLabelField(scope.GetRect(width), _headerContents[i], color);
                }
            }
        }

        private void DoComplexElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect = AstroReorderableListUtility.AdjustRlElementRect(rect);

            using (var scope = new AstroGUILayout.HorizontalScope(rect)) {
                var width    = rect.width / _fieldInfos.Length;
                var property = _list.serializedProperty.GetArrayElementAtIndex(index);

                foreach (var info in _fieldInfos) {
                    EditorGUI.PropertyField(scope.GetRect(width), property.FindPropertyRelative(info.Name), GUIContent.none);
                }
            }
        }

        private int Repeat(int i, int max)
        {
            if (i < 0) {
                return max - 1;
            }

            if (i == max) {
                return 0;
            }

            return i;
        }

        public void DoList(Rect rect)
        {
            _list.DoList(rect);
        }

        public void DoLayoutList()
        {
            _list.DoLayoutList();
        }
    }
}