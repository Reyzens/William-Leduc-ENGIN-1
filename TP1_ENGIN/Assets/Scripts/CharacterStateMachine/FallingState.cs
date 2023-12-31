using UnityEngine;

public class FallingState : CharacterState
{
  


    public override void OnEnter()
    {
        Debug.Log("Enter state: Falling\n");
        m_stateMachine.Animator.SetTrigger("Falling");
        


    }

    public override void OnExit()
    {
        Debug.Log("Exit state: Falling\n");
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
       
    }

    public override bool CanEnter(CharacterState currentState)
    {
        if (currentState is FreeState)
        {
            if(!m_stateMachine.IsInContactWithFloor())
            {
                return true;
            }
            return false;
        }
       
        return false;
    }

    public override bool CanExit()
    {
        return true;
    }
}

