using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;
    private float m_currentStateTimer = 0.0f;

    public override void OnEnter()
    {
        Debug.Log("Enter state: JumpState\n");
        m_stateMachine.Animator.SetTrigger("Jump");
        //Effectuer le saut
        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.m_playerCharacterPositionBeforeJump = m_stateMachine.RB.position;
    }

    public override void OnExit()
    {
        Debug.Log("Exit state: JumpState\n");
        m_stateMachine.m_playerCharacterPositionAfterJump = m_stateMachine.RB.position;
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
            return Input.GetKeyDown(KeyCode.Space);
        }
        //This must be run in Update absolutely
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }
}
