using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy1 : MonoBehaviour
{
    #region physics_vars
    private Rigidbody2D enemyRB;
    private Player player;
    #endregion

    #region attack_vars
    // something to hold our enemies damage
    [SerializeField]
    [Tooltip("Enemy damage to player")]
    private float dmg;
    // an attack timer
    private float att_timer;

    //expected attack delay var/.
    [SerializeField]
    [Tooltip("Delay between enemy attacks")]
    private float att_delay;

    private Player pl;

    #endregion

    #region Movment_var
    // speed of the enemy
    [SerializeField]
    [Tooltip("Speed of enemy")]
    private float move_speed;

    

    Vector2 direction;

    public float nextWaypointDistance = 0f;
    Path path;
    int currWaypoint;
    bool reachedEndofPath = false;

    Seeker seeker;
    #endregion

    #region Health_vars
    [SerializeField]
    private float MaxHealth;
    private float CurrHealth;
    #endregion


    #region Unity_funcs
    private void Awake()
    {
        // We need to get our RigidBody and seeker Components
        enemyRB = GetComponent<Rigidbody2D>();
        //set our max helath
        CurrHealth = MaxHealth;
        
    }
    private void Start()
    {
        
        // We need to find the player so we can move toward them
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 1f);
        
    }

    private void Update()
    {
        look();
        if(att_timer > 0)
        {
            att_timer -= Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        

        if(path == null)
        {
            return;
        }

        Move();

       
    }
    #endregion

    #region move_funcs

    void UpdatePath()
    {

        if (seeker.IsDone()) {
            seeker.StartPath(enemyRB.position, player.transform.position, OnPathComplete);
        }
    }

    private void look()
    {
        
        transform.up = (Vector2) player.transform.position - enemyRB.position;
        
        
    } 

    private void Move()
    {
        if(currWaypoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
        } else
        {
            reachedEndofPath = false;
        }

        direction = ((Vector2)path.vectorPath[currWaypoint] - enemyRB.position).normalized;
        // use direction to influence the enemies velocity
        //Debug.Log(direction);

        enemyRB.velocity = direction * move_speed;
        //enemyRB.AddForce(direction * move_speed * Time.deltaTime);

        float distance = Vector2.Distance(enemyRB.position, path.vectorPath[currWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currWaypoint++;
        }

    }
    #endregion

    #region attack_func

    // we want to "attack" or wait to attack as long as we are in contact with a player
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("collided");
        if (collision.transform.CompareTag("Player"))
        {
            if(att_timer <= 0)
            {
                Debug.Log("attacking");
                collision.gameObject.GetComponent<Player>().TakeDamage(dmg);
                att_timer = att_delay;
            }
        }
    }
    #endregion

    #region AI_funcs
    void OnPathComplete(Path P)
    {
        
        if (!P.error)
        {
            path = P;
            currWaypoint = 0;
        }
    }


    #endregion

    #region Health_funcs
    public void TakeDamage(float d)
    {
        CurrHealth -= d;
        if(CurrHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

}
