using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Movment_vars
    [SerializeField]
    [Tooltip("How fast player moves")]
    private float move_speed;
    private Vector2 currdir;
    private Vector3 mousepos;
    private Vector2 looking;
    private float x_input;
    private float y_input;
    #endregion

    #region Unity_vars
    Rigidbody2D PlayerRB;

    #endregion

    #region Health_vars
    [SerializeField]
    [Tooltip("How much health the player should have")]
    private float max_health;
    private float curr_health;
    #endregion

    #region Attack_vars
    [SerializeField]
    [Tooltip("Amount of health the player should have")]
    private float damage;
    [SerializeField]
    [Tooltip("Fire rate")]
    private float fire_rate;
    private float last_fired;
    [SerializeField]
    [Tooltip("Holds the bullet prefab")]
    private Bullet bullet;
    [SerializeField]
    [Tooltip("How many enemies it hits before disaearing")]
    private int AmtPierced;
    [SerializeField]
    private float bullet_speed;
    #endregion

    #region Unity_funcs
    private void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        curr_health = max_health;
    }
    private void Update()
    {
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");
        move();
        pointing();

        if (Input.GetButton("Fire1"))
        {
            if(Time.time - last_fired > 1 / fire_rate)
            {
                Attack(); 
            }
        }

        
    }
    #endregion

    #region lookingfunc
    private void pointing()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        looking = new Vector2((mousepos.x - transform.position.x), (mousepos.y - transform.position.y));
        transform.up = looking;
    }
    #endregion

    #region movefunc
    private void move()
    {
        //based on x and y input decide how the player should move through their velocity
        // you may want to normilize the vector you make
        PlayerRB.velocity = new Vector2(x_input, y_input).normalized * move_speed;


    }
    #endregion

    #region Attack_funcs

    private void Attack()
    {
        last_fired = Time.time;
        Bullet clone = Instantiate(bullet, transform.position, Quaternion.identity);
        clone.damage = damage;
        clone.piercing = AmtPierced;
        clone.GetComponent<Rigidbody2D>().velocity = looking.normalized * bullet_speed;
    }
    #endregion

    #region Health_funcs
    public void TakeDamage(float dmg)
    {
        curr_health -= dmg;
        Debug.Log("curr_health");
        if(curr_health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
