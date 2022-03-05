using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int numberOfBullet = 8;

    public void CheckBullet(){
        if(numberOfBullet <= 0){
            Destroy(gameObject, 4);
        }
    }
}
