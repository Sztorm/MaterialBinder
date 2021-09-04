using System;
using System.Globalization;
using UnityEngine;

namespace Sztorm.MaterialBinder
{
    /// <summary>
    /// Represents material keyword binding of a specified <see langword="enum"/> type.
    /// </summary>
    public readonly struct EnumKeywordBinding<T>
        where T : unmanaged, Enum, IConvertible
    {
        private readonly Material material;
        private readonly string[] keywordNames;

        public EnumKeywordBinding(Material material, string[] keywordNames)
        {
            this.material = material;
            this.keywordNames = keywordNames;
        }

        /// <summary>
        /// Returns value indicating whether specified keyword is enabled.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsEnabled(T value)
            => material.IsKeywordEnabled(keywordNames[value.ToInt32(CultureInfo.InvariantCulture.NumberFormat)]);

        /// <summary>
        /// Returns value indicating whether specified keyword is disabled.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsDisabled(T value)
            => !material.IsKeywordEnabled(keywordNames[value.ToInt32(CultureInfo.InvariantCulture.NumberFormat)]);

        /// <summary>
        /// Sets specified material keyword and disables previously set keyword.
        /// </summary>
        /// <param name="keyword"></param>
        public void SetKeyword(T keyword)
        {
            int keywordToIndex = keyword.ToInt32(CultureInfo.InvariantCulture.NumberFormat);

            if (!material.IsKeywordEnabled(keywordNames[keywordToIndex]))
            {
                material.EnableKeyword(keywordNames[keywordToIndex]);
            }
            for (int i = 0, length = keywordNames.Length; i < length; i++)
            {
                if (material.IsKeywordEnabled(keywordNames[i]) && i != keywordToIndex)
                {
                    material.DisableKeyword(keywordNames[i]);
                }
            }
        }
    }
}
