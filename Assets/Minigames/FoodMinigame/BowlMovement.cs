using UnityEngine;
using UnityEngine.Rendering;

public class BowlMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        
        pos.z = Vector3.Distance(Camera.main.transform.position, transform.position);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);

        transform.position = new Vector3(transform.position.x, transform.position.y, worldPos.z);
    }
}
