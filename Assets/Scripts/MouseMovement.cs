using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    // start is called before the first frame update
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;
    void Start()
    {
        // lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // called once per frame
    void Update()
    {
        // mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // rotation around the x axis (look up and down)
        xRotation -= mouseY;

        // clap the rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        // rotation around the y axis (look left and right)
        yRotation += mouseX;

        // apply transformation
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

    }
}
