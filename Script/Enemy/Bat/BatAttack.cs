using System.Collections;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public float skillCooldownTime = 5f;
    private float skillCooldown = 0f;
    private GameObject player;
    private bool canFollow = false;
    private bool isAttacking = false;
    public Animator animator;
    void Start()
    {
        player = GameObject.Find("Wizard");
        animator = GetComponent<Animator>();
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

        // Thực hiện hành động tấn công (lao vào player)
        while (Vector2.Distance(transform.position, player.transform.position) > 0.1f && animator.GetBool("isFlying"))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 4f * Time.deltaTime);
            yield return null;
        }

        // Tấn công xong, chuyển sang trạng thái chờ
        canFollow = false;

        // Đợi một khoảng thời gian trước khi bay ngẫu nhiên
        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

        // Bay ngẫu nhiên xung quanh player
        float randomX = Random.Range(-2f, 2f);
        float randomY = Random.Range(1f, 2f);
        Vector2 randomPosition = new Vector2(player.transform.position.x + randomX, player.transform.position.y + randomY);

        // Di chuyển đến vị trí ngẫu nhiên
        while (Vector2.Distance(transform.position, randomPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, randomPosition, 4f * Time.deltaTime);
            yield return null;
        }

        // Reset cooldown và chuyển sang trạng thái có thể theo dõi để tấn công tiếp theo
        skillCooldown = skillCooldownTime;
        canFollow = true;
        isAttacking = false;
    }
}
