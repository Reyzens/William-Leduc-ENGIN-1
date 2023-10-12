using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Collider hitboxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        hitboxCollider = GetComponent<Collider>();
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHitCollider() 
    {
       
        hitboxCollider.enabled = true;
        
        Debug.Log(hitboxCollider.enabled);
    }

    public void DisableHitCollider()
    {
        hitboxCollider.enabled = false;
        
    }
    

}
