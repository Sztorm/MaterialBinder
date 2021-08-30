using UnityEngine;

namespace Sztorm.MaterialBinder
{
    public readonly struct VectorBinding
    {
        private readonly Material material;
        private readonly int propertyId;

        public VectorBinding(Material material, int propertyId)
        {
            this.material = material;
            this.propertyId = propertyId;
        }

        public Vector4 AsVector4 => material.GetVector(propertyId);

        public Vector3 AsVector3
        {
            get
            {
                Vector4 v = material.GetVector(propertyId);
                return new Vector3(v.x, v.y, v.z);
            }
        }

        public Vector2 AsVector2
        {
            get
            {
                Vector4 v = material.GetVector(propertyId);
                return new Vector2(v.x, v.y);
            }
        }

        public Color AsColor
        {
            get
            {
                Vector4 v = material.GetVector(propertyId);
                return new Color(v.x, v.y, v.z, v.w);
            }
        }

        public void Set(Vector4 value) => material.SetVector(propertyId, value);

        public void Set(Vector3 value)
            => material.SetVector(propertyId, new Vector4(value.x, value.y, value.z, 0F));

        public void Set(Vector2 value)
            => material.SetVector(propertyId, new Vector4(value.x, value.y, 0F, 0F));

        public void Set(Color value)
            => material.SetVector(propertyId, new Vector4(value.r, value.g, value.b, value.a));
    }
}
