using UnityEngine;

public class RotateRocko : MonoBehaviour
{
    public float rotationSpeed = 50f;

    public Transform target;

    public SwipeRocko swipeRocko;


    void Start()
    {
      
    }

    void Update() 
    {
        if (Input.touchCount > 0 && !swipeRocko.scrubRocko)
        {
            Touch touch = Input.GetTouch(0);
            float touchDeltaX = touch.deltaPosition.x;
            transform.RotateAround(target.position, Vector3.up, -touchDeltaX * rotationSpeed * Time.deltaTime);

        }
    }
}