using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterHitbox : MonoBehaviour
{
    [SerializeField]
    private CharacterStatus host;
    [SerializeField]
    private string h_tag;
    public string GetTag(){
        return h_tag;
    }
    public CharacterStatus GetHost(){
        return host;
    }

    void Awake()
    {
        if(host == null){
            host = transform.root.GetComponent<CharacterStatus>();
        }
    }
}
