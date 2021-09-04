using UnityEngine;

namespace Sztorm.MaterialBinder
{
    /// <summary>
    /// Represents material property binding of a scalar type which can be either 
    /// <see langword="float"/>, <see langword="int"/> or <see langword="bool"/>.
    /// </summary>
    public readonly struct ScalarBinding
    {
        private readonly Material material;
        private readonly int propertyId;

        public ScalarBinding(Material material, int propertyId)
        {
            this.material = material;
            this.propertyId = propertyId;
        }

        /// <summary>
        /// Returns material property as a <see langword="float"/> value.
        /// </summary>
        public float AsFloat => material.GetFloat(propertyId);

        /// <summary>
        /// Returns material property as an <see langword="int"/> value.
        /// </summary>
        public int AsInt => material.GetInt(propertyId);

        /// <summary>
        /// Returns material property as a <see langword="bool"/> value.
        /// </summary>
        public bool AsBool => material.GetFloat(propertyId) != 0F;

        /// <summary>
        /// Sets material property using a <see langword="float"/> value.
        /// </summary>
        /// <param name="value"></param>
        public void Set(float value) => material.SetFloat(propertyId, value);

        /// <summary>
        /// Sets material property using an <see langword="int"/> value.
        /// </summary>
        public void Set(int value) => material.SetInt(propertyId, value);

        /// <summary>
        /// Sets material property using a <see langword="bool"/> value.
        /// </summary>
        public void Set(bool value) => material.SetFloat(propertyId, value ? 1F : 0F);
    }
}
