using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    public float upperCamBound = 85f;
    public float lowerCambound = -75f;

    public void ProcessCam(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= mouseY * ySensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, lowerCambound, upperCamBound);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX * xSensitivity * Time.deltaTime);
    }
}
