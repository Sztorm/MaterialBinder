using UnityEngine;

namespace Sztorm.MaterialBinder
{
    /// <summary>
    /// Represents material property binding of a vector type which can be either 
    /// <see cref="Vector2"/>, <see cref="Vector3"/>, <see cref="Vector4"/> or <see cref="Color"/>.
    /// </summary>
    public readonly struct VectorBinding
    {
        private readonly Material material;
        private readonly int propertyId;

        public VectorBinding(Material material, int propertyId)
        {
            this.material = material;
            this.propertyId = propertyId;
        }

        /// <summary>
        /// Returns material property as a <see cref="Vector4"/> value.
        /// </summary>
        public Vector4 AsVector4 => material.GetVector(propertyId);

        /// <summary>
        /// Returns material property as a <see cref="Vector3"/> value.
        /// </summary>
        public Vector3 AsVector3
        {
            get
            {
                Vector4 v = material.GetVector(propertyId);
                return new Vector3(v.x, v.y, v.z);
            }
        }

        /// <summary>
        /// Returns material property as a <see cref="Vector2"/> value.
        /// </summary>
        public Vector2 AsVector2
        {
            get
            {
                Vector4 v = material.GetVector(propertyId);
                return new Vector2(v.x, v.y);
            }
        }

        /// <summary>
        /// Returns material property as a <see cref="Color"/> value.
        /// </summary>
        public Color AsColor
        {
            get
            {
                Vector4 v = material.GetVector(propertyId);
                return new Color(v.x, v.y, v.z, v.w);
            }
        }

        /// <summary>
        /// Sets material property using a <see cref="Vector4"/> value.
        /// </summary>
        /// <param name="value"></param>
        public void Set(Vector4 value) => material.SetVector(propertyId, value);

        /// <summary>
        /// Sets material property using a <see cref="Vector3"/> value.
        /// </summary>
        /// <param name="value"></param>
        public void Set(Vector3 value)
            => material.SetVector(propertyId, new Vector4(value.x, value.y, value.z, 0F));

        /// <summary>
        /// Sets material property using a <see cref="Vector2"/> value.
        /// </summary>
        /// <param name="value"></param>
        public void Set(Vector2 value)
            => material.SetVector(propertyId, new Vector4(value.x, value.y, 0F, 0F));

        /// <summary>
        /// Sets material property using a <see cref="Color"/> value.
        /// </summary>
        /// <param name="value"></param>
        public void Set(Color value)
            => material.SetVector(propertyId, new Vector4(value.r, value.g, value.b, value.a));
    }
}
