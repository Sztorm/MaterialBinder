using System;
using System.Globalization;
using UnityEngine;

namespace Sztorm.MaterialBinder
{
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

        public bool IsEnabled(T value)
            => material.IsKeywordEnabled(keywordNames[value.ToInt32(CultureInfo.InvariantCulture.NumberFormat)]);

        public bool IsDisabled(T value)
            => !material.IsKeywordEnabled(keywordNames[value.ToInt32(CultureInfo.InvariantCulture.NumberFormat)]);

        /// <summary>
        /// Sets specified keyword and disables previously set keyword.
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
