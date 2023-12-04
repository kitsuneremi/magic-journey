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

    void Start()
    {
        player = GameObject.Find("Wizard");
        animator = GetComponent<Animator>();
        enemyStat = GetComponent<EnemyStat>();
    }

    void Update()
    {
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

        // Lấy hướng từ quái vật đến player và lưu vào biến direction
        direction = (player.transform.position - transform.position).normalized;

        // Thực hiện hành động tấn công (lao vào player)
        while (Vector2.Distance(transform.position, player.transform.position) > 0.1f && canFollow && canMove)
        {
            animator.SetTrigger("run");
            // Di chuyển theo hướng đã lưu
            transform.localScale = new Vector3(direction.x < 0 ? 1 : -1, 1, 1);
            /*transform.Translate(new Vector3(direction.x * 10f, transform.position.y, transform.position.z) * Time.deltaTime);*/
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(direction.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        skillCooldown = skillCooldownTime;
        canFollow = true;

        yield return new WaitForSeconds(skillCooldownTime);
    }

    public void WallStop()
    {
        Debug.Log("Wall stop");
        animator.ResetTrigger("run");
        animator.SetTrigger("idle");
        canMove = false;
        skillCooldown = skillCooldownTime;
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard"))
        {
            collision.gameObject.GetComponent<PlayerStat>().Health -= enemyStat.Attack;
        }
    }
}
