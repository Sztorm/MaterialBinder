using UnityEngine;
using UnityEngine.Rendering;

namespace Sztorm.MaterialBinder
{
    /// <summary>
    /// Represents material property binding of a texture type.
    /// </summary>
    public readonly struct TextureBinding
    {
        private readonly Material material;
        private readonly int propertyId;

        public TextureBinding(Material material, int propertyId)
        {
            this.material = material;
            this.propertyId = propertyId;
        }

        /// <summary>
        /// Returns material property as a <see cref="Texture"/> object.
        /// </summary>
        public Texture AsTexture => material.GetTexture(propertyId);

        /// <summary>
        /// Returns material property as a <see cref="RenderTexture"/> object.
        /// </summary>
        public RenderTexture AsRenderTexture => (RenderTexture)material.GetTexture(propertyId);

        /// <summary>
        /// Sets material property using a <see cref="Texture"/> object.
        /// </summary>
        /// <param name="value"></param>
        public void Set(Texture value) => material.SetTexture(propertyId, value);

        /// <summary>
        /// Sets material property using <see cref="RenderTexture"/> and 
        /// <see cref="RenderTextureSubElement"/> objects.
        /// </summary>
        /// <param name="value"></param>
        public void Set(RenderTexture value, RenderTextureSubElement element)
            => material.SetTexture(propertyId, value, element);
    }
}
