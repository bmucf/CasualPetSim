using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Collider))]
public class VFXOnTap : MonoBehaviour
{
    private VisualEffect vfx;

    private void Awake()
    {
        vfx = GetComponent<VisualEffect>();
        if (vfx == null)
        {
            Debug.LogError("VFXOnTap: No VisualEffect component found on this GameObject.");
        }
    }

    private void OnMouseDown()
    {
        if (vfx == null) return;


        vfx.SendEvent("OnTap");
    }
}

