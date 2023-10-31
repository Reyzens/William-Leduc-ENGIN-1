using UnityEngine;

public class CinematicState : IState
{
    protected Cinemachine.CinemachineVirtualCamera m_camera;

    public CinematicState(Cinemachine.CinemachineVirtualCamera camera)
    {
        m_camera = camera;
    }

    public bool CanEnter(IState currentState)
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    public bool CanExit()
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    public void OnEnter()
    {
        Debug.Log("On Enter CinematicState");
        m_camera.enabled = true;
    }

    public void OnExit()
    {
        Debug.Log("On Exit CinematicState");
        m_camera.enabled = false;
    }

    public void OnFixedUpdate()
    {
    }

    public void OnStart()
    {
    }

    public void OnUpdate()
    {
    }
}
