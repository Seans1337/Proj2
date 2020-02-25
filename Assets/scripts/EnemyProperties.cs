using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviour : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("How much health the enemy has")]
    private int m_MaxHealth;

    [SerializeField]
    [Tooltip("How fast the enemy can move")]
    private float m_Speed;

    [SerializeField]
    [Tooltip("Damage dealt per frame")]
    private float m_Damage;
    #endregion

    #region Private Variables
    private float p_curHealth;
    #endregion

    #region Cached Components
    private Rigidbody cc_Rb;
    #endregion

    #region Cached References
    private Transform cr_Player;
    #endregion

    #region Initialization
    private void Awake()
    {
        p_curHealth = m_MaxHealth;

        cc_Rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        cr_Player = FindObjectOfType<Bandit>().transform;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {

    }
}
