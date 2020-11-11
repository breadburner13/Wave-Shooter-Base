using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Damage_variables
    public float damage;
    [Tooltip("Must be at least 1 to kill an enemy")]
    public int piercing;
    #endregion
    private Vector3 StartingPos;

    #region Unity_funcs
    private void Start()
    {
        StartingPos = transform.position;
    }

    private void Update()
    {
        if(Vector3.Distance(StartingPos, transform.position)> 10f)
        {
            Debug.Log("Those long days passing by the door like late summer they slowly fade away");
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region Damag_funcs
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy1>().TakeDamage(damage);
            
        }
        piercing--;
        if(piercing <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
