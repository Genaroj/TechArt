using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate()
    {
        RotatePLs();
    }
    public void RotatePLs()
    {
        transform.Rotate(1,1,1);
    }
}
