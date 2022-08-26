using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 4;
    public float runSpeed = 8;
    public float jumpPower = 10;
    public float mouse_x = 500;
    public float mouse_y = 500;
    public float max_angle = 70, min_angel = -60;

    public Transform cam;
    private Rigidbody rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }
    bool jumpCommand = false;
    float angle = 0;
    // Update is called once per frame
    void Update()
    {
        jumpCommand |= Input.GetButtonDown("Jump");
        var mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.rotation *= Quaternion.Euler(0, mouseInput.x * mouse_x * Time.deltaTime, 0);
        angle = Mathf.Clamp(angle - mouseInput.y * mouse_y * Time.deltaTime, -max_angle, -min_angel);
        cam.localRotation = Quaternion.Euler(angle, 0, 0);
    }
    private void FixedUpdate()
    {
        var motionInput = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        motionInput.x += rbody.velocity.x;
        motionInput.z += rbody.velocity.z;
        var speed = Input.GetButton("Fire3") ? runSpeed : moveSpeed;
        motionInput = Vector3.ClampMagnitude(motionInput, speed);

        motionInput.y = rbody.velocity.y;
        if(jumpCommand)
        {
            jumpCommand = false;
            motionInput.y = jumpPower;
        }
        rbody.velocity = motionInput;
    }
}
