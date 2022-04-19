using FishNet.Object;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMove : NetworkBehaviour
    {
        public float Speed = 12f;
        public float JumpHeight = 3f;
        
        public Transform GroundCheck;
        public float GroundDistance = 0.4f;
        public LayerMask GroundMask;
        
        private CharacterController _controller;
        private Vector3 _velocity;
        private bool _isGrounded = true;
    
        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();

            if (_controller is null)
            {
                Debug.LogError("PlayerMove: No CharacterController found!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsOwner)
                return;
            
            _isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
                
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
        
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
        
            var direction = transform.right * horizontal + transform.forward * vertical;
        
            _controller.Move(direction * Speed * Time.deltaTime);
        
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y);
            }
        
            _velocity.y += Physics.gravity.y * Time.deltaTime;
        
            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}
