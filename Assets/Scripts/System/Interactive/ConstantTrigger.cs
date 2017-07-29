using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ConstantTrigger : MonoBehaviour
{
    public SpecialLogicInterface m_Logic;
    public string col_tag = "Player";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == col_tag)
        {
            m_Logic.m_InteractEvent.Invoke();
        }
    }
}

[Serializable]
public class SpecialLogicInterface
{
    //public int ID;
    [Serializable]
    public class InteractEvent : UnityEvent
    {

    }
    [SerializeField]
    public InteractEvent m_InteractEvent = new InteractEvent();
}
