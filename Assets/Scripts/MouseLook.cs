using UnityEngine;

namespace Assets.Scripts
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private float MouseSensitivity = 100f;
        [SerializeField] private Transform PlayerBody;
    
        float _xRotation;
    
        // Start is called before the first frame update
        void Start()
        {
            // Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            var mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        
            PlayerBody.Rotate(Vector3.up * mouseX);
        }
    }
}