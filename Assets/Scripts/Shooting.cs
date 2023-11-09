using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Shooting : MonoBehaviour
{

    public GameObject bala;//de referencia
    public Transform spawnpoint;



    public int carregador = 3;
    public int Timer = 5;
    public int max = 5;

    public float speed = 5f;

   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Botão esquerdo padrão
        if (Input.GetMouseButtonDown(0) && carregador > 0)
        {
            //copia da bala, não a original!
            var balaInstanciada = Instantiate(bala, spawnpoint.position, spawnpoint.rotation);
            balaInstanciada.GetComponent<Rigidbody>().velocity = spawnpoint.forward * speed;
            Destroy(balaInstanciada, 3f);
            carregador--;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Player/Shot", transform.position);

        }
        if (carregador > max) { carregador = max; }
        else if (carregador < 0) { carregador = 0; }

    }
    private void FixedUpdate()
    {
        Timer--;
        if (Timer == 0)
        {
            carregador++;
            Timer = 300;
        }
    }

}