using TMPro;
using UnityEngine;

public class SwipeRocko : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    public float cleanCount = 0;
    public TextMeshProUGUI cleanText;
    public bool scrubRocko;
    public Camera bathCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;

            Ray ray = bathCam.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    scrubRocko = true;
                }
                else
                {
                    scrubRocko = false;
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && scrubRocko)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if (endTouchPosition.x < startTouchPosition.x)
            {
                AddClean(); 
            }

            if (endTouchPosition.x > startTouchPosition.x)
            {
                AddClean();
            }
            scrubRocko = false;
        }
    }

    void AddClean()
    {
        cleanCount++;
        cleanText.text = "Clean Progress " + cleanCount;
    }
}
