﻿using UnityEngine;
using UnityEngine.Rendering;

namespace Sztorm.MaterialBinder
{
    public readonly struct TextureBinding
    {
        private readonly Material material;
        private readonly int propertyId;

        public TextureBinding(Material material, int propertyId)
        {
            this.material = material;
            this.propertyId = propertyId;
        }

        public Texture AsTexture => material.GetTexture(propertyId);

        public void Set(Texture value) => material.SetTexture(propertyId, value);

        public void Set(RenderTexture value, RenderTextureSubElement element)
            => material.SetTexture(propertyId, value, element);
    }
}
