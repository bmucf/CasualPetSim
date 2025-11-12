using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSheep : MonoBehaviour
{
    private SheepSpawning callSheep;

    public Rigidbody sheepRb;

    public float speedForce;
    public float jumpForce;
    public float gravityForce;
    
    public int maxTaps = 0;
    private int currentTaps = 0;

    void Start()
    {
        callSheep = GameObject.Find("SheepSpawner").GetComponent<SheepSpawning>();
    }


    void Update()
    {
        transform.Translate(Vector3.right * speedForce * Time.deltaTime);

        if (transform.position.x >= 19)
        {
            callSheep.SheepScore();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            SceneManager.LoadScene("Home");
            Debug.Log("Sheep hit the fence");
            Destroy(gameObject);
        }
    }

    public void SheepJump()
    {
        if (currentTaps <= maxTaps)
        {
            sheepRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currentTaps++;
        }

    }
}
