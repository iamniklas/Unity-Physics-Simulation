using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinManager : MonoBehaviour
{
    [SerializeField] Text m_tScoredPins = null;
    int m_iPinscore = 0;
    readonly int m_iPinStartScore = 0;
    
    string m_sTextAddition = "Pins im Auffangbehälter: ";

    void Start()
    {
        m_tScoredPins.text = m_sTextAddition + m_iPinscore;
    }

    //Erhöht den Pin Score um 1 und aktualisiert den Text
    public void IncPinScore()
    {
        m_iPinscore++;
        m_tScoredPins.text = m_sTextAddition + m_iPinscore;
    }

    //Setzt den Pin Score zurück, aktualisiert den Text und setzt die Pins auf ihre Startposition zurück
    public void ResetPins()
    {
        m_iPinscore = m_iPinStartScore;
        m_tScoredPins.text = m_sTextAddition + m_iPinscore;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Pin>().ResetPin();
        }
    }
}