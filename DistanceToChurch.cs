using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class DistanceToChurch : MonoBehaviour
{   

    public string fmodEventPath = "event:/SoundTrack(Jordan)";
    public string distanceParameterName = "ChurchDistance";
    public Transform listenerTransform;

    private EventInstance audioEvent;
    private float distance;

    void Start()
    {

        // If there is no transform that we are listening, listen on camera position. 
        if (listenerTransform == null)
        {
            listenerTransform = Camera.main.transform;
        }

        // Initialise event instance to main FMOD instance to avoid triggering multiple times.
        audioEvent = FmodEventManager.Instance.MainEventInstance;

        // Start event instance.
        audioEvent.start();
        
    }

    void Update()
    {  

        // Compare temple postion to camera position.
        distance = Vector3.Distance(transform.position, listenerTransform.position);

        // if distance is greater than the maximum of the FMOD paramter, set to maximum value.
        if(distance > 200)
        {
            distance = 200;
        }

        // Set parameter value.
        audioEvent.setParameterByName(distanceParameterName, distance);
    }

    void OnDestroy()
    {   
        // Stop audio event with fadeout
        audioEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        // Destroy FMOD event.
        audioEvent.release();
    }
}
