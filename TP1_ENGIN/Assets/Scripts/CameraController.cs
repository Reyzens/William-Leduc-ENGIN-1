using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_objectToLookAt;
    [SerializeField]
    private float m_rotationSpeed = 1.0f;
    [SerializeField]
    private Vector2 m_clampingXRotationValues = Vector2.zero;
    [SerializeField]
    private Vector2 m_cameraClamping = Vector2.zero;
    [SerializeField]
    private float m_desireDistance = -3;

    bool InContact;

    // Update is called once per frame
    void Update()
    {
        UpdateHorizontalMovements();
        UpdateVerticalMovements();
       if (InContact == false)
        {
            UpdateCameraScroll();
        }
        
        
        
        
        
    }

    private void FixedUpdate()
    {
        
        MoveCameraInFrontOfObstructionsFUpdate();
        
           
           
        
        
    }

    private void UpdateHorizontalMovements()
    {
        float currentAngleX = Input.GetAxis("Mouse X") * m_rotationSpeed;
        transform.RotateAround(m_objectToLookAt.position, m_objectToLookAt.up, currentAngleX);
        
    }

    private void UpdateVerticalMovements()
    {
        float currentAngleY = Input.GetAxis("Mouse Y") * m_rotationSpeed;
        float eulersAngleX = transform.rotation.eulerAngles.x;

        float comparisonAngle = eulersAngleX + currentAngleY;

        comparisonAngle = ClampAngle(comparisonAngle);

        if ((currentAngleY < 0 && comparisonAngle < m_clampingXRotationValues.x)
            || (currentAngleY > 0 && comparisonAngle > m_clampingXRotationValues.y))
        {
            return;
        }
        transform.RotateAround(m_objectToLookAt.position, transform.right, currentAngleY);
    }
    
    private void UpdateCameraScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            var distanceCameraToCharacter = transform.position - m_objectToLookAt.position;
            var CurrentDistance = distanceCameraToCharacter.magnitude;

            m_desireDistance += Input.mouseScrollDelta.y * 1;

            // Clamp m_desireDistance to the desired range
            m_desireDistance = Mathf.Clamp(m_desireDistance, -10f, -2f);

            Debug.Log(m_desireDistance);
            transform.Translate(Vector3.forward * Input.mouseScrollDelta.y, Space.Self);

            //TODO: Faire une vérification selon la distance la plus proche ou la plus éloignée
            //Que je souhaite entre ma caméra et mon objet
            if (CurrentDistance <= m_cameraClamping.x)
            {
                transform.Translate(-Vector3.forward * m_cameraClamping.x, Space.Self);
            }
            if (CurrentDistance >= m_cameraClamping.y)
            {
                transform.Translate(Vector3.forward * m_cameraClamping.x, Space.Self);
            }



            //TODO: Lerp plutôt que d'effectuer immédiatement la translation de la caméra

        }
    }
    
    private void MoveCameraInFrontOfObstructionsFUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        RaycastHit hit;
        

        var vecteurDiff = transform.position - m_objectToLookAt.position;
        var distance = vecteurDiff.magnitude;
        vecteurDiff.Normalize();

        //Debug.DrawRay(m_objectToLookAt.position, vecteurDiff.normalized * -m_desireDistance, Color.yellow);

        if (Physics.Raycast(m_objectToLookAt.position, vecteurDiff  , out hit, distance, layerMask))
        {
            // There is an obstruction between the focus and the camera
            
            Debug.DrawRay(m_objectToLookAt.position, vecteurDiff.normalized * hit.distance, Color.yellow);
            transform.SetPositionAndRotation(hit.point, transform.rotation);
        }
        else
        {
            
           if (Vector3.Distance(transform.position, m_objectToLookAt.position) <= m_desireDistance)
           {
               transform.Translate((-transform.forward) * 2, Space.Self);
           }
           // No obstruction and m_isObstruct was true in the previous frame
           Debug.DrawRay(m_objectToLookAt.position, vecteurDiff, Color.white);
           
           
           // Reposition the camera to the desired distance from the object

        }
    }

    private float ClampAngle(float angle)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        return angle;
    }
}
