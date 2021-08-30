using System.Text;
using System.IO;
using UnityEngine;

namespace Sztorm.MaterialBinder
{
    [CreateAssetMenu(fileName = "MaterialBinder", menuName = "Sztorm/MaterialBinder")]
    public sealed class MaterialBinder : ScriptableObject
    {
        private static readonly char[] ValidNameSpaceChars =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
            'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
            'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y',
            'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '_', '.'
        };

        private static readonly char[] InvalidPathChars = { ':', '*', '?', '"', '<', '>', '|' };

        [SerializeField]
        private Shader[] shaders;

        [SerializeField]
        [Tooltip("Root folder is Assets. Path must exist, the folders will not be generated.")]
        private string relativeSavePath = "";

        [SerializeField]
        private string nameSpace = "";

        private void ValidateNameSpace(StringBuilder textBuffer)
        {
            textBuffer
                .Clear()
                .Append(nameSpace)
                .RemoveCharsThatNotMatch(ValidNameSpaceChars);
            int prevTextBufferLength;

            do
            {
                prevTextBufferLength = textBuffer.Length;
                textBuffer
                    .RemoveAllStartDigits()
                    .RemoveAllStartChars('.')
                    .RemoveSubsequentCharsOrDigits('.')
                    .RemoveAllEndChars('.');
            }
            while (textBuffer.Length != prevTextBufferLength);

            nameSpace = textBuffer.ToString();
        }

        private void ValidateSavePath(StringBuilder textBuffer)
        {
            textBuffer
                .Clear()
                .Append(relativeSavePath)
                .RemoveCharsThatMatch(InvalidPathChars);
            int prevTextBufferLength;

            do
            {
                prevTextBufferLength = textBuffer.Length;
                textBuffer
                    .RemoveAllStartDigits()
                    .RemoveAllStartChars('/')
                    .RemoveAllStartChars('\\')
                    .RemoveSubsequentChars('/')
                    .RemoveSubsequentChars('\\')
                    .RemoveAllEndChars('/')
                    .RemoveAllEndChars('\\');
            }
            while (textBuffer.Length != prevTextBufferLength);

            relativeSavePath = textBuffer.ToString();
        }

        private void OnValidate()
        {
            var textBuffer = new StringBuilder(capacity: 128);

            ValidateNameSpace(textBuffer);
            ValidateSavePath(textBuffer);
        }

        public void CreateBindingsFiles()
        {
            string basePath = $"{Application.dataPath}/{relativeSavePath}/";

            for (int i = 0; i < shaders.Length; i++)
            {
                CreateBindingsFile(shaders[i], basePath);
            }
        }

        private void CreateBindingsFile(Shader shader, string basePath)
        {
            var shaderMetadata = new ShaderMetadata(shader);
            string path = new StringBuilder(capacity: 128)
                .Append(basePath)
                .AppendFullName(shaderMetadata.FullName)
                .Append("MaterialBindings.cs").ToString();
            string content = CreateBindingsText(shaderMetadata);

            File.WriteAllText(path, content);
        }

        private string CreateBindingsText(in ShaderMetadata shaderMetadata)
        {
            bool hasNameSpace = !string.IsNullOrEmpty(nameSpace);
            int propertyCount = shaderMetadata.PropertyCount;
            var sb = new StringBuilder(capacity: 4096 + propertyCount * 1024);     
            int indentLevel = 0;

            sb.AppendLine("using System;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using Sztorm.MaterialBinder;");
            sb.AppendLine();

            if (hasNameSpace)
            {
                sb.AppendNameSpaceBeginning(nameSpace);
                indentLevel++;
            }
            sb.AppendTypeBeginning(indentLevel, shaderMetadata);
            indentLevel++;

            sb.AppendEnums(indentLevel, shaderMetadata);
            sb.AppendEnumKeywordNamesFields(indentLevel, shaderMetadata);
            sb.AppendNameFields(indentLevel, shaderMetadata);
            sb.AppendFullNameField(indentLevel, shaderMetadata);
            sb.AppendNameField(indentLevel, shaderMetadata);
            sb.AppendNameIdFields(indentLevel, shaderMetadata);
            sb.AppendIndentedLine(indentLevel);
            sb.AppendIndentedLine(indentLevel, "private Material material;");
            sb.AppendIndentedLine(indentLevel);
            sb.AppendIndentedLine(indentLevel, "public Material Material => material;");
            sb.AppendIndentedLine(indentLevel);
            sb.AppendIndentedLine(indentLevel, "public bool IsBound => !(material is null);");
            sb.AppendIndentedLine(indentLevel);
            sb.AppendBindingProperties(indentLevel, shaderMetadata);
            sb.AppendBindMethod(indentLevel, shaderMetadata);
            sb.AppendIndentedLine(indentLevel);
            sb.AppendUnbindMethod(indentLevel, shaderMetadata);

            indentLevel--;
            sb.AppendIndentedLine(indentLevel, "}");

            if (hasNameSpace)
            {
                sb.AppendLine("}");
            }
            return sb.ToString();
        }
    }
}