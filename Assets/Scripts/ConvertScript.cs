using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConvertScript : MonoBehaviour
{

    public float lifePoints = 3f;
    public Material Material1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lifePoints <= 0) 
        {
            transform.gameObject.tag = "Ally";
            transform.GetComponent<MeshRenderer>().material = Material1;

        }
    }
    private void OnTriggerEnter(Collider other)      
    {
        if(other.transform.tag == "Bullet" ) 
        {
            Destroy(other.gameObject);
            
            lifePoints--;
        }
    }
}
