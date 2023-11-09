using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using FMODUnity;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 12.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public int playermaxHP = 100;
    public int playerCurrentHP = 1;

    public Material Material1;
    public Material Material2;

    public HealthBar healthBar1;


    LayerMask mask1;
    

    public Transform target;
    public Transform target2;
    private void Start()
    {
        mask1 = LayerMask.GetMask("Wall");
        controller = gameObject.AddComponent<CharacterController>();

        healthBar1.setMaxHealth(playermaxHP);
        playerCurrentHP = playermaxHP;
        

    }

    void Update()
    {
        if(playerCurrentHP <0)
        {
            Reset();
        }
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        
         
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);




        
        

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);



        //Deals with rotation towards mouse position
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 160f, mask1))
        {
            Debug.DrawRay(Camera.main.transform.position, Input.mousePosition * -160, Color.blue);
            transform.LookAt(target2.position);
        }
        else
        {
            Debug.DrawRay(Camera.main.transform.position, Input.mousePosition * -160, Color.red);
        }
        target.position = hit.point;
        target2.position = new Vector3(target.position.x, target.position.y + 1, target.position.z);


    }
    public void DealDamage(int damage)
    {

        Debug.Log("Machucou");
        playerCurrentHP -= damage;
        transform.GetComponent<MeshRenderer>().material = Material1;
        healthBar1.SetHealth(playerCurrentHP);  
        
       
        
        transform.GetComponent<MeshRenderer>().material = Material2;
        



    }
    private void Reset() //Resta o level :D
    {
        
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

}