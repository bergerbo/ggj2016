using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using XInputDotNetPure;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        public PlayerIndex playerIndex;
        private GamePadState state;
        private GamePadState prevState;
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        public float inversion;

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            prevState = state;
            state = GamePad.GetState(playerIndex);
            // read inputs
            float h = state.ThumbSticks.Left.X;
            float v = state.ThumbSticks.Left.Y;
            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.up, new Vector3(1, 0, 1)).normalized;
                m_Move = 2* inversion * v * m_CamForward + 2 * inversion * h * m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v * Vector3.forward + h * Vector3.right;
            }

            // pass all parameters to the character control script
            m_Character.Move(m_Move, false, false);

			var m_Rigidbody = m_Character.GetComponent<Rigidbody> ();
			m_Rigidbody.velocity = m_Move;
        }
    }
}
