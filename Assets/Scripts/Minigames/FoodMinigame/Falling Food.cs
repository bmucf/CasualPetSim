using System.Linq;
using UnityEngine;

public class FallingFood : MonoBehaviour
{
    private SpawningFood spawningFood;
    private Rigidbody rb;

    [Header("Gravity Settings")]
    public bool useGravity = true;   // toggle Unity's gravity
    public Vector3 customGravity = new Vector3(0, -7.81f, 0); // per-object gravity
    public float maxFallSpeed = -2f; // negative value for downward Y


    [Header("LifeSpan")]
    public float lifeSpan = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    void FixedUpdate()
    {
        // Toggle Unity's built-in gravity
        rb.useGravity = useGravity;

        // Apply custom gravity manually if Unity's gravity is off
        if (!useGravity)
        {
            rb.AddForce(customGravity, ForceMode.Acceleration);
        }

        Vector3 v = rb.linearVelocity;

        // Clamp only the Y axis (falling down)
        if (v.y < maxFallSpeed)
        {
            v.y = maxFallSpeed;
            rb.linearVelocity = v;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bowl"))
        {
            Debug.Log("A food hit has been collected");
            spawningFood.UpdateScore();
            Destroy(gameObject);
        }
    }
    public void Init(SpawningFood spawner)
    {
        spawningFood = spawner;
    }

}
