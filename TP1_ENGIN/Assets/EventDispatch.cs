using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatch : MonoBehaviour
{
    
    public CharacterControllerStateMachine m_CharacterRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHitCollider()
    {

        m_CharacterRef.EnableAttackHitBox();
     
    }
    public void DisableHitCollider()
    {
        m_CharacterRef.DisableAttackHitBox();
    }
}
