using System;
using UnityEngine;

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


        public void Move(Vector3 move, bool withVelocity)
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

        }

        public bool TriggerAction(string actionName)
        {
            if (isInAnimation() || dead)
            {
                return false;
            }

            positionStored = transform.position;
            m_Rigidbody.velocity = Vector3.zero;

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.y = 180;
            transform.eulerAngles = eulerAngles;

            m_Animator.SetTrigger(actionName);
			
			if(actionName=="Up"){GameObject.Find ("Audio").GetComponent<Soundscript>().Play_sound ("Cri1");}
			if(actionName=="Down"){GameObject.Find ("Audio").GetComponent<Soundscript>().Play_sound ("Cri2");}
			if(actionName=="Left"){GameObject.Find ("Audio").GetComponent<Soundscript>().Play_sound ("Cri3");}
			if(actionName=="Right"){GameObject.Find ("Audio").GetComponent<Soundscript>().Play_sound ("Cri4");}

            return true;
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

            // Ray ray = (transform.position, transform.forward);
            RaycastHit hitInfo;
            if(Physics.Raycast(transform.position + transform.up * 2f, transform.forward, out hitInfo, stabDistance ))
            {
            	var tpc = hitInfo.collider.GetComponent<ThirdPersonCharacter>();
                if(tpc != null){
                	tpc.Die();
                } else {
                	Debug.Log(hitInfo.collider.name);
                }
            }
			GameObject.Find ("Audio").GetComponent<Soundscript>().Play_sound ("Stab");
            m_Animator.SetTrigger("Stab");
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

        public void Die()
        {
            dead = true; 	
        	var player = gameObject.GetComponentInParent<Player>();
            
            if(player != null){
                player.god.DeletePlayer(player.playerIndex);
        		GameManager.GetInstance().PlayerDied(player.playerIndex);
                
                Debug.Log(player.playerIndex);
        	}

            m_Animator.SetBool("Dead", true);
        	Destroy(transform.parent.gameObject, 5f);
        }

    }
