using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    RectTransform rt;
    Rect lastSafe;
    Vector2Int lastRes;
    ScreenOrientation lastOrient;

    void Awake() { rt = GetComponent<RectTransform>(); ApplySafeArea(); }
    void OnEnable() => ApplySafeArea();

    void Update()
    {
        if (Screen.safeArea != lastSafe || Screen.width != lastRes.x || Screen.height != lastRes.y || Screen.orientation != lastOrient)
            ApplySafeArea();
    }

    void ApplySafeArea()
    {
        var safe = Screen.safeArea;
        lastSafe = safe;
        lastRes = new Vector2Int(Screen.width, Screen.height);
        lastOrient = Screen.orientation;

        var anchorMin = safe.position;
        var anchorMax = safe.position + safe.size;
        anchorMin.x /= Screen.width; anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width; anchorMax.y /= Screen.height;

        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
