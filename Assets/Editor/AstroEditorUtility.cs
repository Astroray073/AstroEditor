// ***********************************************************************
// The MIT License
// Copyright (c) 2019 Astroray. All rights reserved.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace Astro
{
    /// <summary>
    /// Collections of editor utilities.
    /// </summary>
    public static class AstroEditorUtility
    {
        /// <summary>
        /// Gets the width per indent.
        /// </summary>
        /// <value>The width per indent.</value>
        public static float widthPerIndent { get; } = 16.0f;

        /// <summary>
        /// Gets the label rect considering indent level.
        /// </summary>
        /// <param name="controlRect">The control rect.</param>
        /// <returns><see cref="Rect"/></returns>
        public static Rect GetLabelRect(Rect controlRect)
        {
            var indentCorrection = EditorGUI.indentLevel * widthPerIndent;

            return new Rect(controlRect.x, controlRect.y, EditorGUIUtility.labelWidth - indentCorrection, controlRect.height);
        }

        public static GUIContent GetLabel(this SerializedProperty property) => new GUIContent(property.displayName);

        public static string ToDisplayName(this string name) => ObjectNames.NicifyVariableName(name);

        #region Input
        private static SerializedObject _inputManagerAsset;
        private static List<string>     _inputNames = new List<string>();
        private static GUIContent[]     _inputNameGuiContents;

        public static GUIContent[] inputNameGuiContents {
            get {
                if (_inputNameGuiContents == null
                    || HasInputManagerChanged()) {
                    _inputNameGuiContents = GetInputNames().Select(name => new GUIContent(name)).ToArray();
                }

                return _inputNameGuiContents;
            }
        }

        private static bool HasInputManagerChanged()
        {
            return _inputNames.Count == 0 || _inputManagerAsset.hasModifiedProperties;
        }

        private static void CollectsInputNames()
        {
            _inputNames.Clear();

            var property = _inputManagerAsset.GetIterator();
            property.NextVisible(true); // Enter Axes property.
            property.NextVisible(true); // Enter Axes children.

            while (property.NextVisible(false)) {
                _inputNames.Add(property.displayName);
            }
        }

        public static string[] GetInputNames()
        {
            if (_inputManagerAsset != null
                && !_inputManagerAsset.hasModifiedProperties
                && _inputNames.Count != 0) {
                return _inputNames.ToArray();
            }

            if (_inputManagerAsset == null) {
                var inputManagerAsset = AssetDatabase.LoadAssetAtPath<Object>("ProjectSettings/InputManager.asset");
                Assert.IsNotNull(inputManagerAsset);

                _inputManagerAsset = new SerializedObject(inputManagerAsset);
            }

            _inputManagerAsset.Update();
            CollectsInputNames();

            return _inputNames.ToArray();
        }
        #endregion

        #region Path
        /// <summary>
        /// Gets all sub folders name as <paramref name="folderName" />.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>System.String[].</returns>
        public static string[] GetAllSubFoldersNameAs(string folderName)
        {
            return Directory.GetDirectories(Application.dataPath, folderName, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Gets all path in special folder.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>System.String[].</returns>
        public static string[] GetAllPathInSpecialFolder(string searchPattern, string folderName)
        {
            var          directories  = GetAllSubFoldersNameAs(folderName);
            List<string> filePathList = new List<string>();

            for (int i = 0;
                i < directories.Length;
                i++) {
                filePathList.AddRange(Directory.GetFiles(directories[i], searchPattern, SearchOption.AllDirectories));
            }

            return filePathList.ToArray();
        }

        /// <summary>
        /// Converts asset path [Assets/...] to full path [C:/...]
        /// </summary>
        /// <param name="assetPath">The asset path.</param>
        /// <returns>System.String.</returns>
        public static string GetAbsolutePathFromAssetPath(string assetPath)
        {
            if (assetPath.StartsWith("Assets")) {
                return Application.dataPath + assetPath.Substring("Assets".Length);
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts full path [C:/...] to asset path [Assets/...]
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        /// <returns>System.String.</returns>
        public static string GetAssetPathFromAbsolutePath(string absolutePath)
        {
            var path = NormalizePathAsUnityStyle(absolutePath);

            if (path.StartsWith(Application.dataPath)) {
                return "Assets" + path.Substring(Application.dataPath.Length);
            }

            return string.Empty;
        }

        public static string GetResourcePathFromAbsolutePath(string absolutePath)
        {
            var path              = NormalizePathAsUnityStyle(absolutePath);
            var fileName          = Path.GetFileNameWithoutExtension(path);
            var index             = path.LastIndexOf("Resources/", StringComparison.Ordinal);
            var resourceDirectory = Path.GetDirectoryName(path.Substring(index + "Resources/".Length));

            return $"{resourceDirectory}{(string.IsNullOrEmpty(resourceDirectory) ? "" : "/")}{fileName}";
        }

        /// <summary>
        /// Normalizes the path as windows style.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        public static string NormalizePathAsWindowsStyle(string path)
        {
            var normalizedPath = path.Replace('/', '\\');

            return normalizedPath;
        }

        /// <summary>
        /// Normalizes the path as unity style.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>System.String.</returns>
        public static string NormalizePathAsUnityStyle(string path)
        {
            var normalizedPath = path.Replace('\\', '/');

            return normalizedPath;
        }

        public static bool IsAssetPath(string path)
        {
            return path.StartsWith("Assets");
        }
        #endregion
    }
}