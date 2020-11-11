using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : Bullet
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Can't get my mind out of those memories");
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Still music keeps on turning me from the words that hurt my soul");
            collision.GetComponent<Player>().TakeDamage(damage);
            
        }
        piercing--;
        if(piercing <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
