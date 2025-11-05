using UnityEngine;
using Unity.Cinemachine;

public class CinemachineDynamicTargetSwitcher : MonoBehaviour
{
    [Header("Cinemachine Reference")]
    public CinemachineCamera cinemachineCam;

    [Header("Target Settings")]
    public string targetName;   // Name of object to track
    public bool requireActive = true;         // Only switch if active

    private Transform currentTarget;

    void Start()
    {
        if (cinemachineCam == null)
            cinemachineCam = GetComponent<CinemachineCamera>();
    }

    void Update()
    {
        // Find a matching object
        GameObject found = GameObject.Find(targetName);
        if (found == null)
            return;

        bool valid = !requireActive || found.activeInHierarchy;

        // If valid and not already set as target, update
        if (valid && found.transform != currentTarget)
        {
            currentTarget = found.transform;
            cinemachineCam.Follow = currentTarget;
            cinemachineCam.LookAt = currentTarget;
            Debug.Log($"Cinemachine target switched to: {currentTarget.name}");
        }
        // If it's no longer valid (inactive), clear or revert to default
        else if (!valid && currentTarget != null)
        {
            currentTarget = null;
            cinemachineCam.Follow = null;
            cinemachineCam.LookAt = null;
            // Debug.Log($"Target '{targetName}' became inactive — cleared camera target.");
        }
    }
}
