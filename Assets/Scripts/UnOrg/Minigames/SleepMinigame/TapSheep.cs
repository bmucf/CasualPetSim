using UnityEngine;
using UnityEngine.InputSystem;

public class TapSheep : MonoBehaviour
{
    private InputAction tapAction;
    private InputAction positionAction;
    public MoveSheep moveSheep;

    private void OnEnable()
    {
        tapAction = new InputAction(type: InputActionType.Button);
        tapAction.AddBinding("<Mouse>/leftButton");
        tapAction.AddBinding("<Touchscreen>/primaryTouch/tap");

        positionAction = new InputAction(type: InputActionType.Value, binding: "<Pointer>/position");

        tapAction.Enable();
        positionAction.Enable();
    }

    private void OnDisable()
    {
        tapAction.Disable();
        positionAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        if(tapAction.WasPerformedThisFrame())
        {
            Vector2 screenPos = positionAction.ReadValue<Vector2>();

            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Sheep"))
                {
                    MoveSheep tappedSheep = hit.collider.GetComponent<MoveSheep>();

                    if (tappedSheep != null)
                    {
                        tappedSheep.SheepJump();
                        // Debug.Log("Sheep has Jumped");
                    }
                }
            }
        }
    }
}
