using UnityEngine;

public class rotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 5 * Time.deltaTime);
    }
}
