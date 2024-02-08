using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSpeed = 3f;
    private float camLookSpeed = 3f;
    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

     void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _zMov = Input.GetAxisRaw("Vertical");
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _yMov = Input.GetAxisRaw("Rise");


        Vector3 movHorizontal = transform.right * _xMov;
        Vector3 movVertical = transform.forward * _zMov;
       
        //final movement vector
        Vector3 _velocity = (movHorizontal + movVertical).normalized * speed;

        //Apply Movement
        motor.Move(_velocity);

        //Calculate Rotation as a 3D vector (turning around)
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, yRot, 0) * lookSpeed;

        //Apply rotation
        motor.Rotate(_rotation);

        //calculate camera rotation as a 3D vector
        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _camRotation = new Vector3(xRot, 0f, 0f) * camLookSpeed;

        //Apply Camera Rotation
        motor.RotateCamera(_camRotation);

    }

}
