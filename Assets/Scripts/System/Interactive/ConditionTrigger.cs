using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionTrigger : MonoBehaviour
{
    public SpecialLogicInterface m_Logic_True;
    public SpecialLogicInterface m_Logic_False;

    public bool is_global = false;
    public string switch_name;
    public string col_tag="Player";
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
            if (GetResOfCondition())
            {
                m_Logic_True.m_InteractEvent.Invoke();
            }
            else
            {
                m_Logic_False.m_InteractEvent.Invoke();
            }
        }
    }
    bool GetResOfCondition()
    {
        if (is_global)
        {
            return Global.m_pubSwitches.GetSwitch(switch_name);
        }
        else
        {
            return Global.m_gameFlow.pri_switch.GetSwitch(switch_name);
        }
    }
}
