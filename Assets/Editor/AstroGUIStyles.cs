// ***********************************************************************
// The MIT License
// Copyright (c) 2019 Astroray. All rights reserved.
// ***********************************************************************

using UnityEditor;
using UnityEngine;

namespace Astro
{
    public static class AstroGUIStyles
    {
        private static GUIStyle _midLabel;
        private static GUIStyle _switchButton;

        public static GUIStyle midLabel
        {
            get
            {
                if (_midLabel == null) {
                    _midLabel = new GUIStyle(EditorStyles.label) {
                        alignment = TextAnchor.MiddleCenter
                    };
                }

                return _midLabel;
            }
        }

        public static GUIStyle switchButton
        {
            get
            {
                if (_switchButton == null) {
                    _switchButton = new GUIStyle(EditorStyles.toolbarButton);
                }

                return _switchButton;
            }
        }

        /// <summary>
        /// Color presets holder
        /// </summary>
        public static class Colors
        {
            /// <summary>
            /// Gets the pastel red.
            /// </summary>
            /// <value>The pastel red.</value>
            public static Color pastelRed => new Color32(255, 105, 97, 200);

            /// <summary>
            /// Gets the pastel scarlet.
            /// </summary>
            /// <value>The pastel scarlet.</value>
            public static Color pastelScarlet => new Color32(236, 77, 89, 200);

            /// <summary>
            /// Gets the pastel blue.
            /// </summary>
            /// <value>The pastel blue.</value>
            public static Color pastelBlue => new Color32(128, 162, 255, 200);

            /// <summary>
            /// Gets the pastel skyblue.
            /// </summary>
            /// <value>The pastel skyblue.</value>
            public static Color pastelSkyblue => new Color32(180, 224, 255, 200);

            /// <summary>
            /// Gets the pastel green.
            /// </summary>
            /// <value>The pastel green.</value>
            public static Color pastelGreen => new Color32(119, 190, 119, 200);

            /// <summary>
            /// Gets the pastel yellow.
            /// </summary>
            /// <value>The pastel yellow.</value>
            public static Color pastelYellow => new Color32(253, 253, 150, 200);

            /// <summary>
            /// Gets the pastel orange.
            /// </summary>
            /// <value>The pastel orange.</value>
            public static Color pastelOrange => new Color32(255, 179, 71, 200);

            /// <summary>
            /// Gets the pastel magenta.
            /// </summary>
            /// <value>The pastel magenta.</value>
            public static Color pastelMagenta => new Color32(244, 154, 194, 200);

            /// <summary>
            /// Gets the pastel purple.
            /// </summary>
            /// <value>The pastel purple.</value>
            public static Color pastelPurple => new Color32(150, 70, 150, 200);

            /// <summary>
            /// Gets the transparent grey.
            /// </summary>
            /// <value>The transparent grey.</value>
            public static Color transparentGrey => new Color32(120, 120, 120, 120);

            /// <summary>
            /// Gets the unity on selected color.
            /// </summary>
            /// <value>The unity on selected color.</value>
            public static Color unitySelection => new Color32(62, 125, 231, 255);

            /// <summary>
            /// Gets the default background color.
            /// </summary>
            /// <value>The default background.</value>
            public static Color defaultBackground => Color.white;
        }
    }
}