using System;
using UnityEngine;
using Sztorm.MaterialBinder;

namespace Sztorm.MaterialBinder
{
    public struct ShaderGraphs_Graph_TestAllPropsMaterialBindings
    {
        public enum EnumEnum : ulong
        {
            A,
            B,
            C
        }
        
        private static readonly string[] EnumKeywordNames =
        {
            "ENUM_8C69F9F6_A",
            "ENUM_8C69F9F6_B",
            "ENUM_8C69F9F6_C"
        };
        
        public const string Vector1PropertyName = "Vector1_6ED8F763";
        public const string Vector2PropertyName = "Vector2_784B0007";
        public const string Vector3PropertyName = "Vector3_92521F1E";
        public const string Vector4PropertyName = "Vector4_809C1866";
        public const string ColorPropertyName = "Color_98C98E97";
        public const string Texture2DPropertyName = "Texture2D_50A269D2";
        public const string Texture2D_ArrayPropertyName = "Texture2DArray_AF46C6FE";
        public const string Texture3DPropertyName = "Texture3D_3517F73F";
        public const string CubemapPropertyName = "Cubemap_B887F40E";
        public const string BooleanPropertyName = "Boolean_2ACEF9FA";
        public const string BooleanKeywordPropertyName = "BOOLEAN_EE9E0252";
        public const string EnumPropertyName = "ENUM_8C69F9F6";
        public const string Name = "Graph_TestAllProps";
        public const string FullName = "Shader Graphs/Graph_TestAllProps";
        public static readonly int Vector1PropertyId = Shader.PropertyToID("Vector1_6ED8F763");
        public static readonly int Vector2PropertyId = Shader.PropertyToID("Vector2_784B0007");
        public static readonly int Vector3PropertyId = Shader.PropertyToID("Vector3_92521F1E");
        public static readonly int Vector4PropertyId = Shader.PropertyToID("Vector4_809C1866");
        public static readonly int ColorPropertyId = Shader.PropertyToID("Color_98C98E97");
        public static readonly int Texture2DPropertyId = Shader.PropertyToID("Texture2D_50A269D2");
        public static readonly int Texture2D_ArrayPropertyId = Shader.PropertyToID("Texture2DArray_AF46C6FE");
        public static readonly int Texture3DPropertyId = Shader.PropertyToID("Texture3D_3517F73F");
        public static readonly int CubemapPropertyId = Shader.PropertyToID("Cubemap_B887F40E");
        public static readonly int BooleanPropertyId = Shader.PropertyToID("Boolean_2ACEF9FA");
        
        private Material material;
        
        public Material Material => material;
        
        public bool IsBound => !(material is null);
        
        public ScalarBinding Vector1
            => new ScalarBinding(material, Vector1PropertyId);
        
        public VectorBinding Vector2
            => new VectorBinding(material, Vector2PropertyId);
        
        public VectorBinding Vector3
            => new VectorBinding(material, Vector3PropertyId);
        
        public VectorBinding Vector4
            => new VectorBinding(material, Vector4PropertyId);
        
        public VectorBinding Color
            => new VectorBinding(material, ColorPropertyId);
        
        public TextureBinding Texture2D
            => new TextureBinding(material, Texture2DPropertyId);
        
        public TextureBinding Texture2D_Array
            => new TextureBinding(material, Texture2D_ArrayPropertyId);
        
        public TextureBinding Texture3D
            => new TextureBinding(material, Texture3DPropertyId);
        
        public TextureBinding Cubemap
            => new TextureBinding(material, CubemapPropertyId);
        
        public ScalarBinding Boolean
            => new ScalarBinding(material, BooleanPropertyId);
        
        public BoolKeywordBinding BooleanKeyword
            => new BoolKeywordBinding(material, BooleanKeywordPropertyName);
        
        public EnumKeywordBinding<EnumEnum> Enum
            => new EnumKeywordBinding<EnumEnum>(material, EnumKeywordNames);
        
        /// <summary>
        /// Binds material which contains Graph_TestAllProps.
        /// </summary>
        /// <param name="material">Material containing Graph_TestAllProps.</param>
        public void Bind(Material material)
        {
            if (material.shader.name != FullName)
            {
                throw new ArgumentException(
                    "Material must contain shader that match binder.");
            }
            this.material = material;
        }
        
        /// <summary>
        /// Unbinds material. <see cref="ShaderGraphs_Graph_TestAllPropsMaterialBindings"/> 
        /// shader properties will not usable until material is bound again.
        /// </summary>
        public void Unbind()
        {
            material = null;
        }
    }
}
