using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedObject : MonoBehaviour
{
    public int id;
    public bool is_destroy;
    public void SetDestroyTrue()
    {
        Global.m_gameFlow.obj_destroy[id] = true;
    }
}
