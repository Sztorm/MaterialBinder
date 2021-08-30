using UnityEngine;

namespace Sztorm.MaterialBinder
{
    public readonly struct BoolKeywordBinding
    {
        private readonly Material material;
        private readonly string keywordName;

        public BoolKeywordBinding(Material material, string keywordName)
        {
            this.material = material;
            this.keywordName = keywordName;
        }

        public bool IsEnabled => material.IsKeywordEnabled(keywordName);

        public void SetKeyword(bool value)
        {
            if (value)
            {
                EnableKeyword();
                return;
            }
            DisableKeyword();
        }

        public void EnableKeyword() => material.EnableKeyword(keywordName);

        public void DisableKeyword() => material.DisableKeyword(keywordName);
    }
}
