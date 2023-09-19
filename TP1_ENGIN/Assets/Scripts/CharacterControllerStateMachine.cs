using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateMachine : MonoBehaviour
{
    public Camera Camera { get; private set; }
    [field: SerializeField]
    public Rigidbody RB { get; private set; }
    [field: SerializeField]
    public Animator Animator { get; private set; }  
    [field: SerializeField]
    public float AccelerationValue { get; private set; }
    [field: SerializeField]
    public float SlowingValue { get; private set; }
    [field: SerializeField]
    public float MaxFowardVelocity { get; private set; }
    [field: SerializeField]
    public float MaxSidedVelocity { get; private set; }
    [field: SerializeField]
    public float MaxBackVelocity { get; private set; }
    public float JumpIntensity { get; private set; } = 300.0f;

    public Vector3 m_movementPositionVector = Vector3.zero;

    [SerializeField]
    private CharacterFloorTrigger m_floorTrigger;
    private CharacterState m_currentState;
    private List<CharacterState> m_possibleStates;
    

    private void Awake()
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new FreeState());
        m_possibleStates.Add(new JumpState());
        m_possibleStates.Add(new AttackingState());
        m_possibleStates.Add(new FallingState());
        m_possibleStates.Add(new OnGettingUpState());
        m_possibleStates.Add(new OnGroundState());
        m_possibleStates.Add(new OnHitState());
        m_possibleStates.Add(new StunnedState());
    }

    // Start is called before the first frame update
    void Start()
    {
        Camera = Camera.main;
        

        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();
    }

    private void Update()
    {
        m_currentState.OnUpdate();
        TryStateTransition();
        Animator.SetBool("TouchGround", m_floorTrigger.IsOnFloor);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }

    private void TryStateTransition()
    {
        if (!m_currentState.CanExit())
        {
            return;
        }

        //Je PEUX quitter le state actuel
        foreach (var state in m_possibleStates)
        {
            if (m_currentState == state)
            {
                continue;
            }

            if (state.CanEnter(m_currentState))
            {
                //Quitter le state actuel
                m_currentState.OnExit();
                m_currentState = state;
                //Rentrer dans le state state
                m_currentState.OnEnter();
                return;
            }
        }
    }
   
    public bool IsInContactWithFloor()
    {
        return m_floorTrigger.IsOnFloor;
    }
    
    public void UpdateMovementAnimationValues()
    {
        Animator.SetFloat("MoveX", RB.velocity.x / MaxSidedVelocity * 10);
        Animator.SetFloat("MoveY", RB.velocity.z / MaxFowardVelocity * 10);

    }

 

    public void UpdateAttackAnimationValues()
    {

    }

    public void UpdateStunnedAnimationValues()
    {

    }

    public void UpdateOnHitAnimationValues()
    {

    }

    public void UpdateOnGroundAnimationValues()
    {

    }

    public void UpdateOnGettingUpAnimationValues()
    {

    }

    public void UpdateFallingAnimationValues()
    {

    }
}
