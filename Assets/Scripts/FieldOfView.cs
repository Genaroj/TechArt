using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;
using UnityEngine.ParticleSystemJobs;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public ParticleSystem enemyparticle;

    public GameObject spw;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    PlayerMovement movref;

    bool tocou = false;


    public bool canSeePlayer;
    // Start is called before the first frame update
    void Start()
    {
        enemyparticle = GetComponent<ParticleSystem>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        movref = (PlayerMovement)playerRef.GetComponent<PlayerMovement>();
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if(transform.position.x == spw.transform.position.x && transform.position.z == spw.transform.position.z) 
        {
            enemyparticle.Stop();
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Bullet" && transform.tag == "Enemy" && canSeePlayer==false) //Rotaciona o inimigo para olhar para a direção do jogador quando levar um tiro e ainda não ter o player como target
        {
            transform.LookAt(playerRef.transform);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && transform.tag =="Enemy") 
        {
            movref.DealDamage(15);
        }
    }

    private IEnumerator FOVRoutine() 
    {
      
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true) 
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0 ) //entrou algum objeto no raio de visão
        {
            if (transform.tag == "Enemy")//se o inimigo não estiver convertido, ele deve procurar objetos de tag "Player"
            {
                for (int i = 0; i < rangeChecks.Length; i++)
                {
                    if (rangeChecks[i].gameObject.transform.tag == "Player")
                    {
                        Transform target = rangeChecks[i].transform; //Pega a posição do primeiro obj que entrar em contato com o RANGECHECKS e que esteja na layer mask targetMask
                        Vector3 directionToTarget = (target.position - transform.position).normalized;

                        if(Vector3.Angle(transform.forward, directionToTarget)< angle / 2)
                        {
                            float distanceToTarget = Vector3.Distance(transform.position, target.position);

                            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                            {
                                canSeePlayer = true;
                                if (canSeePlayer)
                                {
                                    enemyparticle.Play();

                                    GetComponent<NavMeshAgent>().destination = target.position;
                                    if(tocou == false) 
                                    {
                                        FMODUnity.RuntimeManager.PlayOneShot("event:/Enemy", GetComponent<Transform>().position);
                                        tocou = true;
                                    }

                                }
                            }
                            else canSeePlayer = false;

                        }
                        else
                        {
                            canSeePlayer = false;
                            Invoke("GobackStage", 0.1f);

                        }
                    }
                }

            }
            else if(transform.tag == "Ally") 
            {
                for (int i = 0; i < rangeChecks.Length; i++)
                {
                    if (rangeChecks[i].gameObject.transform.tag == "Enemy")
                    {
                        Transform target = rangeChecks[i].transform; //Pega a posição do primeiro obj que entrar em contato com o RANGECHECKS e que esteja na layer mask targetMask
                        Vector3 directionToTarget = (target.position - transform.position).normalized;

                        if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                        {
                            float distanceToTarget = Vector3.Distance(transform.position, target.position);

                            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                            {
                                canSeePlayer = true;
                                if (canSeePlayer)
                                {
                                    GetComponent<NavMeshAgent>().destination = target.position;


                                }
                            }
                            else canSeePlayer = false;

                        }
                        else
                        {
                            canSeePlayer = false;

                        }
                    }
                }
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;

        }
    }

    private void GobackStage()
    {
        Debug.Log("Acioou");
        GetComponent<NavMeshAgent>().destination = spw.transform.position;
        enemyparticle.Play();

        tocou = false;

    }
}
