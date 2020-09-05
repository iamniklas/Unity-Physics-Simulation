using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] float m_fSpeed = 0.0f;
    float m_fOffset = 0.0f;

    readonly float maxOffset = 1.0f;
    readonly float minOffset = 0.0f;

    readonly string m_sTextureOffsetName = "_MainTex";
    
    void Update()
    {
        //Offset permanent erhöhen zwischen 0 und 1 und zurücksetzen, falls 
        //Offset >= 1
        m_fOffset = (m_fOffset < maxOffset) ? 
            m_fOffset + Time.deltaTime * m_fSpeed : minOffset;
        
        //Offset anwenden
        GetComponent<MeshRenderer>().material
            .SetTextureOffset(m_sTextureOffsetName, new Vector2(0, m_fOffset));
    }
}