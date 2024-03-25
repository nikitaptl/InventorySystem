using UnityEngine;

// необходимо чтобы название скрипта и название класса совпадали
namespace _Source.Core.CharacterManagement
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody rigid;
        [SerializeField] private Transform mainCamera;
        private readonly float _jumpForce = 3.5f;
        private readonly float _walkingSpeed = 2f;
        private readonly float _runningSpeed = 6f;
        private float _currentSpeed;

        private float _animationInterpolation = 1f;
        private static readonly int X = Animator.StringToHash("x");
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int Magnitude = Animator.StringToHash("magnitude");

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Run()
        {
            _animationInterpolation = Mathf.Lerp(_animationInterpolation, 1.5f, Time.deltaTime * 3);
            animator.SetFloat(X, Input.GetAxis("Horizontal") * _animationInterpolation);
            animator.SetFloat(Y, Input.GetAxis("Vertical") * _animationInterpolation);

            _currentSpeed = Mathf.Lerp(_currentSpeed, _runningSpeed, Time.deltaTime * 3);
        }

        void Walk()
        {
            _animationInterpolation = Mathf.Lerp(_animationInterpolation, 1f, Time.deltaTime * 3);
            animator.SetFloat(X, Input.GetAxis("Horizontal") * _animationInterpolation);
            animator.SetFloat(Y, Input.GetAxis("Vertical") * _animationInterpolation);

            _currentSpeed = Mathf.Lerp(_currentSpeed, _walkingSpeed, Time.deltaTime * 3);
        }

        private void Update()
        {
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(rotation.eulerAngles.x, mainCamera.rotation.eulerAngles.y,
                rotation.eulerAngles.z);
            transform.rotation = rotation;

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    Walk();
                }
                else
                {
                    Run();
                }
            }
            else
            {
                Walk();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump");
            }
        }

        void FixedUpdate()
        {
            Vector3 camF = mainCamera.forward;
            Vector3 camR = mainCamera.right;

            camF.y = 0;
            camR.y = 0;
            Vector3 movingVector;

            movingVector =
                Vector3.ClampMagnitude(
                    camF.normalized * Input.GetAxis("Vertical") * _currentSpeed +
                    camR.normalized * Input.GetAxis("Horizontal") * _currentSpeed, _currentSpeed);

            animator.SetFloat(Magnitude, movingVector.magnitude / _currentSpeed);
            Debug.Log(movingVector.magnitude / _currentSpeed);

            rigid.velocity = new Vector3(movingVector.x, rigid.velocity.y, movingVector.z);
            rigid.angularVelocity = Vector3.zero;
        }

        public void Jump()
        {
            rigid.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}   