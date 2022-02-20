using UnityEngine;

public class JumpyMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform freeCamera;

    public float speed = 6f;
    public float turn = 0.1f;
    private float turnVelocity;

    public float hopSpeed = 3f;
    public float jumpSpeed = 6f;
    public float gravity = 9.8f;
    private float vSpeed = 0f;

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var direction = new Vector3(h, 0f, v).normalized;
        var moveSpeed = new Vector3();

        vSpeed -= gravity * Time.deltaTime;
        if (direction.magnitude >= 0.1f)
        {
            if (controller.isGrounded)
            {
                vSpeed = hopSpeed;
                if (Input.GetKey(KeyCode.Space))
                    vSpeed = jumpSpeed;
            }

            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + freeCamera.eulerAngles.y;
            var movingAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turn);

            transform.rotation = Quaternion.Euler(0f, movingAngle, 0f);

            var moveDirection = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;

            moveSpeed = moveDirection * speed;
            
        }

        moveSpeed.y = vSpeed;
        controller.Move(moveSpeed * Time.deltaTime);
    }
}
