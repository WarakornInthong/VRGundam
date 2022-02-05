using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Awake() {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown(){
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject,2);
    }
}
