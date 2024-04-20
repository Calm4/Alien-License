using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorldDirection(Camera camera, Vector2 screenPosition)
    {
        Ray ray = camera.ScreenPointToRay(screenPosition);
        Plane plane = new Plane(Vector3.forward, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            return (worldPosition - camera.transform.position).normalized;
        }
        return Vector3.zero;
    }

}