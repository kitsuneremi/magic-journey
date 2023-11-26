using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastSkill : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Transform spawn_point;
    [SerializeField] private GameObject fireball_prefab;
    [SerializeField] private float fireball_speed;

    private Animator anim;

    void Start()
    {
        player = GameObject.Find("Wizard");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CastSkill()
    {
        Vector3 directionToPlayer = player.transform.position - spawn_point.position;
        this.transform.localScale = new Vector3(directionToPlayer.x < 0 ? -1 : 1, 1, 1);
        var fireball = Instantiate(fireball_prefab, spawn_point.position, spawn_point.rotation);
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        fireball.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(directionToPlayer.x, directionToPlayer.y).normalized * fireball_speed;

    }
}
