using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public GameObject target;
    public float minDis = 1000;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Arrow")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        }
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");
    }

    void Update()
    {
        float distance = Mathf.Sqrt((target.transform.position.x - transform.position.x) * (target.transform.position.x - transform.position.x) + (target.transform.position.y - transform.position.y) * (target.transform.position.y - transform.position.y));
        if(distance < minDis)
        {
            minDis = distance;
        }
    }
}
