using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPos : MonoBehaviour
{
    public bool inValidPos;

    void Start()
    {
        inValidPos = false;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        // if collision is valid, set bool to true else false
        //Debug.Log("enter");

        if (collision.tag == "DropValid")
        {
            inValidPos = true;
            //Debug.Log(inValidPos);
        }
        else
        {
            inValidPos = false;
            //Debug.Log(inValidPos);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        // if collision is animal, set true else set bool to false
        //Debug.Log("exit");

        if (collision.tag == "Animal")
        {
            inValidPos = true;
            //Debug.Log(inValidPos);
        }
        else
        {
            inValidPos = false;
            //Debug.Log(inValidPos);
        }
    }
}
