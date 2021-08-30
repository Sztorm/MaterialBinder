using UnityEngine;

namespace Sztorm.MaterialBinder
{
    public readonly struct ScalarBinding
    {
        private readonly Material material;
        private readonly int propertyId;

        public ScalarBinding(Material material, int propertyId)
        {
            this.material = material;
            this.propertyId = propertyId;
        }

        public float AsFloat => material.GetFloat(propertyId);

        public int AsInt => material.GetInt(propertyId);

        public bool AsBool => material.GetFloat(propertyId) != 0F;

        public void Set(float value) => material.SetFloat(propertyId, value);

        public void Set(int value) => material.SetInt(propertyId, value);

        public void Set(bool value) => material.SetFloat(propertyId, value ? 1F : 0F);
    }
}
