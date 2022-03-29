using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBot : MonoBehaviour
{
    protected float hp;
    protected float maxHP;
    protected bool isDead;
    protected bool hasSeenPlayer;
    protected FieldOfView fieldOfView;
    protected Animator animator;
    protected AudioSource sound;
    protected Slider hpBar;
    protected ParticleSystem explosionParticle;
    void OnCollisionEnter(Collision other)
    {
        //if(other.gameObject.CompareTag("Weapon")){
        //    hp -= other.gameObject.GetComponent<Weapon>().damage;
        //}
    
    }
}