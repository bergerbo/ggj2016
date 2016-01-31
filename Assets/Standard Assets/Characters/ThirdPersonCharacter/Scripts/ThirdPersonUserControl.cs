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
        public float speed;

        private void Start()
        {

            m_Cam = Camera.main.transform;

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

            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.up, new Vector3(1, 0, 1)).normalized;
            m_Move = speed * inversion * v * m_CamForward + speed * inversion * h * m_Cam.right;

            // pass all parameters to the character control script
            m_Character.Move(m_Move, true);
        }
    }
}
