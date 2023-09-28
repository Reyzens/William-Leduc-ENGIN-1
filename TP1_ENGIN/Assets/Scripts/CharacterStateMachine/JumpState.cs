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
        

        
       
    }

    public override void OnFixedUpdate()
    {
       
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
        if(m_stateMachine.IsInContactWithFloor())
        {
            m_stateMachine.m_playerCharacterPositionAfterJump = m_stateMachine.RB.position;
            m_stateMachine.CharacterJumpDistance();
        }
        Vector3 WVector = new Vector3 (0,0, 0.01f);
        Vector3 SVector = new Vector3(0, 0, -0.01f);
        Vector3 AVector = new Vector3(-0.01f, 0, 0);
        Vector3 DVector = new Vector3(0.01f, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            m_stateMachine.RB.position += WVector;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_stateMachine.RB.position += SVector;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_stateMachine.RB.position += AVector;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_stateMachine.RB.position += DVector;
        }


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
