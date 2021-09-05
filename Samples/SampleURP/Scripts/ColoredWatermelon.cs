using System;
using UnityEngine;

namespace Sztorm.MaterialBinder
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class ColoredWatermelon : MonoBehaviour
    {
        private const int MinColorCount = 2;
        private const int MaxColorCount = 10;

        private ShaderGraphs_Graph_WaterMelonMaterialBindings matBindings;

        [SerializeField]
        private Color[] mainColors;

        [SerializeField]
        private Color[] additionalColors;

        [SerializeField]
        private int colorIndex;

        private static void ValidateArray<T>(ref T[] array, int minLength, int maxLength)
        {
            if (array == null)
            {
                return;
            }
            if (array.Length < minLength)
            {
                var newArray = new T[minLength];

                array.CopyTo(newArray, index: 0);
                array = newArray;
            }
            else if (array.Length > maxLength)
            {
                var newArray = new T[maxLength];

                Array.Copy(
                    sourceArray: array,
                    sourceIndex: 0,
                    destinationArray: newArray,
                    length: maxLength,
                    destinationIndex: 0);

                array = newArray;
            }
        }

        private void OnValidate()
        {
            ValidateArray(ref mainColors, MinColorCount, MaxColorCount);
            ValidateArray(
                ref additionalColors, minLength: mainColors.Length, maxLength: mainColors.Length);

            colorIndex %= mainColors.Length;
        }

        private void Awake()
        {
            var renderer = GetComponent<MeshRenderer>();
            Material material = renderer.material;

            matBindings.Bind(material);
        }

        private void OnMouseDown()
        {
            Color mainColor = mainColors[colorIndex];
            Color additionalColor = additionalColors[colorIndex];

            matBindings.MainColor.Set(mainColor);
            matBindings.AdditionalColor.Set(additionalColor);

            colorIndex = (colorIndex + 1) % mainColors.Length;
        }
    }
}