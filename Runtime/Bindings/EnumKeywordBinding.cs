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

        public void SetKeyword(T keyword, bool value)
        {
            if (value)
            {
                EnableKeyword(keyword);
                return;
            }
            DisableKeyword(keyword);
        }

        public void EnableKeyword(T keyword)
            => material.EnableKeyword(keywordNames[keyword.ToInt32(CultureInfo.InvariantCulture.NumberFormat)]);

        public void DisableKeyword(T keyword)
            => material.DisableKeyword(keywordNames[keyword.ToInt32(CultureInfo.InvariantCulture.NumberFormat)]);
    }
}
