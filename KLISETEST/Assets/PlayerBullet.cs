using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rigidBody.position.x > 2 || rigidBody.position.x < -2 || rigidBody.position.y > 1 || rigidBody.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
    public void destroyBullet()
    {
        Destroy(gameObject);
    }

}
