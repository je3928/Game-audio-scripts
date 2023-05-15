using UnityEngine;
using FMOD.Studio;
using System.Collections;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private string _parameterName;
    [SerializeField] private float _enterValue;
    [SerializeField] private float _exitValue;
    [SerializeField] private float _interpolationTime;

    private Coroutine _interpolationCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        // If player enters collider object set parameter to specified value. 
        if (other.CompareTag("Player"))
        {
            float parameterValue = _enterValue;
            SetParameterValue(parameterValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If player exits collider object set parameter to specified value. 
        if (other.CompareTag("Player"))
        {
            float parameterValue = _exitValue;
            SetParameterValue(parameterValue);
        }
    }

    public void SetParameterValue(float value)
    {

        // Destroy previous coroutine if still in operation
        if (_interpolationCoroutine != null)
        {
            StopCoroutine(_interpolationCoroutine);
        }

        // Start coroutine in order to apply calculations over many frames
        _interpolationCoroutine = StartCoroutine(InterpolateParameterValue(value));
    }

    private IEnumerator InterpolateParameterValue(float targetValue)
    {
        // Get main event instance to avoid overwriting.
        EventInstance eventInstance = FmodEventManager.Instance.MainEventInstance;

        // Get start time
        float startTime = Time.time;

        // Initialise start value
        float startValue = 0f;

        // Get FMOD paramter.
        eventInstance.getParameterByName(_parameterName, out startValue);

        // Initialise elapsed time
        float elapsedTime = 0f;

        // If elapsed time is less than interpolation time, linearly interpolate parameter.
        while (elapsedTime < _interpolationTime)
        {
            // Calculate elapsed time
            elapsedTime = Time.time - startTime;

            // Current time value
            float t = Mathf.Clamp01(elapsedTime / _interpolationTime);

            // Linear interpolation
            float value = Mathf.Lerp(startValue, targetValue, t);

            // Set FMOD paramter value.
            eventInstance.setParameterByName(_parameterName, value);

            yield return null;
        }

        // When interpolation is complete, set parameter to final value.
        eventInstance.setParameterByName(_parameterName, targetValue);
    }
}
