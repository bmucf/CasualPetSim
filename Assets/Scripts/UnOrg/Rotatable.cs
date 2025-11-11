using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotatable : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis, screenPos;

    [SerializeField] private float rotateSpeed;

    public Transform cam;

    private Vector2 rotation;

    private bool rotateAllowed;

    private Vector3 currentScreenPos;

    Camera camera;

    private bool isGrabbed
    {
        get
        {
            Ray ray = camera.ScreenPointToRay(currentScreenPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.transform == transform;
            }
            return false;
        }
    }

    private void Awake()
    {
        camera = Camera.main;

        pressed.AddBinding("<Mouse>/leftButton");
        pressed.AddBinding("<Touchscreen>/press");

        axis.AddBinding("<Mouse>/delta");
        axis.AddBinding("<Touchscreen>/delta");

        screenPos.AddBinding("<Mouse>/Position");
        screenPos.AddBinding("<Touchscreen>/Position");

        pressed.Enable();
        axis.Enable();
        screenPos.Enable();

        pressed.performed += _ => { if(!isGrabbed) StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
        screenPos.performed += context => { currentScreenPos = context.ReadValue<Vector2>(); };

        rotateSpeed = 0.3f;

        cam = Camera.main.transform;
    }

    private IEnumerator Rotate()
    {
        rotateAllowed = true;

        while (rotateAllowed && !isGrabbed)
        {
            rotation *= rotateSpeed;
            transform.Rotate(-cam.up, rotation.x, Space.World);
            // transform.Rotate(cam.right, rotation.y, Space.World);
            yield return null;
        }
    }
}
