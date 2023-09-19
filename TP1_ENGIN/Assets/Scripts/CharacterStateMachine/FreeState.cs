using UnityEngine;
using UnityEngine.EventSystems;

public class FreeState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("Enter state: FreeState\n");
    }

    public override void OnUpdate()
    {
        // Reset the move direction vector.
        var vectorOnFloorFront = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, Vector3.up);
        var vectorOnFloorBack = Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.forward, Vector3.down);
        var vectorOnFloorLeft = Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.right, Vector3.down);
        var vectorOnFloorRigth = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, Vector3.up);
        

        vectorOnFloorFront.Normalize();
        vectorOnFloorBack.Normalize();
        vectorOnFloorLeft.Normalize();
        vectorOnFloorRigth.Normalize();
        
        if(m_stateMachine.RB.velocity.magnitude > 0)
        m_stateMachine.m_movementPositionVector = Vector3.zero;

        // Check for input and add movement in the desired directions.
        if (Input.GetKey(KeyCode.W))
        {
            m_stateMachine.m_movementPositionVector += vectorOnFloorFront;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_stateMachine.m_movementPositionVector += vectorOnFloorBack;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_stateMachine.m_movementPositionVector += vectorOnFloorLeft;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_stateMachine.m_movementPositionVector += vectorOnFloorRigth;
        }
       
       
        // Normalize the movement vector if necessary.
        if (m_stateMachine.m_movementPositionVector != Vector3.zero)
        {
            m_stateMachine.m_movementPositionVector.Normalize();
        }
    }

    public override void OnFixedUpdate()
    {    
        m_stateMachine.RB.AddForce(m_stateMachine.m_movementPositionVector * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
        m_stateMachine.RB.velocity -= m_stateMachine.SlowingValue * m_stateMachine.RB.velocity;
        if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxFowardVelocity)
        {
            m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
            m_stateMachine.RB.velocity *= m_stateMachine.MaxFowardVelocity;
        }

        float ALLCOMPONENT = Vector3.Dot(m_stateMachine.RB.velocity, m_stateMachine.m_movementPositionVector);
        m_stateMachine.UpdateAnimationValues();
        
        //TODO 31 AOÛT:
        //Appliquer les déplacements relatifs à la caméra dans les 3 autres directions
        //Avoir des vitesses de déplacements maximales différentes vers les côtés et vers l'arrière
        //Lorsqu'aucun input est mis, décélérer le personnage rapidement

        //Debug.Log(m_stateMachine.RB.velocity.magnitude);
    }

    public override void OnExit()
    {
        Debug.Log("Exit state: FreeState\n");
    }

    public override bool CanEnter()
    {
        //Je ne peux entrer dans le FreeState que si je touche le sol
        return m_stateMachine.IsInContactWithFloor();
    }

    public override bool CanExit()
    {
        return true;
    }
}
