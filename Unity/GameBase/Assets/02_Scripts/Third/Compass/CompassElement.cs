using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CompassElement : MonoBehaviour
{
    [Tooltip("The marker on the compass for this element")]
    public CompassMarker CompassMarkerPrefab;

    [Tooltip("Text override for the marker, if it's a direction")]
    public string TextDirection;

    private Compass _compass;

    private void Awake()
    {
        _compass = FindObjectOfType<Compass>();

        var markerInstance = Instantiate(CompassMarkerPrefab);

        markerInstance.Initialize(this, TextDirection);
        _compass.RegisterCompassElement(transform, markerInstance);
    }

    void OnDestroy()
    {
        _compass.UnregisterCompassElement(transform);
    }

}
