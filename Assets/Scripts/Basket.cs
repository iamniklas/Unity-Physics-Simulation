using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] PinBomb m_bomb = null;

    public PinBomb m_Bomb
    {
        get { return m_bomb; }
    }
}