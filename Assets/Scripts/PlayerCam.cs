using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        //get mouse input
//        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
//        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
//
//        yRotation += mouseX;
//        xRotation -= mouseY;
//
//        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
//
//        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
