using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotSet : MonoBehaviour
{
    [SerializeField] Image m_image = null;
    [SerializeField] GameObject m_pipe = null;
    [SerializeField] BowlingBall m_ball = null;

    [SerializeField] CameraManager m_cameraManager = null;

    float m_fMultiplicator = 0.0f;
    float m_fMultiplicatorMod = 2.0f;
    readonly float m_fMaxMultiplicatorValue = 1.0f;
    bool m_bGoingUp = true;
    public bool m_bDoMultiplicatorMod = true;

    [SerializeField] float m_fMinShootPower = 0.05f;

    Vector3 m_shootVector;

    public bool m_bCanShoot = true;

    [SerializeField] KeyCode m_shootKey = KeyCode.Space;
    
    void Start()
    {
        m_cameraManager = Camera.main.GetComponent<CameraManager>();
    }
    
    void Update()
    {   
        if(m_bDoMultiplicatorMod)
        {
            //Mutliplikator erhöhen
            if(m_bGoingUp)
            {
                if(m_fMultiplicator < m_fMaxMultiplicatorValue)
                {
                    m_fMultiplicator += m_fMultiplicatorMod * Time.deltaTime;
                }
                else
                {
                    m_fMultiplicator = m_fMaxMultiplicatorValue;
                    m_bGoingUp = false;
                }
            }
            else
            {   
                //Mutliplikator verringern
                if (m_fMultiplicator > m_fMinShootPower)
                {
                    m_fMultiplicator -= m_fMultiplicatorMod * Time.deltaTime;
                }
                else
                {
                    m_fMultiplicator = m_fMinShootPower;
                    m_bGoingUp = true;
                }
            }
        }

        if (Input.GetKeyDown(m_shootKey) && m_bCanShoot)
        {
            //Kugel schießen, wenn Taste gedrückt und geschossen werden kann
            m_bCanShoot = false;
            m_bDoMultiplicatorMod = false;
            m_shootVector = m_pipe.transform.right;
            m_cameraManager.m_bFollowBall = true;
            m_ball.Shoot(m_fMultiplicator, m_shootVector);
        }

        m_image.fillAmount = m_fMultiplicator;
    }
}