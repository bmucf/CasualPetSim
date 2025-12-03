using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class SwipeRocko : MonoBehaviour
{
    [SerializeField] private string currentPetID;

    //private Vector2 startTouchPosition;
    //private Vector2 endTouchPosition;

    public float cleanCount = 0;
    public TextMeshProUGUI cleanText;
    public bool scrubRocko;
    public bool minigameActive;
    //public Camera bathCam;


    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //    {
    //        startTouchPosition = Input.GetTouch(0).position;

    //        Ray ray = bathCam.ScreenPointToRay(Input.GetTouch(0).position);
    //        if (Physics.Raycast(ray, out RaycastHit hit))
    //        {
    //            if (hit.transform == transform)
    //            {
    //                scrubRocko = true;
    //            }
    //            else
    //            {
    //                scrubRocko = false;
    //            }
    //        }
    //    }

    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && scrubRocko)
    //    {
    //        endTouchPosition = Input.GetTouch(0).position;

    //        if (endTouchPosition.x < startTouchPosition.x)
    //        {
    //            AddClean(); 
    //        }

    //        if (endTouchPosition.x > startTouchPosition.x)
    //        {
    //            AddClean();
    //        }
    //        scrubRocko = false;
    //    }
    //}

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
            currentPetID = SessionContent.CurrentPetID;

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
            // transform.Rotate(cam.right, rotation.y, Space.World);
            yield return null;
        }
    }


    void AddClean()
    {
        cleanCount++;
        cleanText.text = "Cleanliness: " + cleanCount * 5 + "%";
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
        float totalReduce = cleanCount * 5;
        DataPersistenceManager.instance.UpdatePetStat(petID, s => s.dirtinessMain -= totalReduce);
    }
}
