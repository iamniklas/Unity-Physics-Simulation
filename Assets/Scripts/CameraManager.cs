using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform m_ball = null;
    [SerializeField] Vector3 m_alleyStartPosition = Vector3.zero;

    [SerializeField] Vector3 m_offset = Vector3.zero;

    Vector3 m_targetPosition = Vector3.zero;

    public bool m_bFollowBall = false;

    Vector3 m_startPosition = Vector3.zero;

    private void Start()
    {
        //Speichern der Startposition
        m_startPosition = transform.position;
    }

    void Update()
    {
        if(m_bFollowBall)
        {
            //Der Kugel folgen mit Offset Vektor
            m_targetPosition = m_ball.position + m_offset;
            Vector3 cameraPosition;

            cameraPosition = new Vector3(
                    m_ball.position.x + m_offset.x,
                    m_offset.y,
                    m_ball.position.z + m_offset.z);
            transform.position = cameraPosition;     
        }

        //Auf die Kugel schauen
        transform.LookAt(m_ball);
    }

    //Kamera auf Startposition zurücksetzen
    public void ResetCamera()
    {
        m_bFollowBall = false;
        transform.position = m_startPosition;
    }
}
