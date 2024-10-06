using UnityEngine;

namespace DefaultNamespace
{
    public static class MathUtils
    {
        public static bool IsPointInsideEllipse(Vector2 center, Vector2 size, Vector2 point)
        {
            float p = Mathf.Pow((point.x - center.x), 2) / Mathf.Pow(size.x, 2) + (Mathf.Pow((point.y - center.y), 2) / Mathf.Pow(size.y, 2));
            return p < 1f;
        }

        public static Vector2 ToVector2XZ(this Vector3 vec3)
        {
            return new Vector2(vec3.x, vec3.z);
        }
    }
}