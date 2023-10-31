using UnityEngine;
using System.Collections.Generic;

public class GameManagerSM : BaseStateMachine<IState>
{
    [SerializeField]
    protected Cinemachine.CinemachineVirtualCamera m_gameplayCamera;
    [SerializeField]
    protected Cinemachine.CinemachineVirtualCamera m_cinematicCamera;

    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<IState>();
        m_possibleStates.Add(new GameplayState(m_gameplayCamera));
        m_possibleStates.Add(new CinematicState(m_cinematicCamera));
    }
}