using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class MeleeEnemy : EnemyBot
{
    //public Transform target;
    private NavMeshAgent agent;
    public Transform[] spoted;
    public Vector3 prev;
    public Vector3 spotedPosition;
    private float countingTime;
    private bool isTrigger;
    private bool isAttack;
    private bool isOutSpoted;
    public int num;
    public Transform RHand;
    public float hitBox;
    private bool isTakingDamage = false;
    // Start is called before the first frame update
    void Awake()
    {
        isTrigger=false;
        isOutSpoted=false;
        agent = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponent<FieldOfView>();
        animator = GetComponent<Animator>();

        maxHP = 8;
        hp = maxHP;
        animator.SetInteger("speed", 1);
    }
    void Update()
    {
        
        if(!isDead){
            //dead
            if(hp<=0){
                isDead = true;
                Debug.Log("dead");
                animator.SetBool("die",true);
                Destroy(gameObject, 4f);
            }
            //alive
            else{
                //see player
                if(fieldOfView.isSeePlayer==true){
                    
                    //out of attacking range
                    // Debug.Log(Vector3.ProjectOnPlane(fieldOfView.castPoint, Vector3.up));
                    if(Vector3.Distance(Vector3.ProjectOnPlane(fieldOfView.castPoint, Vector3.up),Vector3.ProjectOnPlane(transform.position, Vector3.up) ) > agent.stoppingDistance){
                        agent.SetDestination(fieldOfView.target.position);
                        isOutSpoted=true;
                        countingTime = 0;
                        //Debug.Log("out of range");
                        animator.SetInteger("speed", 1);
                    }
                    //in attacking range
                    else{
                        CalDamage();
                        if(!isAttack){
                            // Debug.Log("Attack");
                            agent.SetDestination(transform.position);
                            isAttack=true;
                            StartCoroutine(AttackRoutine());
                            
                            //animator.SetInteger("speed",0);
                        }
                    }
            
                }
                // can not see player
                else{
                    if(isOutSpoted){
                        countingTime += Time.deltaTime;
                        if(countingTime > 1.5f){
                            isOutSpoted = false;
                            animator.SetInteger("speed", 1);
                            //isAttack = false;
                        }
                        
                    }
                    else{
                        // Already assign walk spoted
                        if(spoted.Length!=0){
                            float dstToTarget = Vector3.Distance(transform.position, spoted[num].position);
                            if(dstToTarget > agent.stoppingDistance){
                                agent.SetDestination(spoted[num].position);
                            }
                            else{
                                
                                if(!isTrigger){
                                    animator.SetInteger("speed", 0);
                                    Debug.Log(prev);
                                    StartCoroutine(NavRoutine());
                                }
                            }
                        }
                        // Stop and Find
                        else{
                            agent.SetDestination(transform.position);
                            animator.SetInteger("speed", 0);
                        }
                    }
                    
                //animator.SetInteger("speed",1);
                
                }
            }
               
        }

    }


    IEnumerator NavRoutine(){
        isTrigger=true;
        yield return new WaitForSeconds(2);
        num++;
        num%=spoted.Length;
        isTrigger=false;

        animator.SetInteger("speed", 1);
    }

    IEnumerator AttackRoutine(){
        animator.SetBool("attack", isAttack);
        yield return new WaitForSeconds(2f);
        isAttack=false;
        isTakingDamage = true;
        animator.SetBool("attack", isAttack);
        
    }

    private void CalDamage(){
        if(isTakingDamage){
            Collider[] attackedTargets = Physics.OverlapSphere(RHand.position, hitBox, fieldOfView.targetMask);
            if(attackedTargets.Length != 0){
                if(attackedTargets[0].GetComponent<JaganController>() != null){
                    attackedTargets[0].GetComponent<JaganController>().GetTakingDamage(2);
                    
                    isTakingDamage = false;
                }
                if(attackedTargets[0].GetComponent<CharacterHitbox>() != null){
                    CharacterStatus status = attackedTargets[0].transform.root.GetComponent<CharacterStatus>();
                    status.GetDamagedWithTag(2, attackedTargets[0].GetComponent<CharacterHitbox>().GetTag());
                    
                    isTakingDamage = false;
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Weapon")){
            hp-=3;
        }
    }
}
