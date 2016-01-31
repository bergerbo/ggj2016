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
    }
}
