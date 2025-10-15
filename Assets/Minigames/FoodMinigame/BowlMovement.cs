using UnityEngine;
using UnityEngine.Rendering;

public class BowlMovement : MonoBehaviour
{
    public Camera food;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        
        pos.z = Vector3.Distance(food.transform.position, transform.position);

        Vector3 worldPos = food.ScreenToWorldPoint(pos);

        transform.position = new Vector3(worldPos.x, transform.position.y, transform.position.z);
    }
}
