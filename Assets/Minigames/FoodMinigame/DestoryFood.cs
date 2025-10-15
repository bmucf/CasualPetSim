using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private SpawningFood foodSpawner;
    public float fallSpeed;
    private Rigidbody foodRb;
    private UI uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foodSpawner = GameObject.Find("Spawner").GetComponent<SpawningFood>();
        foodRb = GetComponent<Rigidbody>();
        uiManager = GameObject.Find("UIManager").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        foodRb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);

        if (uiManager.minigameHasStarted == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bowl"))
        {
            
            foodSpawner.UpdateScore();
            Destroy(gameObject);

            Debug.Log("Food was Destroyed");
        }
    }
}
