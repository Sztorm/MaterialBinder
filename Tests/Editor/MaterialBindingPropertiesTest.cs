using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

namespace Sztorm.MaterialBinder.Tests
{
    using static TestUtils;
    using TestedMatBindings = ShaderGraphs_Graph_TestAllPropsMaterialBindings;

    [TestFixture]
    public class MaterialBindingPropertiesTest
    {
        private Material testedMaterial;
        private TestedMatBindings testedMaterialBindings;

        [SetUp]
        public void SetUp()
        {
            testedMaterial = AssetDatabase.LoadAssetAtPath<Material>(
                assetPath: "Assets/Samples/SampleURP/Materials/Mat_TestAllProps.mat");
            testedMaterialBindings.Bind(testedMaterial);
        }

        [TearDown]
        public void TearDown()
        {
            testedMaterialBindings.Unbind();
        }

        private static void TestScalarBinding(
            ScalarBinding binding,
            (bool V0, float V1, int V2) values,
            (bool V0, float V1, int V2) expected)
        {
            (bool V0, float V1, int V2) actual;
            binding.Set(values.V0);
            actual.V0 = binding.AsBool;

            binding.Set(values.V1);
            actual.V1 = binding.AsFloat;

            binding.Set(values.V2);
            actual.V2 = binding.AsInt;

            Assert.AreEqual(expected.V0, actual.V0);
            Assert.That(Mathf.Approximately(expected.V1, actual.V1));
            Assert.AreEqual(expected.V2, actual.V2);
        }

        private static void TestVectorBinding(
            VectorBinding binding,
            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) values,
            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) expected)
        {  
            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) actual;
            binding.Set(values.V0);
            actual.V0 = binding.AsVector2;

            binding.Set(values.V1);
            actual.V1 = binding.AsVector3;

            binding.Set(values.V2);
            actual.V2 = binding.AsVector4;

            binding.Set(values.V3);
            actual.V3 = binding.AsColor;

            Assert.That(AreApproximatelyEqual(expected.V0, actual.V0));
            Assert.That(AreApproximatelyEqual(expected.V1, actual.V1));
            Assert.That(AreApproximatelyEqual(expected.V2, actual.V2));
            Assert.That(AreApproximatelyEqual(expected.V3, actual.V3));
        }

        [Test]
        public void TestScalarBinding_Bool()
        {
            (bool V0, float V1, int V2) expected;
            expected.V0 = true;
            expected.V1 = 1F;
            expected.V2 = 1;

            (bool V0, float V1, int V2) values;
            values.V0 = true;
            values.V1 = 1F;
            values.V2 = 1;

            TestScalarBinding(testedMaterialBindings.Boolean, values, expected);
        }

        [Test]
        public void TestScalarBinding_Float()
        {
            (bool V0, float V1, int V2) expected;
            expected.V0 = true;
            expected.V1 = 1F;
            expected.V2 = 1;

            (bool V0, float V1, int V2) values;
            values.V0 = true;
            values.V1 = 1F;
            values.V2 = 1;

            TestScalarBinding(testedMaterialBindings.Vector1, values, expected);
        }

        [Test]
        public void TestVectorBinding_Vector2()
        {
            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) expected;
            expected.V0 = new Vector2(1F, 2F);
            expected.V1 = new Vector3(1F, 2F, 3F);
            expected.V2 = new Vector4(1F, 2F, 3F, 4F);
            expected.V3 = new Color(1F, 2F, 3F, 4F);

            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) values;
            values.V0 = new Vector2(1F, 2F);
            values.V1 = new Vector3(1F, 2F, 3F);
            values.V2 = new Vector4(1F, 2F, 3F, 4F);
            values.V3 = new Color(1F, 2F, 3F, 4F);

            TestVectorBinding(testedMaterialBindings.Vector2, values, expected);
        }

        [Test]
        public void TestVectorBinding_Vector3()
        {
            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) expected;
            expected.V0 = new Vector2(1F, 2F);
            expected.V1 = new Vector3(1F, 2F, 3F);
            expected.V2 = new Vector4(1F, 2F, 3F, 4F);
            expected.V3 = new Color(1F, 2F, 3F, 4F);

            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) values;
            values.V0 = new Vector2(1F, 2F);
            values.V1 = new Vector3(1F, 2F, 3F);
            values.V2 = new Vector4(1F, 2F, 3F, 4F);
            values.V3 = new Color(1F, 2F, 3F, 4F);

            TestVectorBinding(testedMaterialBindings.Vector3, values, expected);
        }

        [Test]
        public void TestVectorBinding_Vector4()
        {
            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) expected;
            expected.V0 = new Vector2(1F, 2F);
            expected.V1 = new Vector3(1F, 2F, 3F);
            expected.V2 = new Vector4(1F, 2F, 3F, 4F);
            expected.V3 = new Color(1F, 2F, 3F, 4F);

            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) values;
            values.V0 = new Vector2(1F, 2F);
            values.V1 = new Vector3(1F, 2F, 3F);
            values.V2 = new Vector4(1F, 2F, 3F, 4F);
            values.V3 = new Color(1F, 2F, 3F, 4F);

            TestVectorBinding(testedMaterialBindings.Vector4, values, expected);
        }

        [Test]
        public void TestVectorBinding_Color()
        {
            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) expected;
            expected.V0 = new Vector2(1F, 2F);
            expected.V1 = new Vector3(1F, 2F, 3F);
            expected.V2 = new Vector4(1F, 2F, 3F, 4F);
            expected.V3 = new Color(1F, 2F, 3F, 4F);

            (Vector2 V0, Vector3 V1, Vector4 V2, Color V3) values;
            values.V0 = new Vector2(1F, 2F);
            values.V1 = new Vector3(1F, 2F, 3F);
            values.V2 = new Vector4(1F, 2F, 3F, 4F);
            values.V3 = new Color(1F, 2F, 3F, 4F);

            TestVectorBinding(testedMaterialBindings.Color, values, expected);
        }

        [Test]
        public void TestBoolKeywordBinding_SetKeyword()
        {
            BoolKeywordBinding binding = testedMaterialBindings.BooleanKeyword;
            bool expected = true;
            bool value = true;

            binding.SetKeyword(value);
            bool actual = binding.IsEnabled;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestBoolKeywordBinding_EnableKeyword()
        {
            BoolKeywordBinding binding = testedMaterialBindings.BooleanKeyword;
            bool expected = true;

            binding.EnableKeyword();
            bool actual = binding.IsEnabled;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestBoolKeywordBinding_DisableKeyword()
        {
            BoolKeywordBinding binding = testedMaterialBindings.BooleanKeyword;
            bool expected = false;

            binding.DisableKeyword();
            bool actual = binding.IsEnabled;

            Assert.AreEqual(expected, actual);
        }
    }
}
