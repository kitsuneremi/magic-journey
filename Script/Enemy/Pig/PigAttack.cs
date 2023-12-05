using System.Collections;
using UnityEngine;

public class PigAttack : MonoBehaviour
{
    public float skillCooldownTime = 5f;
    [SerializeField] private float moveSpeed = 3f;

    private float skillCooldown = 0f;
    private GameObject player;
    private bool canFollow = false;
    private bool isAttacking = false;
    private bool canMove = true;
    private Animator animator;
    private Vector3 direction;
    private EnemyStat enemyStat;
    private Rigidbody2D rb;

    private bool engange = false;
    private bool hasJumped = false;

    void Start()
    {
        player = GameObject.Find("Wizard");
        animator = GetComponent<Animator>();
        enemyStat = GetComponent<EnemyStat>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        engange = animator.GetBool("engange");
        if (canFollow)
        {
            // Khi có thể theo dõi player, kiểm tra cooldown
            if (skillCooldown > 0)
            {
                skillCooldown -= Time.deltaTime;
            }

            // Nếu cooldown đã hết và không phải đang tấn công, thực hiện hành động tấn công
            if (skillCooldown <= 0 && !isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
        if (engange && !hasJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, 8f);
            hasJumped = true;
        }
    }

    public void CanFollow()
    {
        canFollow = true;
    }

    public void CantFollow()
    {
        canFollow = false;
        isAttacking = false;
        StopAllCoroutines(); // Ngừng tất cả các coroutine đang chạy nếu chuyển sang trạng thái chờ
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        canMove = true;
        direction = (player.transform.position - transform.position).normalized;

        while (Vector2.Distance(transform.position, player.transform.position) > 0.1f && canFollow && canMove)
        {
            animator.SetTrigger("run");

            // Di chuyển theo hướng đã lưu
            transform.localScale = new Vector3(direction.x < 0 ? 1 : -1, 1, 1);
            // Nếu quái vật đang rơi xuống và đến gần mặt đất, đặt velocity.y về 0 và đặt lại hasJumped

            // Di chuyển bằng cách thay đổi vị trí
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            yield return null;
        }

        isAttacking = false;
        yield return new WaitForSeconds(skillCooldownTime);
    }




    public void WallStop()
    {
        if(canMove = true && isAttacking)
        {
            Debug.Log("Wall stop");
            animator.ResetTrigger("run");
            animator.SetTrigger("idle");
            canMove = false;
            isAttacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            collision.gameObject.GetComponent<PlayerStat>().Health -= enemyStat.Attack;
        }
    }

    public void ResetJump() {
        hasJumped = false;
    }
}
