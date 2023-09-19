using UnityEngine;

public class AttackingState : CharacterState
{
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("Enter state: Attacking\n");

        
    }

    public override void OnExit()
    {
        Debug.Log("Exit state: Attacking\n");
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override bool CanEnter(CharacterState currentState)
    {
        if (currentState is JumpState)
        {
            //Je ne peux entrer dans le FreeState que si je touche le sol
            return m_stateMachine.IsInContactWithFloor();
        }
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}
