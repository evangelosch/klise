using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform transform;

    void Start()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (transform.position.x > 2 || transform.position.x < -2 || transform.position.y > 1 || transform.position.y < -1)
        {
            Destroy(gameObject);

        }
    }
    public void destroyBullet()
    {
        Destroy(gameObject);
    }
}
