using UnityEngine;

public class OnHitState : CharacterState
{
    private const float STATE_EXIT_TIMER = 1f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("Enter state: Hit\n");
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.Animator.SetTrigger("OnHit");
    }

    public override void OnExit()
    {
        Debug.Log("Exit state: Hit\n");
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
        if(currentState is FreeState)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                return true;
            }
            m_stateMachine.IsHit();
        }
        if(currentState is AttackingState)   
        {
            m_stateMachine.IsHit();
        }
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}
