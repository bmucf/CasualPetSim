// 12/3/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwipeRocko : MonoBehaviour
{
    [SerializeField] private string currentPetID;

    public float cleanCount = 0;
    public TextMeshProUGUI cleanText;
    public bool scrubRocko;
    public bool minigameActive;

    [SerializeField] private InputAction pressed, axis, screenPos;

    [SerializeField] private float rotateSpeed;

    public Transform cam;

    private Vector2 rotation;

    private bool rotateAllowed;

    private Vector3 currentScreenPos;

    [SerializeField]
    private float timer = 100f;
    private float timerTarget;

    Camera camera;

    private bool isGrabbed
    {
        get
        {
            Ray ray = camera.ScreenPointToRay(currentScreenPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.transform == transform;
            }
            return false;
        }
    }

    private void Awake()
    {
        timerTarget = timer;

        camera = Camera.main;

        pressed.AddBinding("<Mouse>/leftButton");
        pressed.AddBinding("<Touchscreen>/press");

        axis.AddBinding("<Mouse>/delta");
        axis.AddBinding("<Touchscreen>/delta");

        screenPos.AddBinding("<Mouse>/Position");
        screenPos.AddBinding("<Touchscreen>/Position");

        pressed.Enable();
        axis.Enable();
        screenPos.Enable();

        pressed.performed += _ => { if (!isGrabbed) StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
        screenPos.performed += context => { currentScreenPos = context.ReadValue<Vector2>(); };

        rotateSpeed = 0.3f;

        cam = Camera.main.transform;
    }

    void Start()
    {
        if (SessionContent.CurrentPetID != null)
        {
            currentPetID = SessionContent.CurrentPetID;
            Debug.Log($"CurrentPetID set to: {currentPetID}");
        }
        else
        {
            Debug.LogError("SessionContent.CurrentPetID is null. Ensure it is set correctly before starting the minigame.");
        }

        minigameActive = true;
    }

    private void Update()
    {
        if (isGrabbed)
        {
            if (timer < 0)
            {
                AddClean();
                timer = timerTarget;
            }
            else
            {
                timer--;
            }
        }

        if (minigameActive)
        {
            if (cleanCount == 20)
            {
                minigameActive = false;
                ShowerEnd();
            }
        }
    }

    private IEnumerator Rotate()
    {
        rotateAllowed = true;

        while (rotateAllowed && !isGrabbed)
        {
            rotation *= rotateSpeed;
            transform.Rotate(-cam.up, rotation.x, Space.World);
            yield return null;
        }
    }

    void AddClean()
    {
        cleanCount++;
        cleanText.text = "Cleanliness: " + cleanCount * 5 + "%";
        Debug.Log($"Cleanliness updated: {cleanCount * 5}%");
    }

    public void ShowerEnd()
    {
        minigameActive = false;

        // Calling Rewards Function Goes Here
        ApplyCleaningRewardToPet(currentPetID);

        // Go back to "Home" scene
        SceneManager.LoadScene("Home");
    }

    private void ApplyCleaningRewardToPet(string petID)
    {
        if (string.IsNullOrEmpty(petID))
        {
            Debug.LogError("Pet ID is null or empty. Cannot apply cleaning reward.");
            return;
        }

        float totalReduce = cleanCount * 5;
        Debug.Log($"Applying cleaning reward to pet with ID: {petID}, reducing dirtiness by: {totalReduce}");

        DataPersistenceManager.instance.UpdatePetStat(petID, s =>
        {
            Debug.Log($"Updating pet stats for ID: {petID}");
            s.dirtinessMain -= totalReduce;
        });
    }
}