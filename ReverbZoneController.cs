using UnityEngine;
using FMODUnity;

public class ReverbZoneController : MonoBehaviour
{
    [SerializeField]
    private string fmodSnapshotPath = "snapshot:/RoomReverb";
    
    // Reverb setting snapshot 
    private FMOD.Studio.EventInstance snapshot;

    // Collider counter. This is here as multiple colliders are used for reverb triggering of temple and courtyard as they have non rectangular shapes.
    // The counter is used to ensure the reverb is taken off when the player has walked through multiple colliders and exitted all colliders.
    private int colliderCount = 0;



    private void OnTriggerEnter(Collider other)
    {
        // Check if player has entered collider
        if (other.CompareTag("Player"))
        {
            // Increment collider count
            colliderCount++;

            // If player is in a collider, create snapshot instance and apply to global master bus. 
            if (colliderCount == 1)
            {
            snapshot = FMODUnity.RuntimeManager.CreateInstance (fmodSnapshotPath);
            snapshot.start();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // Check player has exited collider
        if (other.CompareTag("Player"))
        {
            // Roll back collider count
            colliderCount--;
            
            // If player is not in collider stop and destroy FMOD snapshot, the reverb will then revert to the default setting (Fully dry)
            if (colliderCount == 0)
            {
            snapshot.release();
            snapshot.stop(0);
            }
        }
    }
}
