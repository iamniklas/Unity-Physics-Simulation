using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBomb : MonoBehaviour
{
    [SerializeField] float m_fSphereRadius = 10.0f;

    [SerializeField] float m_fExplosionForce = 2.0f;
    [SerializeField] float m_fExplosionRadius = 20.0f;
    [SerializeField] float m_fUpwardsModifier = 0.25f;

    //Explosion auslösen
    public void TriggerExplosion()
    {
        //Array aller umliegenden Pins in Kugel um die eigene Position
        Collider[] colliders = 
            Physics.OverlapSphere(transform.position, m_fSphereRadius);

        //Löse eigene Explision aus
        GetComponent<Rigidbody>().AddExplosionForce(
            m_fExplosionForce, 
            transform.position, 
            m_fExplosionRadius, 
            m_fUpwardsModifier, 
            ForceMode.Impulse);

        //Explosionskraft auf alle Pins in Kugel
        foreach (Collider col in colliders)
        {
            if(col.CompareTag(TagHolder.TAG_PIN))
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(
                    m_fExplosionForce, 
                    transform.position,
                    m_fExplosionRadius, 
                    m_fUpwardsModifier, 
                    ForceMode.Impulse);
            }
        }
    }
}
