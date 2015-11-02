using UnityEngine;
using System;

[Flags]
public enum Actions
{
    None = 0,

    MoveRight = (1 << 0),
    MoveLeft = (1 << 1),
    StopHorizontal = (1 << 2),

    MoveUp = (1 << 3),
    MoveDown = (1 << 4),
    StopVertical = (1 << 5),
}

public class ActorBehaviour : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 20;


    public new Rigidbody rigidbody { get; private set; }

    Vector3 velocity;

    //impulse available for the current jump
    float jumpImpulse;

    bool isJumping;
    bool isGrounded;
    
    [NonSerialized]
    public bool justTeleported = false;

    // FPS Variable
    public bool isFPSEnabled;
    Camera PlayerCamera;
    public float FPSSensitivity = 3f;
    private float currentCameraRotationX = 0f;
    private float cameraRotationLimit = 85f;

    public GameObject BulletPrefab;
    public float BulletSpeed = 20.0f;
    public Transform WeaponFirePoint;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        //FPS Objects
        PlayerCamera = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            rigidbody.AddForce(Vector3.up * (jumpImpulse * .2f), ForceMode.VelocityChange);
            jumpImpulse *= .8f;
        }

        if (isGrounded)
        {
            jumpImpulse = jumpForce;
        }

        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Platform")
        {

            // is only grounded if touched the ground from the top (positive normal y component)
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Platform")
        {
            isGrounded = false;
        }
    }

    public void PerformActions(float horizontal, float vertical, float camHorizontal, float camVertical, bool jump = false,  bool fireWeapon = false)
    {
        // Test for input
        if (Mathf.Abs(horizontal) > Mathf.Epsilon || Mathf.Abs(vertical) > Mathf.Epsilon)
        {
            //Test for kind of controls
            if (isFPSEnabled)
            {// FPS controls
                velocity = transform.forward * vertical * speed + transform.right * horizontal * speed;                
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0.0f, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, 0.0f);
                velocity = transform.forward * speed;
            }
        }
        else
        { // No input means we must stop
            velocity = Vector3.zero;
        }

        this.isJumping = jump;

        if (isFPSEnabled && PlayerCamera!= null)
        {
            UpdateFPSCamera(camHorizontal, camVertical);
        }      

        if (fireWeapon)
        {
            ShootWeapon();
        }
    }

    public void setSpeed(float value)
    {
        speed = value;
    }

    public void setJumpForce(float value)
    {
        jumpForce = value;
    }
    public void SetFpsControls(bool value)
    {
        isFPSEnabled = value;
    }

    public void UpdateFPSCamera( float camHorizontal, float camVertical)
    {
        //Calculate rotation as a 3D vector (turning around)
        Vector3 rotation = new Vector3(0f, camHorizontal, 0f) * FPSSensitivity;

        //Calculate camera rotation as a 3D vector (turning around)
        float cameraRotationX = camVertical * FPSSensitivity;

        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotation));
        if (this.gameObject.tag == "Ghost")
            Debug.Log(rigidbody.rotation.eulerAngles);
        if (PlayerCamera != null)
        {
            // Set our rotation and clamp it
            currentCameraRotationX += cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply our rotation to the transform of our camera
            PlayerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

    public void ShootWeapon()
    {
        GameObject Projectile = (GameObject)Instantiate(BulletPrefab, WeaponFirePoint.position, Quaternion.identity);

        // Fires projectile.
        Projectile.GetComponent<Rigidbody>().velocity = WeaponFirePoint.forward * BulletSpeed;
    }
}
