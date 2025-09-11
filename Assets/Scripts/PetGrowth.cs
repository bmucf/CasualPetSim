using UnityEngine;

public class PetGrowth : MonoBehaviour
{
    [Range(0f, 0.1f)]
    public float growthRate;
    private Vector3 scaleChange;

    void Awake()
    {
        //makes pet face camera
        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.transform.Rotate(0, 180, 0);
    }

    void Update()
    {
        Growth();
    }

    public void Growth()
    {
        scaleChange = new Vector3(growthRate, growthRate, growthRate);
        gameObject.transform.localScale += scaleChange * Time.deltaTime;
    }
}
