using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentIndex = 0;

    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject enemy_body;

    private Animator anim;
    // 1 is patrol, 2 is attack
    public int state = 1;
    void Start()
    {
        anim = enemy_body.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetInteger("state") == 1)
        {
            enemy_body.transform.localScale = new Vector3(currentIndex == 0 ? -1 : 1, 1, 1);
            anim.SetInteger("state", 1);
            if (Mathf.Abs(waypoints[currentIndex].transform.position.x - enemy_body.transform.position.x) < .2f)
            {
                currentIndex++;

                if (currentIndex >= waypoints.Length)
                {
                    currentIndex = 0;
                }
            }

            enemy_body.transform.position = Vector2.MoveTowards(enemy_body.transform.position, waypoints[currentIndex].transform.position, Time.deltaTime * speed);
        }
    }
}
