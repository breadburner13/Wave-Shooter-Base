using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapons : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Amount of health the player should have")]
    public float damage;
    [SerializeField]
    [Tooltip("Fire rate")]
    public float fire_rate;
    public float last_fired = 0f;
    [SerializeField] public Bullet BulletType;
    [SerializeField] public string Name;
    [SerializeField] public float ReloadTime;
    [SerializeField] public float bullet_speed;
    public virtual void Attack(Vector3 position, Vector2 looking)
    {
        Debug.Log("shit eat");
        //last_fired = Time.time + fire_rate;
        Bullet clone = Instantiate(BulletType, position, Quaternion.identity);
        clone.damage = damage;
        clone.GetComponent<Rigidbody2D>().velocity = looking.normalized * bullet_speed;
    }
    public Vector2 Rotate(Vector2 v, float delta)
    {
        return new Vector2(v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta), v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta));
    }
}


