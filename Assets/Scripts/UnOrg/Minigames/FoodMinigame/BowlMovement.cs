using UnityEngine;
using UnityEngine.Rendering;

public class BowlMovement : MonoBehaviour
{
    public Camera foodCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        
        pos.z = Vector3.Distance(foodCamera.transform.position, transform.position);

        Vector3 worldPos = foodCamera.ScreenToWorldPoint(pos);

        transform.position = new Vector3(worldPos.x, transform.position.y, transform.position.z);
    }
}
