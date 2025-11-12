using UnityEngine;

public class RaycastClickHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;   // Explicit reference wiring
    [SerializeField] private string targetTag = "Pet";

    private void Update()
    {
        // Handle both mouse and touch
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick(Input.mousePosition);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            HandleClick(Input.GetTouch(0).position);
        }
    }

    private void HandleClick(Vector3 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                // Trigger your action here
                // Debug.Log($"Hit {hit.collider.name} with tag {targetTag}");

                // Look for ANY component that implements IActivatable
                var activatables = hit.collider.GetComponents<IActivatable>();
                foreach (var activatable in activatables)
                {
                    activatable.Activate();
                }

            }
        }
    }
}
public interface IActivatable
{
    void Activate();
}
