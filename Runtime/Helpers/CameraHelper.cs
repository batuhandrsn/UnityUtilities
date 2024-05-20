using System.Linq;
using UnityEngine;

public static class CameraHelper
{
    /// <summary>
    /// Gets the active camera based on the highest depth and enabled state.
    /// </summary>
    /// <returns>The active camera if available, otherwise null.</returns>
    public static Camera GetActiveCamera()
    {
        var allCameras = Camera.allCameras;
        if (allCameras == null || allCameras.Length == 0) return null;

        return allCameras
            .Where(cam => cam.enabled)
            .OrderByDescending(cam => cam.depth)
            .FirstOrDefault();
    }
}