using UnityEngine;

public class TimerForInstructions : MonoBehaviour
{
    private float instructTime = 1f;
    public GameObject instruction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        instructTime -= Time.deltaTime;

        if (instructTime < 0)
        {
            instruction.SetActive(false);
            instructTime = Time.deltaTime;
        }
    }
}
