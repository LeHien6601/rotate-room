using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private float oldDegree = 0f;
    private List<PlatformEffector2D> platforms = new List<PlatformEffector2D>();
    private void Start()
    {
        foreach (PlatformEffector2D platform in GetComponentsInChildren<PlatformEffector2D>())
        {
            platforms.Add(platform);
        }
    }
    private void Update()
    {
        //Rotates all PlatformEffector2D based on camera's angle
        if (cam.transform.eulerAngles.z != oldDegree)
        {
            oldDegree = cam.transform.eulerAngles.z;
            platforms.ForEach(platform =>
            {
                platform.rotationalOffset = -oldDegree;
            });
        }
    }
}
