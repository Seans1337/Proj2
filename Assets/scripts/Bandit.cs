using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    #region character variables
    [SerializeField] float      m_speed = 1.0f;
    [SerializeField] float      m_jumpForce = 2.0f;
    [SerializeField] GameObject fireballFab;
    [SerializeField] float fireball_speed = 1.0f;
    [SerializeField] bool m_player = false;
    
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;
    #endregion

    #region general character functions
    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }
	
	// Update is called once per frame
	void Update () {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if(m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0) {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (inputX < 0) {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        ////Death
        //if (Input.GetKeyDown("e")) {
        //    if(!m_isDead)
        //        m_animator.SetTrigger("Death");
        //    else
        //        m_animator.SetTrigger("Recover");

        //    m_isDead = !m_isDead;
        //}
            
        ////Hurt
        //else if (Input.GetKeyDown("q"))
        //    m_animator.SetTrigger("Hurt");

        //Attack
        if(Input.GetMouseButtonDown(0)) {
            m_animator.SetTrigger("Attack");
            fireball();
        }

        //Change between idle and combat idle
        if (Input.GetKeyDown("f")) {
            m_combatIdle = !m_combatIdle;
        }

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded) {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon) {
            m_animator.SetInteger("AnimState", 2);
        }

        //Combat Idle
        else if (m_combatIdle) {
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else {
            m_animator.SetInteger("AnimState", 0);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("Enemy") || coll.transform.CompareTag("projectile"))
        {
            if (m_player)
            {
                Die();
            } else
            {
                m_animator.SetTrigger("Death");
                Destroy(this);
            }
        }

        else if (coll.transform.CompareTag("winGate"))
        {
            if (m_player)
            {
                Win();
            }
        }

        else if (coll.transform.CompareTag("nextLevel"))
        {
            if (m_player)
            {
                nextLevel();
            }
        }

    }

    private void Die()
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        gm.GetComponent<GameManager>().LoseGame();
    }

    private void Win()
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        gm.GetComponent<GameManager>().WinGame();

    }

    private void nextLevel()
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        gm.GetComponent<GameManager>().NextLevel();
    }
    #endregion

    #region attack functions
    private void fireball()
    {
        GameObject ball = Instantiate(fireballFab) as GameObject;
        Physics2D.IgnoreCollision(ball.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        ball.transform.position = transform.position;
        ball.transform.position += new Vector3(0, (float)0.2, 0);
        //ball.transform.position -= new Vector3(transform.localScale.x/3, (float) -0.2, 0);
        ball.transform.localScale = Vector3.Scale(ball.transform.localScale, -transform.localScale);
        Destroy(ball, 3);
    }

    #endregion
}
