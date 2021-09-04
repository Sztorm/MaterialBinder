using UnityEngine;

namespace Sztorm.MaterialBinder
{
    /// <summary>
    /// Represents material keyword binding of a <see langword="bool"/> type.
    /// </summary>
    public readonly struct BoolKeywordBinding
    {
        private readonly Material material;
        private readonly string keywordName;

        public BoolKeywordBinding(Material material, string keywordName)
        {
            this.material = material;
            this.keywordName = keywordName;
        }

        /// <summary>
        /// Returns value indicating whether keyword is enabled.
        /// </summary>
        public bool IsEnabled => material.IsKeywordEnabled(keywordName);

        /// <summary>
        /// Returns value indicating whether keyword is disabled.
        /// </summary>
        public bool IsDisabled => !material.IsKeywordEnabled(keywordName);

        /// <summary>
        /// Sets material keyword to a specified value.
        /// </summary>
        /// <param name="value"></param>
        public void SetKeyword(bool value)
        {
            if (value)
            {
                EnableKeyword();
                return;
            }
            DisableKeyword();
        }

        /// <summary>
        /// Sets material keyword to <see langword="true"/>.
        /// </summary>
        public void EnableKeyword() => material.EnableKeyword(keywordName);

        /// <summary>
        /// Sets material keyword to <see langword="false"/>.
        /// </summary>
        public void DisableKeyword() => material.DisableKeyword(keywordName);
    }
}
