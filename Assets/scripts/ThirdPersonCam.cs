using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{

    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform aimPoint;

    public CameraStyle currentStyle;
    public GameObject basicCam;
    public GameObject aimCam;

    public enum CameraStyle
    {
        Basic,
        Aim
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.leftTrigger.IsActuated()) SwitchCam(CameraStyle.Aim);
        else SwitchCam(CameraStyle.Basic);

        if (currentStyle == CameraStyle.Basic)
        {
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;


            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;


            if (inputDir != Vector3.zero)
            {
                playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

            }
        }
        else if (currentStyle == CameraStyle.Aim) 
        {
            Vector3 dirToAimPoint = aimPoint.position - new Vector3(transform.position.x, aimPoint.position.y, transform.position.z);
            orientation.forward = dirToAimPoint.normalized;

            playerObject.forward = Vector3.Slerp(playerObject.forward, dirToAimPoint.normalized, Time.deltaTime * rotationSpeed);
        }
    }
    private void SwitchCam(CameraStyle newStyle)
    {

        if (newStyle == CameraStyle.Basic)
        {
            aimCam.GetComponent<CinemachineCamera>().Priority = 0;

            //basicCam.SetActive(true);
            //aimCam.transform.position = basicCam.transform.position;
        }


        if (newStyle == CameraStyle.Aim)
        {
            aimCam.GetComponent<CinemachineCamera>().Priority = 2;

           //aimCam.SetActive(true);
          //basicCam.transform.position = aimCam.transform.position;

        }
        currentStyle = newStyle;
    }
}
