using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class Attack : MonoBehaviour
{

    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int maxBullet = 5;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Camera mainCamera;


    private bool refreshable = true;
    private int bulletRemaining;
    private Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        bulletRemaining = maxBullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletRemaining < maxBullet && refreshable)
        {
            bulletRemaining++;
            text.text = "bullet remaining: " + bulletRemaining;
        }
        if (Input.GetKeyDown(KeyCode.Q) && bulletRemaining > 0)
        {
            // vị trí con trỏ chuột trên màn hình
            Vector3 mousePositionScreen = Input.mousePosition;

            // Chuyển tọa độ chuột thành một điểm trong không gian trò chơi
            Vector3 mousePositionWorld = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, mainCamera.nearClipPlane));

            // Tính vector hướng giữa player và con trỏ chuột
            Vector3 directionToMouse = mousePositionWorld - spawnPoint.position;
            transform.localScale = new Vector3(directionToMouse.x < 0 ? -1 : 1, 1, 1);
            bulletRemaining -= 1;
            text.text = "bullet remaining: " + bulletRemaining;
            refreshable = false;
            if (bulletRemaining == 0)
            {
                Invoke(nameof(RefreshBullet), 2);
            }
            anim.SetTrigger("attack");
            var bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(directionToMouse.x, directionToMouse.y).normalized * bulletSpeed;
        }
    }
    public void RefreshBullet()
    {
        refreshable = true;
    }
}
