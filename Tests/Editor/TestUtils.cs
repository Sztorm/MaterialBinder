using UnityEngine;

namespace Sztorm.MaterialBinder.Tests
{
    public static class TestUtils
    {
        public static bool AreApproximatelyEqual(Vector2 left, Vector2 right)
        {
            const float SqrEpsilon = Vector2.kEpsilon * Vector2.kEpsilon;

            return (left - right).sqrMagnitude < SqrEpsilon;
        }

        public static bool AreApproximatelyEqual(Vector3 left, Vector3 right)
        {
            const float SqrEpsilon = Vector3.kEpsilon * Vector3.kEpsilon;

            return (left - right).sqrMagnitude < SqrEpsilon;
        }

        public static bool AreApproximatelyEqual(Vector4 left, Vector4 right)
        {
            const float SqrEpsilon = Vector4.kEpsilon * Vector4.kEpsilon;

            return (left - right).sqrMagnitude < SqrEpsilon;
        }

        public static bool AreApproximatelyEqual(Color left, Color right)
        {
            const float SqrEpsilon = Vector4.kEpsilon * Vector4.kEpsilon;
            Vector4 leftAsV4 = new Vector4(left.r, left.g, left.b, left.a);
            Vector4 rightAsV4 = new Vector4(right.r, right.g, right.b, right.a);

            return (leftAsV4 - rightAsV4).sqrMagnitude < SqrEpsilon;
        }
    }
}