using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform m_Rotor = null, m_Pipe = null;

    [SerializeField] float m_fSpeedModifier = 50.0f;

    [SerializeField] Vector3 m_velocityY = Vector3.zero;
    [SerializeField] Vector3 m_velocityZ = Vector3.zero;

    float m_fValueY = 0;
    float m_fValueZ = 0;

    readonly float m_fFullCycle = 360.0f;

    public bool m_bIsAbleToMove = true;

    [SerializeField] KeyCode m_keyCanonDown = KeyCode.W;
    [SerializeField] KeyCode m_keyCanonUp = KeyCode.S;
    [SerializeField] KeyCode m_keyCanonLeft = KeyCode.A;
    [SerializeField] KeyCode m_keyCanonRight = KeyCode.D;
    
    void Update()
    {
        //Setze Rotation der Plattform und des Rohrs
        m_Rotor.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(m_velocityY));
        m_Pipe.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(m_velocityZ));
        //Rotationsvektoren
        m_velocityY = new Vector3(0, m_fValueY, 0);
        m_velocityZ = new Vector3(0, m_fValueY, m_fValueZ);

        //Handlen einer vollen Umdrehung. Rotation >= 360, dann Rotation = =
        m_fValueY = m_fValueY >= m_fFullCycle ? 0 : m_fValueY;
        m_fValueZ = m_fValueZ >= m_fFullCycle ? 0 : m_fValueZ;

        if (m_bIsAbleToMove)
        {
            if (Input.GetKey(m_keyCanonDown))
            {
                //Nach unten neigen
                m_fValueZ += Time.deltaTime * m_fSpeedModifier;
            }

            if (Input.GetKey(m_keyCanonLeft))
            {
                //Nach links gieren
                m_fValueY -= Time.deltaTime * m_fSpeedModifier;
            }

            if (Input.GetKey(m_keyCanonUp))
            {
                //Nach oben neigen
                m_fValueZ -= Time.deltaTime * m_fSpeedModifier;
            }

            if (Input.GetKey(m_keyCanonRight))
            {
                //Nach rechts gieren
                m_fValueY += Time.deltaTime * m_fSpeedModifier;
            }
        }
    }

    //Setzt die Kanone zurück
    public void ResetCannon()
    {
        m_bIsAbleToMove = true;
        m_fValueY = 0;
        m_fValueZ = 0;
    }
}