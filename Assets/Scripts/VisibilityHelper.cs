using UnityEngine;

class VisibilityHelper
{
    public static bool IsObjectVisible(Collider collider, Camera cam)
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(planes, collider.bounds))
            return true;
        else
            return false;
    }
}
