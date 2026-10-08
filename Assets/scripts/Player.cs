using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.leftStick.ReadValue() != Vector2.zero)
        {
            Movement();
        }
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            Debug.Log("You Pressed X/A");
            transform.position += new Vector3(0,10,0) * 5f *Time.deltaTime;
        }
    }

    private void Movement()
    {
        Vector2 input = Gamepad.current.leftStick.ReadValue();
        transform.position += new Vector3(input.x,0,input.y) * 5f * Time.deltaTime;
    }
}
