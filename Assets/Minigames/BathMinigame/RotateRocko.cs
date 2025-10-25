using UnityEngine;

public class RotateRocko : MonoBehaviour
{
    public Transform target;        // Object to orbit around
    public float distance = 5f;     // Distance from target
    public float rotationSpeed = 0.2f;

    private Vector2 turn = Vector2.zero;

    void Start()
    {
        if (target == null)
            Debug.LogWarning("RotateRocko: No target assigned!");
    }

    void LateUpdate()
    {
        // --- Touch input (Simulator & Mobile) ---
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Calculate input deltas
                turn.x += touch.deltaPosition.x * rotationSpeed;
                turn.y -= touch.deltaPosition.y * rotationSpeed;

                // Clamp vertical angle
                turn.y = Mathf.Clamp(turn.y, -80f, 80f);
            }
        }

        // --- Mouse input (Editor/Game View) ---
        else if (Input.GetMouseButton(0))
        {
            turn.x += Input.GetAxis("Mouse X") * rotationSpeed * 10f;
            turn.y -= Input.GetAxis("Mouse Y") * rotationSpeed * 10f;
            turn.y = Mathf.Clamp(turn.y, -80f, 80f);
        }

        if (target == null) return;

        // Compute rotation and position
        Quaternion rotation = Quaternion.Euler(turn.y, turn.x, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        // Move camera around target
        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}