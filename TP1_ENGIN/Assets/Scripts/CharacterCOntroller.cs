using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCOntroller : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private float m_acelerationValue;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var vectorOnFloor = Vector3.ProjectOnPlane(m_camera.transform.forward, Vector3.up);
        vectorOnFloor.Normalize();

        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(vectorOnFloor * m_acelerationValue, ForceMode.Acceleration);
        }
    }
}
