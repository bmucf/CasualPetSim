using TMPro;
using UnityEngine;

public class SwipeRocko : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    public float cleanCount = 50;
    public TextMeshProUGUI cleanText;
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
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
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
        }
    }

    void AddClean()
    {
        cleanCount++;
        cleanText.text = "Clean Progress " + cleanCount;
    }
}
