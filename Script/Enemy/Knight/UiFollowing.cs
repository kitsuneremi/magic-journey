using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowing : MonoBehaviour
{
    [SerializeField] private GameObject enemy_body;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(enemy_body.transform.position.x, enemy_body.transform.position.y, enemy_body.transform.position.z);
    }
}
