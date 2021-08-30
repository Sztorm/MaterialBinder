using UnityEngine;

namespace Sztorm.MaterialBinder
{
    public readonly struct ShaderMetadata
    {
        private readonly ShaderPropertyMetadata[] properties;
        public readonly string FullName;
        public readonly string Name;

        public int PropertyCount => properties.Length;

        public ShaderMetadata(Shader shader)
        {
            int propertyCount = shader.GetPropertyCount();
            properties = new ShaderPropertyMetadata[propertyCount];

            for (int i = 0; i < propertyCount; i++)
            {
                properties[i] = new ShaderPropertyMetadata(shader, i);
            }
            FullName = shader.name;
            Name = GetShaderName(shader);
        }

        public ref readonly ShaderPropertyMetadata GetProperty(int index) => ref properties[index];

        public static string GetShaderName(Shader shader)
        {
            string fullName = shader.name;
            int nameStartIndex = fullName.LastIndexOf('/') + 1;
            int length = fullName.Length - nameStartIndex;

            return fullName.Substring(nameStartIndex, length);
        }
    }
}
