using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    private State state;


    private void Start()
    {
        camera = GetComponent<Camera>();
        state = State.Idle;
    }

    public void IncreaseTheEffectOfAcceleration()
    {
        if (camera.fieldOfView < 73)
        {
            camera.fieldOfView += 25 * Time.deltaTime;
            state = State.Acceleration;
        }
    }

    public void ChangeReset()
    {
        state = State.Idle;
    }

    public void ResetCamera()
    {
        if(camera.fieldOfView > 65)
        {
            camera.fieldOfView -= 15 * Time.deltaTime;
        }
    }

    private void Update()
    {
        if (state == State.Idle)
            ResetCamera();
    }

    public enum State
    {
        Acceleration,
        Idle
    }
}
