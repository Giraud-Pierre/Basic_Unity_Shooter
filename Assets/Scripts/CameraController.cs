using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float CameraSensitivity = 1f;
    
    private float CameraXPosition;
    private float CameraYPosition;
    private float realCameraSensitivity;


    private void MoveCamera()
    {
        CameraXPosition += -Input.GetAxis("Mouse Y")* realCameraSensitivity;
        CameraYPosition += Input.GetAxis("Mouse X") * realCameraSensitivity;

        CameraXPosition = Mathf.Clamp(CameraXPosition, -45f, 45f);

        transform.eulerAngles = new Vector3(CameraXPosition, CameraYPosition, CameraSensitivity);

    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CameraXPosition = 0f;
        CameraYPosition = 0f;
        transform.eulerAngles = new Vector3(CameraXPosition, CameraYPosition, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        realCameraSensitivity = CameraSensitivity * Time.deltaTime * 1000f;
        MoveCamera();
    }
}
