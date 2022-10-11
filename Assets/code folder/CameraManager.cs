using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Cameras cameras;
    public Transform target;

    public Vector3 offset;
    public float damping;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        cameras = Cameras.SpectorCamera;
    }

    void FixedUpdate()
    {
        if (cameras == Cameras.SpectorCamera) return;
        if(cameras == Cameras.PlayerCamera)
        {
            Vector3 movePosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        }
    }
    public void ChangeCamera(Cameras _cameras)
    {
        if (_cameras == Cameras.PlayerCamera)
        {
            cameras = _cameras;
        }
    }
}
public enum Cameras
{
    SpectorCamera,
    PlayerCamera,
    OtherCamera
}
