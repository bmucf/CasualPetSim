using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotatable : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis;

    [SerializeField] private float rotateSpeed;

    public Transform cam;

    private Vector2 rotation;

    private bool rotateAllowed;

    private void Awake()
    {
        pressed.AddBinding("<Mouse>/leftButton");
        pressed.AddBinding("<Touchscreen>/press");

        axis.AddBinding("<Mouse>/delta");
        axis.AddBinding("<Touchscreen>/delta");

        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };

        pressed.Enable();
        axis.Enable();

        rotateSpeed = 0.3f;

        cam = Camera.main.transform;
    }

    private IEnumerator Rotate()
    {
        rotateAllowed = true;

        while (rotateAllowed)
        {
            rotation *= rotateSpeed;
            transform.Rotate(-cam.up, rotation.x, Space.World);
            transform.Rotate(cam.right, rotation.y, Space.World);
            yield return null;
        }
    }
}
