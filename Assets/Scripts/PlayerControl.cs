using System.Runtime.Serialization.Formatters;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class PlayerControl : MonoBehaviour
{
    private PlayerCharacter m_Character;
    private Transform m_Cam;
    private Vector3 m_CamForward;
    private Vector3 m_Move;

    // private PlayerDetection m_playerDetection; //provisoire

    private Vector3 m_TmpCamForward;
    private Vector3 m_TmpCamRight;

    private bool m_keepControl;
    private Vector2 m_TmpAxis;

    private void Start()
    {
        if (Camera.main != null)
            m_Cam = Camera.main.transform;
        else
            Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");

        m_Character = GetComponent<PlayerCharacter>();
        // m_playerDetection = GetComponent<PlayerDetection>();
    }


    private void Update()
    {

    }


    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

            // var sensibilityAxis = MainOptionManager.GetMainOptionManager().GetSensibilityStickChangeDirectionControl();

            if (Mathf.Abs(m_TmpAxis.x - h) > sensibilityAxis || Mathf.Abs(m_TmpAxis.y - v) > sensibilityAxis)
            {
                m_keepControl = false;
            }
        }


            if (m_Cam != null)
            {
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
                m_Move = v * Vector3.forward + h * Vector3.right;


        if (runCrouch == 0)
            m_Move *= 0.6f;

        bool crouch = runCrouch > 0;
        m_Character.Move(m_Move, crouch);
        if (action)
            m_Character.ActionContextual();
    }

    public void KeepControl()
    {
        m_keepControl = true;
        m_TmpCamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
        m_TmpCamRight = m_Cam.right;

        m_TmpAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
