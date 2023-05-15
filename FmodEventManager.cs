using UnityEngine;
using FMOD.Studio;
using FMODUnity;

// Event manager script so that an event can be referenced between scripts for parameter modulation and avoid triggering multiple events.
public class FmodEventManager : MonoBehaviour
{
    public static FmodEventManager Instance;

    [SerializeField] private string _eventPath;
    private EventInstance _mainEventInstance;

    public EventInstance MainEventInstance
    {
        get
        {   
            // Checks validity of event instance and initialises event.
            if (_mainEventInstance.isValid() == false)
            {
                _mainEventInstance = RuntimeManager.CreateInstance(_eventPath);
                _mainEventInstance.start();
            }

            // Returns event as main event. 
            return _mainEventInstance;
        }
    }

    private void Awake()
    {
        // Intialise instance of class upon loading of scene, if instance is not this class then destroy.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
