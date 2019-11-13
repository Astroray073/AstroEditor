// ***********************************************************************
// The MIT License
// Copyright (c) 2019 Astroray. All rights reserved.
// ***********************************************************************

using System;
using System.Reflection;

namespace Astro
{
    /// <summary>
    /// Collection of TypeExtension.
    /// </summary>
    public static class ReflectionExtension
    {
        /// <summary>
        /// Determines whether the specific <see cref="MemberInfo"/> has attribute.
        /// </summary>
        /// <typeparam name="T">The attribute</typeparam>
        /// <param name="memberInfo">info</param>
        /// <returns><c>true</c> if <paramref name="memberInfo"/> has attribute; otherwise, <c>false</c>.</returns>
        public static bool HasAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            var attributeList = memberInfo.GetCustomAttributes(typeof(T), true);

            return attributeList.Length > 0;
        }

        /// <summary>
        /// Tries the get attribute.
        /// </summary>
        /// <typeparam name="T">The attribute</typeparam>
        /// <param name="memberInfo"></param>
        /// <param name="attribute">The attribute.</param>
        /// <returns><c>true</c> if <paramref name="memberInfo"/> has attribute, <c>false</c> otherwise.</returns>
        public static bool TryGetAttribute<T>(this MemberInfo memberInfo, out T attribute) where T : Attribute
        {
            var attributeList = memberInfo.GetCustomAttributes(typeof(T), true);

            if (attributeList.Length > 0) {
                attribute = (T) attributeList[0];

                return true;
            }

            attribute = null;

            return false;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T">The attribute.</typeparam>
        /// <param name="memberInfo">The member information.</param>
        /// <returns>The attribute.</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo) where T : Attribute => memberInfo.TryGetAttribute(out T attribute) ? attribute : null;

        /// <summary>
        /// Determines whether the specified type has interface.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type has interface; otherwise, <c>false</c>.</returns>
        public static bool HasInterface<T>(this Type type) => typeof(T).IsAssignableFrom(type);

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public static object CreateInstance(this Type type) => Activator.CreateInstance(type);
    }
}