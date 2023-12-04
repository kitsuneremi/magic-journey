using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigWallStop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ground") || collision.gameObject.CompareTag("Wall"))
        {
            transform.parent.gameObject.GetComponent<PigAttack>().WallStop();
        }
    }
}
