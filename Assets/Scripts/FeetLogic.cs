using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetLogic : MonoBehaviour
{
    public CharacterAnimBasedMovement CABM;
    
    private void OnTriggerStay(Collider other)
    {
        CABM.puedosaltar = true;
    }

    private void OnTriggerExit(Collider other)
    {
        CABM.puedosaltar = false;
    }
}
