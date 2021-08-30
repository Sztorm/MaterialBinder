using System;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using static Sztorm.MaterialBinder.StringBuilderExtensions;

namespace Sztorm.MaterialBinder
{
    public readonly struct ShaderPropertyMetadata
    {
        private static readonly char[] InvalidCSharpNameChars =
        {
            '`', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '-', '+', '=', '{', '[', '}',
            ']', ':', ';', '"', '\'', '|', '\\', '<', ',', '>', '.', '?', '/', ' ', '\t'
        };
        private static readonly char[] CharsToRemove = { '\r', '\n' };

        public readonly string Description;
        public readonly string Name;
        public readonly string[] PropertyAttributes;
        public readonly ShaderPropertyType Type;
        public readonly KeywordType KeywordType;
        public readonly string CSharpDescription;
        public readonly string CSharpName;
        public readonly string CSharpTypeName;
        public readonly string EnumName;
        public readonly string[] EnumKeywordNames;
        public readonly string[] EnumNames;

        public string BindingTypeName
        {
            get
            {
                switch (KeywordType)
                {
                    case KeywordType.None:
                        switch (Type)
                        {
                            case ShaderPropertyType.Color:
                                return nameof(VectorBinding);
                            case ShaderPropertyType.Vector:
                                return nameof(VectorBinding);
                            case ShaderPropertyType.Float:
                                return nameof(ScalarBinding);
                            case ShaderPropertyType.Range:
                                return nameof(ScalarBinding);
                            case ShaderPropertyType.Texture:
                                return nameof(TextureBinding);
                            default:
                                return nameof(ScalarBinding);
                        }
                    case KeywordType.Bool:
                        return nameof(BoolKeywordBinding);
                    case KeywordType.Enum:
                        const string BaseName = "EnumKeywordBinding<";
                        var typeNameBuilder = new StringBuilder(
                            capacity: BaseName.Length + EnumName.Length + 1)
                            .Append("EnumKeywordBinding<")
                            .Append(EnumName)
                            .Append('>');

                        return typeNameBuilder.ToString();
                    default:
                        return nameof(ScalarBinding);
                }
            }
        }

        public static string GetCSharpName(string name)
        {
            var resultBuilder = new StringBuilder(capacity: name.Length)
                .Append(name)
                .ReplaceCharsThatMatch(replacement: '_', match: InvalidCSharpNameChars)
                .RemoveCharsThatMatch(CharsToRemove);

            if (resultBuilder.Length > 0 && IsDigit(resultBuilder[0]))
            {
                resultBuilder.Insert(0, '_');
            }
            return resultBuilder.ToString();
        }

        public static string GetCSharpTypeName(ShaderPropertyType type)
        {
            switch (type)
            {
                case ShaderPropertyType.Color:
                    return "Color";
                case ShaderPropertyType.Vector:
                    return "Vector4";
                case ShaderPropertyType.Float:
                    return "float";
                case ShaderPropertyType.Range:
                    return "float";
                case ShaderPropertyType.Texture:
                    return "Texture";
                default:
                    return "float";
            }
        }

        public static KeywordType GetKeywordType(string[] propertyAttributes)
        {
            if (propertyAttributes.Length == 0)
            {
                return KeywordType.None;
            }
            if (Array.Exists(propertyAttributes, i => i == "Toggle"))
            {
                return KeywordType.Bool;
            }
            if (Array.Exists(propertyAttributes, i => i.StartsWith("KeywordEnum")))
            {
                return KeywordType.Enum;
            }
            return KeywordType.None;
        }

        private static string[] GetEnumKeywordNameSuffixes(
            string[] propertyAttributes, KeywordType keywordType)
        {
            if (keywordType != KeywordType.Enum)
            {
                return null;
            }
            int enumKeywordCount = 1;
            string propertyAttribute = propertyAttributes[0];

            for (int i = 0, length = propertyAttribute.Length; i < length; i++)
            {
                if (propertyAttribute[i] == ',')
                {
                    enumKeywordCount++;
                }
            }
            var result = new string[enumKeywordCount];
            int startIndex = propertyAttribute.IndexOf('(') + 1;
            int resultIndex = 0;
            int propertyAttrIndex = startIndex;

            while (propertyAttrIndex < propertyAttribute.Length)
            {
                if (propertyAttribute[propertyAttrIndex] == ',' ||
                    propertyAttribute[propertyAttrIndex] == ')')
                {
                    int keywordLength = propertyAttrIndex - startIndex;
                    result[resultIndex] = new StringBuilder(
                        propertyAttribute,
                        startIndex,
                        keywordLength,
                        capacity: keywordLength).ToString();
                    resultIndex++;
                    startIndex = propertyAttrIndex + 2;
                }
                propertyAttrIndex++;
            }
            return result;
        }

        private static string[] GetEnumNames(string[] enumSuffixes, KeywordType keywordType)
        {
            if (keywordType != KeywordType.Enum)
            {
                return null;
            }
            int length = enumSuffixes.Length;
            var result = new string[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = GetCSharpName(enumSuffixes[i]);
            }
            return result;
        }

        private static string[] GetEnumKeywordNames(
            string[] enumSuffixes, string name, KeywordType keywordType)
        {
            if (keywordType != KeywordType.Enum)
            {
                return null;
            }
            int length = enumSuffixes.Length;
            var result = new string[length];

            for (int i = 0; i < length; i++)
            {
                var keywordBuilder = new StringBuilder(
                    capacity: name.Length + 1 + enumSuffixes[i].Length);
                result[i] = keywordBuilder
                    .Append(name)
                    .Append('_')
                    .Append(enumSuffixes[i])
                    .ToString();
            }
            return result;
        }

        private static string GetEnumName(string csharpDescription, KeywordType keywordType)
            => keywordType != KeywordType.Enum ? null : csharpDescription + "Enum";

        public ShaderPropertyMetadata(Shader shader, int propertyIndex)
        {
            Description = shader.GetPropertyDescription(propertyIndex);
            Name = shader.GetPropertyName(propertyIndex);
            Type = shader.GetPropertyType(propertyIndex);
            PropertyAttributes = shader.GetPropertyAttributes(propertyIndex);
            KeywordType = GetKeywordType(PropertyAttributes);
            CSharpDescription = GetCSharpName(Description);
            CSharpName = GetCSharpName(Name);
            CSharpTypeName = GetCSharpTypeName(Type);
            EnumName = GetEnumName(CSharpDescription, KeywordType);
            string[] enumSuffixes = GetEnumKeywordNameSuffixes(PropertyAttributes, KeywordType);
            EnumKeywordNames = GetEnumKeywordNames(enumSuffixes, Name, KeywordType);
            EnumNames = GetEnumNames(enumSuffixes, KeywordType);
        }
    }
}
