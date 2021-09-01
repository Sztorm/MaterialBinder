using System.Text;

namespace Sztorm.MaterialBinder
{
    internal static class StringBuilderExtensions
    {
        public static StringBuilder RemoveCharsThatNotMatch(this StringBuilder source, params char[] match)
        {
            int matchLength = match.Length;

            for (int i = source.Length - 1; i >= 0; i--)
            {
                bool isMatching = false;

                for (int j = 0; j < matchLength; j++)
                {
                    if (source[i] == match[j])
                    {
                        isMatching = true;
                        break;
                    }
                }
                if (!isMatching)
                {
                    source.Remove(startIndex: i, length: 1);
                }
            }
            return source;
        }

        public static StringBuilder RemoveCharsThatMatch(this StringBuilder source, params char[] match)
        {
            int matchLength = match.Length;

            for (int i = source.Length - 1; i >= 0; i--)
            {
                bool isMatching = false;

                for (int j = 0; j < matchLength; j++)
                {
                    if (source[i] == match[j])
                    {
                        isMatching = true;
                        break;
                    }
                }
                if (isMatching)
                {
                    source.Remove(startIndex: i, length: 1);
                }
            }
            return source;
        }

        public static StringBuilder ReplaceCharsThatMatch(
            this StringBuilder source, char replacement, params char[] match)
        {
            int matchLength = match.Length;

            for (int i = 0, length = source.Length; i < length; i++)
            {
                bool isMatching = false;

                for (int j = 0; j < matchLength; j++)
                {
                    if (source[i] == match[j])
                    {
                        isMatching = true;
                        break;
                    }
                }
                if (isMatching)
                {
                    source[i] = replacement;
                }
            }
            return source;
        }

        public static bool IsDigit(char c) => c >= '0' && c <= '9';

        public static StringBuilder RemoveAllStartDigits(this StringBuilder source)
        {
            int startDigitCount = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (!IsDigit(source[i]))
                {
                    break;
                }
                startDigitCount++;
            }   
            return source.Remove(0, startDigitCount);
        }

