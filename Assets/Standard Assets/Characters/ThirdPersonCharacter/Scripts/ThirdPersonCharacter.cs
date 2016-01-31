using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class ThirdPersonCharacter : MonoBehaviour
    {
        [SerializeField]
        float m_MovingTurnSpeed = 360;
        [SerializeField]
        float m_StationaryTurnSpeed = 180;
        [SerializeField]
        public float m_MoveSpeedMultiplier = 1f;

        Rigidbody m_Rigidbody;
        Animator m_Animator;
        const float k_Half = 0.5f;
        float m_TurnAmount;
        float m_ForwardAmount;

        static int pelicanRun = Animator.StringToHash("Base.PelicanRun");
        static int pelicanIdle = Animator.StringToHash("Base.PelicanIdle");

        private Vector3 positionStored;
        private bool dead = false;
        public float stabDistance;

        void Start()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Animator.speed = 1;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }


        public void Move(Vector3 move, bool crouch, bool jump)
        {
            if (isInAnimation() || dead)
            {
                return;
            }

            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);

            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;


            ApplyExtraTurnRotation();

            UpdateAnimator(move);

            m_Rigidbody.velocity = move;
        }

        public void TriggerAction(string actionName)
        {
            if (isInAnimation() || dead)
            {
                return;
            }

            positionStored = transform.position;
            m_Rigidbody.velocity = Vector3.zero;

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = 180;
            transform.eulerAngles = eulerAngles;

            m_Animator.SetTrigger(actionName);
        }

        public bool isInAnimation()
        {
            int nameHash = m_Animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            return nameHash != pelicanRun && nameHash != pelicanIdle;
        }

        public void Stab()
        {
            if (isInAnimation() || dead)
            {
                return;
            }

            m_Animator.SetTrigger("Stab");

            var hitInfo = new RaycastHit();
            if(Physics.Raycast(transform.position, transform.forward, out hitInfo, stabDistance))
            {
                if(hitInfo.collider.tag == "Player" || hitInfo.collider.tag == "NPC"){
                    hitInfo.collider.GetComponent<ThirdPersonCharacter>().Die();
                }
            }
        }


        void UpdateAnimator(Vector3 move)
        {
            // update the animator parameters
            m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        }


        void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }


        //public void OnAnimatorMove()
        //{
        //	// we implement this function to override the default root motion.
        //	// this allows us to modify the positional speed before it's applied.
        //	if (Time.deltaTime > 0)
        //	{
        //		Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
        //		v.y = m_Rigidbody.velocity.y;
        //		m_Rigidbody.velocity = v;
        //	}
        //}

        public void Die()
        {
            dead = true;
            m_Animator.SetBool("Dead", true);
        }

    }
}
