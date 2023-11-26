using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private Slider ease;
    [SerializeField] private PlayerStat stat;
    [SerializeField] private TextMeshProUGUI hp_value_ui;
    private Animator anim;
    private bool alive = true;
    void Start()
    {
        bar.maxValue = stat.playerData.health;
        ease.maxValue = stat.playerData.health;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bar.value = stat.Health;
        hp_value_ui.text = stat.Health + "/" + stat.playerData.health;
        if (ease.value != bar.value)
        {
            ease.value = Mathf.Lerp(ease.value, stat.Health, 0.05f);
        }
        if(stat.Health <= 0)
        {
            if (alive)
            {
                alive = false;
                anim.SetTrigger("die");
                RestartLevel();
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CancelInvoke(nameof(TakeDmgToxic));
        if (collision.gameObject.name.Equals("Toxic"))
        {
            InvokeRepeating(nameof(TakeDmgToxic), 1, 2);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke(nameof(TakeDmgToxic));
    }

    private void TakeDmgToxic()
    {
        if(stat.playerData.health > 0)
        {
            stat.Health -= 100;
        }
    }


    public void RestartLevel()
    {
        Invoke(nameof(Restart), 3f);
        
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ResetAnimation()
    {
        anim.SetBool("isLookUp", false);
        anim.SetBool("isRun", false);
        anim.SetBool("isJump", false);
        anim.ResetTrigger("hurt");
    }


}
