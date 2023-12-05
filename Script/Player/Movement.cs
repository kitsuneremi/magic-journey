using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private bool isJumping = false;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float moveAcceleration = 60f;
    [SerializeField] private LayerMask jumpableground;
    private float dirX;

    private float fallSpeedDampingThreshold;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        fallSpeedDampingThreshold = CameraManager.instance.fallSpeedDampingThreshold;

    }

    void Update()
    {
        Jump();

        if(rb.velocity.y < fallSpeedDampingThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        if(rb.velocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpedFromPlayerFalling = false;
            CameraManager.instance.LerpYDamping(false);
        }
    }

    private void FixedUpdate()
    {
        Run();
        anim.SetBool("isJump", isJumping);
    }

    void Run()
    {
        anim.SetBool("isRun", false);


        dirX = Input.GetAxis("Horizontal");

        if (dirX != 0f)
        {
/*            transform.localScale = new Vector3(dirX < 0 ? -1 : 1, 1, 1);*/
            transform.rotation = Quaternion.Euler(0, dirX < 0 ? 180 : 0, 0);
            float targetVelocity = dirX * moveSpeed;
            float acceleration = Mathf.Abs(rb.velocity.x - targetVelocity) < 0.05f ? 0f : moveAcceleration;
            rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, targetVelocity, acceleration * Time.deltaTime), rb.velocity.y);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);
            /*            transform.position += moveSpeed * Time.deltaTime * (dirX < 0 ? Vector3.left : Vector3.right);*/
        }


    }
    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0) && !isJumping)
        {
            isJumping = true;
            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0f, jumpHeight);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }

    }
    public void ResetJumping()
    {
        isJumping = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ground"))
        {
            /*anim.SetBool("idle", true);*/
            anim.SetBool("isJump", false);
            isJumping = false;
        }
    }

}
