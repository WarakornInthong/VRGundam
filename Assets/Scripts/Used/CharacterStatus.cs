using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    private float c_MoveSpeed;
    private float c_HP;
    private float c_Fuel;
    private float c_Booster;
    private float c_Armor;
    private float c_Shiled;
    private bool c_IsDowned = false;
    private float c_DebufPoint = 0.0f;
    private bool c_GetDebuf = false;
    private float bu_Armor;
    private float bu_MoveSpeed;
    private float bu_Shiled;

    public HitArea[] hitAreas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!c_IsDowned){
            if(c_HP <= 0){
                c_IsDowned = true;
                c_MoveSpeed = 0;
            }
            if(c_Fuel <= 0){
                SystemDowned();
            }
            if(c_GetDebuf){
                c_DebufPoint = 0;
            }
        }
    }

    public void RestoreSystem(){
        c_GetDebuf = false;
        c_Armor = bu_Armor;
        c_MoveSpeed = bu_MoveSpeed;
        c_Shiled = bu_Shiled;
    }


    public void StartEngine(float speed, float hp, float fuel, float armor, float shiled ){
        c_MoveSpeed = speed;
        c_HP = hp;
        c_Fuel = fuel;
        c_Armor = armor;
        c_Shiled = shiled;
        c_DebufPoint = 0.0f;
        c_GetDebuf = false;
        c_IsDowned = false;
    }

    public void SetDownedStatus(float debufPoint){
        if(debufPoint >= 0)
            c_DebufPoint += debufPoint;
        if(c_DebufPoint >= 100){
            c_DebufPoint = 100;
        }
    }

    private void SystemDowned(){
        if(c_DebufPoint >= 100 & !c_GetDebuf){
            bu_Armor = c_Armor;
            bu_MoveSpeed = c_MoveSpeed;
            bu_Shiled = c_Shiled;

            c_Armor = 0;
            c_MoveSpeed = 0;
            c_Shiled = 0;
            c_GetDebuf = true;
        }
    }

    public void UseBooster(float booster){
        c_Booster -= booster * Time.deltaTime;
    }

    public void GetDamaged(float damage){
        if(damage > 0)
            c_HP -= damage;
        if(c_HP <= 0){
            c_HP = 0;
        }
    }

    public float GetHP(){
        return c_HP;
    }

    public float GetMoveSpeed(){
        return c_MoveSpeed;
    }

    public void UseFuel(float fuel){
        c_Fuel -= fuel * Time.deltaTime;
    }

    public void GetDamagedWithTag(float damage, string tag){
        if(hitAreas.Length != 0){
            for(int i = 0 ; i < hitAreas.Length ; i++){
                if(hitAreas[i].tag == tag){
                    hitAreas[i].value -= damage;
                    Debug.Log("Get Attack at " + hitAreas[i].tag);
                    Debug.Log("now duration is " + hitAreas[i].value);
                    break;
                }
            }
        }
    }

    [System.Serializable]
    public class HitArea{
        public string tag;
        public CharacterHitbox[] hitboxs;
        public float value;
    }
}
