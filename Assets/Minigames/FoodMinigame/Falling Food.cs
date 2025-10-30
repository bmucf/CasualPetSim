using UnityEngine;

public class FallingFood : MonoBehaviour
{
    public float fallSpeed = 5f;

    SpawningFood foodSpawner;
    Rigidbody foodRb;
    RoomUIManager uiManager;

    void Start()
    {
        foodSpawner = GameObject.Find("Spawner").GetComponent<SpawningFood>();
        foodRb = GetComponent<Rigidbody>();
        uiManager = FindObjectOfType<RoomUIManager>();
    }

    void Update()
    {
        foodRb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);

        if (uiManager == null || uiManager.minigameHasStarted == false)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bowl"))
        {
            if (foodSpawner != null)
                foodSpawner.UpdateScore();

            Destroy(gameObject);
        }
    }
}
