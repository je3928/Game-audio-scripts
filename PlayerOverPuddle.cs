using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverPuddle : MonoBehaviour
{

    private bool PlayerOnPuddle = false;


    private void OnTriggerEnter(Collider other)
    {   
        // Check if collider is a puddle by checking its tag, if so apply to bool. 
        if(other.gameObject.tag == "Puddle")
        {   
            PlayerOnPuddle = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {   
        // Check if collider is a puddle by checking its tag, if so update bool.
        if(other.gameObject.tag == "Puddle")
        {
            PlayerOnPuddle = false;
        }

    }

    // Return if player is over puddle
    public bool IsOverPuddle()
    {
        return PlayerOnPuddle;
    }


}
