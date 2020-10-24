using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons 
{
    public void ShotgunAttack(Vector3 position, Vector2 looking)
    {
        last_fired = Time.time;
        Bullet clone = Instantiate(BulletType, position, Quaternion.identity);
        clone.damage = damage;
        clone.GetComponent<Rigidbody2D>().velocity = looking.normalized * bullet_speed;
        Bullet clone2 = Instantiate(BulletType, position, Quaternion.identity);
        clone2.damage = damage;
        clone2.GetComponent<Rigidbody2D>().velocity = Rotate(looking.normalized, 30) * bullet_speed;
        Bullet clone3 = Instantiate(BulletType, position, Quaternion.identity);
        clone3.damage = damage;
        clone3.GetComponent<Rigidbody2D>().velocity = Rotate(looking.normalized, -30) * bullet_speed;
    }
}
