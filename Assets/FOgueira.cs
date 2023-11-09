using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Experimental.GlobalIllumination;

public class FOgueira : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambience/Firer", transform.position);
    }

    // Update is called once per frame
   
}
