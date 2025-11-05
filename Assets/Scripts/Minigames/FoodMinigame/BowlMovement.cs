using UnityEngine;
using UnityEngine.InputSystem;

public class BowlMovement : MonoBehaviour
{
    [Header("Camera that looks at the food area")]
    public Camera foodCamera;

    [Header("Movement Bounds")]
    public float minX = -5f;
    public float maxX = 5f;

    private InputAction clickAction;
    private InputAction positionAction;

    private void OnEnable()
    {
        clickAction = new InputAction(type: InputActionType.Button);
        clickAction.AddBinding("<Mouse>/leftButton");
        clickAction.AddBinding("<Touchscreen>/primaryTouch/press");

        positionAction = new InputAction(type: InputActionType.Value, binding: "<Pointer>/position");

        clickAction.Enable();
        positionAction.Enable();
    }

    private void OnDisable()
    {
        clickAction.Disable();
        positionAction.Disable();
    }

    private void Update()
    {
        if (foodCamera == null) return;

        if (clickAction.IsPressed())
        {
            Vector2 screenPos = positionAction.ReadValue<Vector2>();

            if (foodCamera.orthographic)
            {
                Vector3 pos = new Vector3(screenPos.x, screenPos.y, Mathf.Abs(foodCamera.transform.position.z - transform.position.z));
                Vector3 worldPos = foodCamera.ScreenToWorldPoint(pos);

                float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            }
            else
            {
                Plane plane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
                Ray ray = foodCamera.ScreenPointToRay(screenPos);

                if (plane.Raycast(ray, out float enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);

                    float clampedX = Mathf.Clamp(hitPoint.x, minX, maxX);
                    transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
                }
            }
        }

    }
}