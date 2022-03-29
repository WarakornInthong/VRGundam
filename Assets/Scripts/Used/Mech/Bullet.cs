using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    private void Awake() {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown(){
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy")){
            Destroy(gameObject,0.5f);
        }
        Destroy(gameObject,2);
    }
}
