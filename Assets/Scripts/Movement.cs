using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{    
    NavMeshAgent _agent; 
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 9f;
    [SerializeField]
    private float _gravity = 60f;
    [SerializeField]
    private float _jumpHeight = 20f;
    [SerializeField]
    private float _rotationSpeed = 2.5f;
    private Vector3 _direction, _velocity = Vector3.zero;
    private bool _isJumping = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("Navmesh agent not found.");
        }
        
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Character Controller not found.");
        }
    }

    void Update()
    {
        Movements();
    }

    private void Movements()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        transform.RotateAround(transform.position, Vector3.up, horizontal * _rotationSpeed);
        if (_controller.isGrounded)
        {
            if (_isJumping)
            {
                _isJumping = false;
            }

            _direction = new Vector3(0, 0, vertical);

            _velocity = _direction * _speed;

            _velocity = transform.TransformDirection(_velocity);

            if (Input.GetButtonDown("Jump"))
            {
                _isJumping = true;
                _velocity.y = _jumpHeight;
            }
        }
        _velocity.y -= _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.localPosition.x == 0)
        {
            other.transform.localPosition = new Vector3(1.3f, other.transform.localPosition.y, other.transform.localPosition.z);
        }
    }
}
