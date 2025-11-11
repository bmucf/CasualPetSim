using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSheep : MonoBehaviour
{
    public float speedForce;
    private SheepSpawning callSheep;
    public Rigidbody sheepRb;
    public float jumpForce;
    public float gravityForce;
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        callSheep = GameObject.Find("SheepSpawner").GetComponent<SheepSpawning>();
 

    }

    // Update is called once per frame
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
            callSheep.SheepEnd();            
            Debug.Log("Sheep hit the fence");
            SceneManager.LoadScene("Home");
            Destroy(gameObject);
        }
    }

    public void SheepJump()
    {
        sheepRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
