using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastSkill : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject skillPrefab1;
    [SerializeField] private TextMeshProUGUI skill_1_cooldown_ui;
    [SerializeField] private Image skill_1_cooldown_image;
    
    [SerializeField] private GameObject skillPrefab2;
    [SerializeField] private TextMeshProUGUI skill_2_cooldown_ui;
    [SerializeField] private Image skill_2_cooldown_image;

/*    [SerializeField] private GameObject skillPrefab3;

    [SerializeField] private GameObject skillPrefab4;*/

    [SerializeField] private Camera mainCamera;
    
    public PlayerStat stat;
    public GameObject conditionTextPrefab;

    public float skill_1_cooldown;
    public float skill_2_cooldown;
    public float skill_3_cooldown;
    public float skill_4_cooldown;

    private float skill_1_current_cooldown;
    private float skill_2_current_cooldown;
    private float skill_3_current_cooldown;
    private float skill_4_current_cooldown;

    public float skill_1_consume = 40f;
    public float skill_2_consume = 120f;
    public float skill_3_consume;
    public float skill_4_consume;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        ReduceCooldown();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(skill_1_current_cooldown <= 0f && stat.Mana >= skill_1_consume)
            {
                Vector3 mousePositionScreen = Input.mousePosition;
                Vector3 mousePositionWorld = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, mainCamera.nearClipPlane));
                Vector3 directionToMouse = mousePositionWorld - spawnPoint.position;
                transform.localScale = new Vector3(directionToMouse.x < 0 ? -1 : 1, 1, 1);
                skill_1_current_cooldown = skill_1_cooldown;
                stat.Mana -= skill_1_consume;
                anim.SetTrigger("attack");
                var bullet1 = Instantiate(skillPrefab1, spawnPoint.position, spawnPoint.rotation);
                float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
                bullet1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bullet1.GetComponent<Rigidbody2D>().velocity = new Vector2(directionToMouse.x, directionToMouse.y).normalized * 20f;

            }
            else if(skill_1_current_cooldown > 0f)
            {
                conditionTextPrefab.GetComponent<TextMeshPro>().color = Color.white;
                conditionTextPrefab.GetComponent<TextMeshPro>().text = "skill on cooldown";
                var text = Instantiate(conditionTextPrefab, transform.position, Quaternion.identity);
                Destroy(text, .5f);
            }else if(stat.Mana < skill_1_consume)
            {
                conditionTextPrefab.GetComponent<TextMeshPro>().color = Color.cyan;
                conditionTextPrefab.GetComponent<TextMeshPro>().text = "not enough mana";
                var text = Instantiate(conditionTextPrefab, transform.position, Quaternion.identity);
                Destroy(text, .5f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (skill_2_current_cooldown <= 0f && stat.Mana >= skill_2_consume)
            {
                Vector3 mousePositionScreen = Input.mousePosition;
                Vector3 mousePositionWorld = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, mainCamera.nearClipPlane));
                Vector3 directionToMouse = mousePositionWorld - spawnPoint.position;
                transform.localScale = new Vector3(directionToMouse.x < 0 ? -1 : 1, 1, 1);
                skill_2_current_cooldown = skill_2_cooldown;
                stat.Mana -= skill_2_consume;
                anim.SetTrigger("attack");
                var bullet2 = Instantiate(skillPrefab2, spawnPoint.position, spawnPoint.rotation);
                float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
                bullet2.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(directionToMouse.x, directionToMouse.y).normalized * 20f;

            }
            else if (skill_2_current_cooldown > 0f)
            {
                conditionTextPrefab.GetComponent<TextMeshPro>().color = Color.white;
                conditionTextPrefab.GetComponent<TextMeshPro>().text = "skill on cooldown";
                var text = Instantiate(conditionTextPrefab, transform.position, Quaternion.identity);
                Destroy(text, .5f);
            }
            else if (stat.Mana < skill_2_consume)
            {
                conditionTextPrefab.GetComponent<TextMeshPro>().color = Color.cyan;
                conditionTextPrefab.GetComponent<TextMeshPro>().text = "not enough mana";
                var text = Instantiate(conditionTextPrefab, transform.position, Quaternion.identity);
                Destroy(text, .5f);
            }
        }
    }


    void ReduceCooldown()
    {
        // cooldown skill 1
        if(skill_1_current_cooldown <= 0f)
        {
            skill_1_cooldown_image.color = Color.clear;
            skill_1_cooldown_ui.text = "";
            skill_1_current_cooldown = 0;
        }
        else if(skill_1_current_cooldown > 0)
        {
            skill_1_cooldown_ui.text = skill_1_current_cooldown.ToString("F1");
            skill_1_cooldown_image.color = new Color(87/255f, 40/255f, 40/255f, 208/255f);
            skill_1_current_cooldown -= Time.deltaTime;
        }

        // cooldown skill 2
        if (skill_2_current_cooldown <= 0f)
        {
            skill_2_cooldown_image.color = Color.clear;
            skill_2_cooldown_ui.text = "";
            skill_2_current_cooldown = 0;
        }
        else if (skill_2_current_cooldown > 0)
        {
            skill_2_current_cooldown -= Time.deltaTime;
            skill_2_cooldown_ui.text = skill_2_current_cooldown.ToString("F1");
            skill_2_cooldown_image.color = new Color(87 / 255f, 40 / 255f, 40 / 255f, 208 / 255f);
        }
    }
}
