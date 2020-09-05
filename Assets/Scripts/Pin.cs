using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    Vector3 m_startPosition = Vector3.zero;
    Vector3 m_startRotation = Vector3.zero;

    bool m_bPinIsScored = false;
    
    void Start()
    {
        //Speichern der Startposition und -rotation
        m_startPosition = transform.position;
        m_startRotation = transform.eulerAngles;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case TagHolder.TAG_BALL_COLLECTOR:
                //Erhöhe den Score um 1, 
                //falls der Pin zum ersten Mal in dieser Runde in den Behälter fällt
                if (!m_bPinIsScored)
                {
                    GetComponentInParent<PinManager>().IncPinScore();
                    m_bPinIsScored = true;
                }
                break;
        }
    }

    //Jetzt den Pin zurück auf seine Startposition und -rotation 
    //und setzt die (Winkel-)geschwindigkeit auf 0
    public void ResetPin()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.eulerAngles = m_startRotation;
        transform.position = m_startPosition;
        m_bPinIsScored = false;
    }
}
