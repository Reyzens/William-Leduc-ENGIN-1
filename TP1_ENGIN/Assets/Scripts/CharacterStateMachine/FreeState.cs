using UnityEngine;

public class FreeState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("Enter state: FreeState\n");
    }

    public override void OnUpdate()
    {
    }

    public override void OnFixedUpdate()
    {
        var vectorOnFloorFront = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, Vector3.up);
        var vectorOnFloorBack = Vector3.ProjectOnPlane(- m_stateMachine.Camera.transform.forward, Vector3.down);
        var vectorOnFloorLeft = Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.right, Vector3.down);
        var vectorOnFloorRigth = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, Vector3.up);
        var slowingVector = Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.forward, Vector3.down);
        
        vectorOnFloorFront.Normalize();
        vectorOnFloorBack.Normalize();
        vectorOnFloorLeft.Normalize();
        vectorOnFloorRigth.Normalize();

        if (Input.GetKey(KeyCode.W))
        {
            m_stateMachine.RB.AddForce(vectorOnFloorFront * m_stateMachine.FowardAccelerationValue, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_stateMachine.RB.AddForce(vectorOnFloorBack * m_stateMachine.FowardAccelerationValue, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_stateMachine.RB.AddForce(vectorOnFloorLeft * m_stateMachine.SideAccelerationValue, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_stateMachine.RB.AddForce(vectorOnFloorRigth * m_stateMachine.SideAccelerationValue, ForceMode.Acceleration);
        }
        if (m_stateMachine.RB.velocity.magnitude > m_stateMachine.MaxVelocity)
        {
            m_stateMachine.RB.velocity = m_stateMachine.RB.velocity.normalized;
            m_stateMachine.RB.velocity *= m_stateMachine.MaxVelocity;
        }
        float fowardComponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorOnFloorFront);
        float SideComponent = Vector3.Dot(m_stateMachine.RB.velocity, vectorOnFloorRigth);
        m_stateMachine.UpdateAnimationValues(new Vector2(SideComponent, fowardComponent));
        
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
