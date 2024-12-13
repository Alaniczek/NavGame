using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // Add this line to include TextMeshPro namespace

public class GetLocation : MonoBehaviour
{
    [SerializeField] TextMeshPro debugText; // Corrected to TextMeshProUGUI

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location not enabled on device or app does not have permission to access location");
            if (debugText != null)
            {
                debugText.text = "Location not enabled on device or app does not have permission to access location";
            }
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            if (debugText != null)
            {
                debugText.text = "Timed out";
            }
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            if (debugText != null)
            {
                debugText.text = "Unable to determine device location";
            }
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            string locationInfo = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            Debug.Log(locationInfo);
            if (debugText != null)
            {
                debugText.text = locationInfo;
            }
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}