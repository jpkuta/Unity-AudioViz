using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*allow the player to look around from the base using mouse controls */
public class playerLook : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationY = 0F;
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    public bool fly;//enable/disable ability to move the player
    [SerializeField]
    private float speed = 5f;

    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);


        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }

        float _zMov = Input.GetAxisRaw("Vertical");
        float _xMov = Input.GetAxisRaw("Horizontal");


        Vector3 movHorizontal = transform.right * _xMov;
        Vector3 movVertical = transform.forward * _zMov;
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

    }
    private void FixedUpdate()
    {
        if (fly)
        {
            PerformMovement();
        }
    }

    void Start()
    {
        // Make the rigid body not change rotation
        if (!fly)
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        if (fly)
        {
            rb = GetComponent<Rigidbody>();
        }
    }


    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

}
