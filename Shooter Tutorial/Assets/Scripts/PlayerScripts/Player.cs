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
    [SerializeField]
    private Vector2 last_visited;
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
    [SerializeField] Weapons CurrentWeapon;
    #region Unity_funcs
    private void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        curr_health = max_health;
        CurrentWeapon.last_fired = CurrentWeapon.fire_rate;
        transform.position = last_visited;
    }
    private void Update()
    {
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");
        move();
        pointing();
        CurrentWeapon.last_fired += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if(CurrentWeapon.last_fired > CurrentWeapon.fire_rate)
            {
                Attack(); 
                CurrentWeapon.last_fired = 0f;
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
        Debug.Log("live clean");
        CurrentWeapon.Attack(transform.position, looking);
    }
    #endregion

    #region Health_funcs
    public void TakeDamage(float dmg)
    {
        curr_health -= dmg;
        Debug.Log("curr_health");
        if(curr_health <= 0)
        {
            Start();
        }
    }
    #endregion

    public void SetSpawn(Vector2 checkpoint)
    {
        last_visited = checkpoint;
    }
}
