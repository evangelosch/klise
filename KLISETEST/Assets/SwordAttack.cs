using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour {

    public Collider2D swordCollider;
    Vector2 rightAttackOffset;
    public GameObject audioSource;
   
    public float damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
        audioSource = GameObject.FindGameObjectWithTag("parry");
    }

    public void AttackRight()
    {
        
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;

    }

    public void AttackLeft()
    {
       
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
    }

     public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("enemy hit");
                enemy.Health -= damage;
             
            }
            
        }
        else if (other.tag == "bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity *= -3f;
                audioSource.GetComponent<AudioSource>().Play();
                Debug.Log("bullet hit");
                

            }

        }
        
    }
}
