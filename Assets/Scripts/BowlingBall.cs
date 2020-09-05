using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlingBall : MonoBehaviour
{
    [SerializeField] float m_fShootStrength = 0.0f;
    [SerializeField] ShotSet m_shotSet = null;
    [SerializeField] Cannon m_cannon = null;
    [SerializeField] CameraManager m_cameraManager = null;
    [SerializeField] PinManager m_pinManager = null;

    [SerializeField] Text m_tResetInfo = null;

    Vector3 m_spawn = Vector3.zero;

    Rigidbody m_rigidbody = null;

    bool m_bIsReseting = false;

    [SerializeField] KeyCode m_keyReset = KeyCode.R;
    [SerializeField] float m_fResetDelay = 5.0f;

    [SerializeField] float m_fConveyorStrengthModifier = 200.0f;

    [SerializeField] float m_fBumperStrengthModifier = 150.0f;

    [SerializeField] float m_fMagnetStrengthModifier = 10.0f;

    void Start()
    {
        //Speichern der Anfangsposition
        m_spawn = transform.position;

        m_rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        //Reset, wenn die Reset Taste gedrückt wurde
        //und nicht bereits zurückgesetzt wird
        if(Input.GetKeyDown(m_keyReset) && !m_bIsReseting)
        {
            ResetEverything();
        }
    }

    //Schießen der Kugel mit Multiplicator und Schussvektor
    public void Shoot(float multiplicator, Vector3 direction)
    {
        m_cannon.m_bIsAbleToMove = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>()
            .AddForce(-direction * m_fShootStrength * multiplicator);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            //Behälter: Starte Co-Routine Reset
            case TagHolder.TAG_BALL_COLLECTOR:
                StartCoroutine(Reset());
                break;

            //Ring: Löse Explosion aus
            case TagHolder.TAG_BASKET_TRIGGER:
                other.GetComponent<Basket>().m_Bomb.TriggerExplosion();
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            //Bumper: Stoße mit Impuls mit Vektor von Bumper zur Kugel, wobei y = 0 ist, ab
            case TagHolder.TAG_BUMPER:
                GetComponent<Rigidbody>().AddForce(
                    m_fBumperStrengthModifier * new Vector3(transform.position.x - collision.transform.position.x,
                    0,
                    transform.position.z - collision.transform.position.z),
                    ForceMode.Impulse);
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch(other.tag)
        {
            //Magnet: Anziehen der Kugel zum Magneten mit Modifier
            case TagHolder.TAG_MAGNET:
                GetComponent<Rigidbody>()
                    .AddForce(m_fMagnetStrengthModifier *
                    (other.transform.position - transform.position));
                break;

            //Förderband: Bewege Kugel mit Modifier in Blickrichtung des Bandes
            case TagHolder.TAG_CONVEYOR:
                GetComponent<Rigidbody>()
                    .AddForce(m_fConveyorStrengthModifier *
                    other.transform.forward);
                break;
        }
    }

    //Co-Routine Reset
    IEnumerator Reset()
    {
        m_bIsReseting = true;
        m_tResetInfo.gameObject.SetActive(true);
        yield return new WaitForSeconds(m_fResetDelay);
        ResetEverything();
        yield return null;
    }

    //Methode, um alles zurückzusetzen
    void ResetEverything()
    {
        transform.position = m_spawn;
        m_rigidbody.isKinematic = true;
        m_shotSet.m_bDoMultiplicatorMod = true;
        m_shotSet.m_bCanShoot = true;
        m_cannon.ResetCannon();
        m_cameraManager.ResetCamera();
        m_pinManager.ResetPins();
        m_bIsReseting = false;
        m_tResetInfo.gameObject.SetActive(false);
    }
}