        public static StringBuilder RemoveAllStartChars(this StringBuilder source, char c)
        {
            int startDotsCount = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != c)
                {
                    break;
                }
                startDotsCount++;
            }
            return source.Remove(0, startDotsCount);
        }

        public static StringBuilder RemoveAllEndChars(this StringBuilder source, char c)
        {
            int endDotsCount = 0;

            for (int i = source.Length - 1; i >= 0; i--)
            {
                if (source[i] != c)
                {
                    break;
                }
                endDotsCount++;
            }
            if (endDotsCount > 0)
            {
                return source.Remove(source.Length - endDotsCount, endDotsCount);
            }
            return source;
        }

        public static StringBuilder RemoveSubsequentCharsOrDigits(this StringBuilder source, char c)
        {
            for (int i = source.Length - 1; i > 0; i--)
            {
                if ((source[i] == c && source[i - 1] == c) ||
                    (IsDigit(source[i]) && source[i - 1] == c))
                {
                    source.Remove(startIndex: i - 1, length: 1);
                }
            }
            return source;
        }

        public static StringBuilder RemoveSubsequentChars(this StringBuilder source, char c)
        {
            for (int i = source.Length - 1; i > 0; i--)
            {
                if (source[i] == c && source[i - 1] == c)
                {
                    source.Remove(startIndex: i - 1, length: 1);
                }
            }
            return source;
        }

        public static StringBuilder AppendIndent(this StringBuilder source, int level)
        {
            const string Indent = "    ";

            for (int i = 0; i < level; i++)
            {
                source.Append(Indent);
            }
            return source;
        }

        public static StringBuilder AppendIndentedLine(this StringBuilder source, int indentlevel)
            => source.AppendIndent(indentlevel).AppendLine();

        public static StringBuilder AppendIndentedLine(
            this StringBuilder source, int indentlevel, string value)
            => source.AppendIndent(indentlevel).AppendLine(value);

        public static StringBuilder AppendFullName(this StringBuilder source, string fullName)
        {
            int fullNameLength = fullName.Length;
            int fullNameStart = source.Length;
            return source
                .Append(fullName)
                .Replace('/', '_', fullNameStart, fullNameLength)
                .Replace(" ", "", fullNameStart, fullNameLength);
        }

        public static StringBuilder AppendNameSpaceBeginning(
            this StringBuilder source, string nameSpace)
            => source.Append("namespace ")
                .AppendLine(nameSpace)
                .AppendLine("{");

        public static StringBuilder AppendTypeBeginning(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
            => source.AppendIndent(indentLevel)
                .Append("public struct ")
                .AppendFullName(shaderMetadata.FullName)
                .AppendLine("MaterialBindings")
                .AppendIndent(indentLevel)
                .AppendLine("{");

        public static StringBuilder AppendBindMethod(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
        {
            source.AppendIndent(indentLevel).AppendLine("/// <summary>");
            source.AppendIndent(indentLevel)
                .Append("/// Binds material which contains ")
                .Append(shaderMetadata.Name)
                .AppendLine(".");
            source.AppendIndent(indentLevel).AppendLine("/// </summary>");
            source.AppendIndent(indentLevel)
                .Append("/// <param name=\"material\">Material containing ")
                .Append(shaderMetadata.Name)
                .AppendLine(".</param>");
            source.AppendIndent(indentLevel).AppendLine("public void Bind(Material material)");
            source.AppendIndent(indentLevel).AppendLine("{");
            indentLevel++;
            source.AppendIndent(indentLevel).AppendLine("if (material.shader.name != FullName)");
            source.AppendIndent(indentLevel).AppendLine("{");
            indentLevel++;
            source.AppendIndent(indentLevel).AppendLine("throw new ArgumentException(");
            source.AppendIndent(indentLevel)
                .AppendLine("    \"Material must contain shader that match binder.\");");
            indentLevel--;
            source.AppendIndent(indentLevel).AppendLine("}");
            source.AppendIndent(indentLevel).AppendLine("this.material = material;");
            indentLevel--;
            return source.AppendIndent(indentLevel).AppendLine("}");   
        }

        public static StringBuilder AppendUnbindMethod(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
        {
            source.AppendIndent(indentLevel).AppendLine("/// <summary>");
            source.AppendIndent(indentLevel)
                .Append("/// Unbinds material. <see cref=\"")
                .AppendFullName(shaderMetadata.FullName)
                .AppendLine("MaterialBindings\"/> ");
            source.AppendIndent(indentLevel)
                .AppendLine("/// shader properties will not usable until material is bound again.");
            source.AppendIndent(indentLevel).AppendLine("/// </summary>");
            source.AppendIndent(indentLevel).AppendLine("public void Unbind()");
            source.AppendIndent(indentLevel).AppendLine("{");
            indentLevel++;
            source.AppendIndent(indentLevel).AppendLine("material = null;");
            indentLevel--;
            return source.AppendIndent(indentLevel).AppendLine("}");
        }

        public static StringBuilder AppendNameIdField(
            this StringBuilder source, int indentLevel, in ShaderPropertyMetadata propertyMetadata)
            => source.AppendIndent(indentLevel)
               .Append("public static readonly int ")
               .Append(propertyMetadata.CSharpDescription)
               .Append("PropertyId = Shader.PropertyToID(\"")
               .Append(propertyMetadata.Name)
               .AppendLine("\");");

        public static StringBuilder AppendNameField(
            this StringBuilder source, int indentLevel, in ShaderPropertyMetadata propertyMetadata)
            => source.AppendIndent(indentLevel)
               .Append("public const string ")
               .Append(propertyMetadata.CSharpDescription)
               .Append("PropertyName = \"")
               .Append(propertyMetadata.Name)
               .AppendLine("\";");

        public static StringBuilder AppendNameField(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
            => source.AppendIndent(indentLevel)
                .Append("public const string FullName = \"")
                .Append(shaderMetadata.FullName)
                .AppendLine("\";");

        public static StringBuilder AppendFullNameField(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
            => source.AppendIndent(indentLevel)
                .Append("public const string Name = \"")
                .Append(shaderMetadata.Name)
                .AppendLine("\";");

        public static StringBuilder AppendBindingGetter(
            this StringBuilder source, int indentLevel, in ShaderPropertyMetadata propertyMetadata)
        {
            source.AppendIndent(indentLevel)
                .Append("public ")
                .Append(propertyMetadata.BindingTypeName)
                .Append(" ")
                .AppendLine(propertyMetadata.CSharpDescription);
            source.AppendIndent(indentLevel + 1)
                .Append("=> new ")
                .Append(propertyMetadata.BindingTypeName)
                .Append("(material, ")
                .Append(propertyMetadata.CSharpDescription);

            string secondArgNameSuffix;

            switch (propertyMetadata.KeywordType)
            {
                case KeywordType.None:
                    secondArgNameSuffix = "PropertyId);";
                    break;
                case KeywordType.Bool:
                    secondArgNameSuffix = "PropertyName);";
                    break;
                case KeywordType.Enum:
                    secondArgNameSuffix = "KeywordNames);";
                    break;
                default:
                    secondArgNameSuffix = "";
                    break;
            }
            return source.AppendLine(secondArgNameSuffix);
        }

        public static StringBuilder AppendEnumKeywordPropertyNamesField(
            this StringBuilder source, int indentLevel, in ShaderPropertyMetadata propertyMetadata)
        {
            source.AppendIndent(indentLevel)
                .Append("private static readonly string[] ")
                .Append(propertyMetadata.CSharpDescription)
                .AppendLine("KeywordNames =");
            source.AppendIndent(indentLevel).AppendLine("{");
            indentLevel++;
            string[] keywordNames = propertyMetadata.EnumKeywordNames;
            int lastIndex = keywordNames.Length - 1;

            for (int i = 0; i < lastIndex; i++)
            {
                source.AppendIndent(indentLevel)
                    .Append('"')
                    .Append(keywordNames[i])
                    .AppendLine("\",");
            }
            source.AppendIndent(indentLevel)
                .Append('"')
                .Append(keywordNames[lastIndex])
                .AppendLine("\"");

            indentLevel--;
            return source.AppendIndent(indentLevel).AppendLine("};");
        }

        public static StringBuilder AppendKeywordPropertyEnum(
            this StringBuilder source, int indentLevel, in ShaderPropertyMetadata propertyMetadata)
        {
            string[] memberNames = propertyMetadata.EnumMemberNames;
            int lastIndex = memberNames.Length - 1;
            source.AppendIndent(indentLevel)
                .Append("public enum ")
                .AppendLine(propertyMetadata.EnumName);
            source.AppendIndent(indentLevel).AppendLine("{");
            indentLevel++;

            for (int i = 0; i < lastIndex; i++)
            {
                source.AppendIndent(indentLevel).Append(memberNames[i]).AppendLine(",");
            }
            source.AppendIndent(indentLevel).AppendLine(memberNames[lastIndex]);

            indentLevel--;
            return source.AppendIndent(indentLevel).AppendLine("}");
        }

        public static StringBuilder AppendEnums(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
        {
            for (int i = 0, propertyCount = shaderMetadata.PropertyCount; i < propertyCount; i++)
            {
                ShaderPropertyMetadata propertyMetadata = shaderMetadata.GetProperty(i);

                if (propertyMetadata.KeywordType == KeywordType.Enum)
                {
                    source.AppendKeywordPropertyEnum(indentLevel, propertyMetadata);
                    source.AppendIndentedLine(indentLevel);
                }
            }
            return source;
        }

        public static StringBuilder AppendEnumKeywordNamesFields(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
        {
            for (int i = 0, propertyCount = shaderMetadata.PropertyCount; i < propertyCount; i++)
            {
                ShaderPropertyMetadata propertyMetadata = shaderMetadata.GetProperty(i);

                if (propertyMetadata.KeywordType == KeywordType.Enum)
                {
                    source.AppendEnumKeywordPropertyNamesField(indentLevel, propertyMetadata);
                    source.AppendIndentedLine(indentLevel);
                }
            }
            return source;
        }

        public static StringBuilder AppendNameFields(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
        {
            for (int i = 0, propertyCount = shaderMetadata.PropertyCount; i < propertyCount; i++)
            {
                ShaderPropertyMetadata propertyMetadata = shaderMetadata.GetProperty(i);

                source.AppendNameField(indentLevel, propertyMetadata);
            }
            return source;
        }

        public static StringBuilder AppendNameIdFields(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
        {
            for (int i = 0, propertyCount = shaderMetadata.PropertyCount; i < propertyCount; i++)
            {
                ShaderPropertyMetadata propertyMetadata = shaderMetadata.GetProperty(i);

                if (propertyMetadata.KeywordType == KeywordType.None)
                {
                    source.AppendNameIdField(indentLevel, propertyMetadata);
                }
            }
            return source;
        }

        public static StringBuilder AppendBindingProperties(
            this StringBuilder source, int indentLevel, in ShaderMetadata shaderMetadata)
        {
            for (int i = 0, propertyCount = shaderMetadata.PropertyCount; i < propertyCount; i++)
            {
                ShaderPropertyMetadata propertyMetadata = shaderMetadata.GetProperty(i);

                source.AppendBindingGetter(indentLevel, propertyMetadata);
                source.AppendIndentedLine(indentLevel);
            }
            return source;
        }
    }
}
