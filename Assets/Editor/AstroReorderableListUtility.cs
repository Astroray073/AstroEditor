// ***********************************************************************
// The MIT License
// Copyright (c) 2019 Astroray. All rights reserved.
// ***********************************************************************

using UnityEngine;

namespace Astro
{
    /// <summary>
    /// Collections of utilities for <see cref="UnityEditorInternal.ReorderableList"/>
    /// </summary>
    public static class AstroReorderableListUtility
    {
        public static Rect AdjustRlHeaderRect(Rect rect)
        {
            rect.xMin  += 14.0f;
            return rect;
        }

        /// <summary>
        /// Gets the corrected rect for reorderable list.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>Rect.</returns>
        public static Rect AdjustRlElementRect(Rect rect)
        {
            rect.yMin += 2.0f;
            rect.yMax -= 2.0f;

            return rect;
        }
    }
}