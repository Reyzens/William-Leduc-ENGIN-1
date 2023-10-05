using System.Collections;
using System.Collections.Generic;
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

        hitboxCollider.isTrigger = true;
        Debug.Log(hitboxCollider.enabled);
    }

   

}
