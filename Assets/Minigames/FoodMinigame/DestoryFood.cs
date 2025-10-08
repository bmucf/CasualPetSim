using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private SpawningFood foodSpawner;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foodSpawner = GameObject.Find("Spawner").GetComponent<SpawningFood>();
    }

    // Update is called once per frame
    void Update()
    {

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
