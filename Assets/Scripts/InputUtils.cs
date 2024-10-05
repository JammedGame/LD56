using UnityEngine;

namespace DefaultNamespace
{
    public static class InputUtils
    {
        public static bool ScreenToWorld(Vector2 screenPos, out Vector3 worldPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            Plane groundPlane = new Plane(Vector3.up, 0);
            if (groundPlane.Raycast(ray, out float enter) && enter < GameSettings.MaxInputRaycastDepth)
            {
                Debug.Log($"Enter {enter:F3}");
                worldPos = ray.GetPoint(enter);
                return true;
            }

            worldPos = Vector3.zero;
            return false;
        }
    }
}