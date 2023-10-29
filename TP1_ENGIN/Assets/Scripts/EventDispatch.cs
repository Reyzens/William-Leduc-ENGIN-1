using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDispatch : MonoBehaviour
{
    public CharacterControllerStateMachine m_CharacterRef;
    // Start is called before the first frame update
    public void EnableHitCollider()
    {

        m_CharacterRef.EnableAttackHitBox();

    }
    public void DisableHitCollider()
    {
        m_CharacterRef.DisableAttackHitBox();
    }
}
