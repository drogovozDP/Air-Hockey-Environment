using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody _rbStick;
    private float _force = 80f;
    public float handleSpeed = 14f;
    [HideInInspector]public Vector3 maxSpeed;

    // public string playerTagRed = "playerRed";
    // public string playerTagBlue = "playerBlue";

    private void Start()
    {
        _rbStick = GetComponentInChildren<Rigidbody>();
        maxSpeed = new Vector3(handleSpeed, 0, handleSpeed);
    }

    public void MoveStick(int keyPressed)
    {
        switch (keyPressed)
        {
            case 0:
                _rbStick.AddForce(-_force, 0, 0, ForceMode.Acceleration);
                break;
            case 1:
                _rbStick.AddForce(_force, 0, 0, ForceMode.Acceleration);
                break;
            case 2:
                _rbStick.AddForce(0, 0, -_force, ForceMode.Acceleration);
                break;
            case 3:
                _rbStick.AddForce(0, 0, _force, ForceMode.Acceleration);
                break;
        }    
    }

    private void FixedUpdate()
    {
        // bool isRed = CompareTag(playerTagRed);
        // bool isBlue = CompareTag(playerTagBlue);
        //
        // if (Input.GetKey(KeyCode.W) && isBlue || Input.GetKey(KeyCode.UpArrow) && isRed)
        //     if (Mathf.Abs(_rbStick.velocity.x) < maxSpeed.x)
        //         MoveStick(0);
        //
        // if (Input.GetKey(KeyCode.S) && isBlue || Input.GetKey(KeyCode.DownArrow) && isRed)
        //     if (_rbStick.velocity.x < maxSpeed.x)
        //         MoveStick(1);
        //
        // if (Input.GetKey(KeyCode.A) && isBlue || Input.GetKey(KeyCode.LeftArrow) && isRed)
        //     if (Mathf.Abs(_rbStick.velocity.z) < maxSpeed.z)
        //         MoveStick(2);
        //
        // if (Input.GetKey(KeyCode.D) && isBlue || Input.GetKey(KeyCode.RightArrow) && isRed)
        //     if (_rbStick.velocity.z < maxSpeed.z)
        //         MoveStick(3);
    }
}
