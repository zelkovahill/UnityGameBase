using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public RectTransform CompassRect;
    public float VisibilityAngle = 180f;
    public float HeightDifferenceMultiplier = 2f;
    public float MinScale = 0.5f;
    public float DistanceMinScale = 50f;
    public float CompassMarginRatio = 0.8f;

    public GameObject MargerDirectionPrefab;

    public Transform _PlayerTransform;
    private Dictionary<Transform, CompassMarker> _ElementsDictionnary = new Dictionary<Transform, CompassMarker>();

    private float _widthMultiplier;
    private float _heightOffset;

    private void Awake()
    {
        // ThirdPersonController thirdPersonController = FindObjectOfType<ThirdPersonController>();

        _PlayerTransform = Camera.main.transform;

        _widthMultiplier = CompassRect.rect.width / VisibilityAngle;
        _heightOffset = CompassRect.rect.height / 2;
    }

    private void Update()
    {

        // this is all very WIP, and needs to be reworked
        foreach (var element in _ElementsDictionnary)
        {
            float distanceRatio = 1;
            float heightDifference = 0;
            float angle;

            if (element.Value.IsDirection)
            {
                angle = Vector3.SignedAngle(_PlayerTransform.forward,
                    element.Key.transform.localPosition.normalized, Vector3.up);
            }
            else
            {
                Vector3 targetDir = (element.Key.transform.position - _PlayerTransform.position).normalized;
                targetDir = Vector3.ProjectOnPlane(targetDir, Vector3.up);
                Vector3 playerForward = Vector3.ProjectOnPlane(_PlayerTransform.forward, Vector3.up);
                angle = Vector3.SignedAngle(playerForward, targetDir, Vector3.up);

                Vector3 directionVector = element.Key.transform.position - _PlayerTransform.position;

                heightDifference = (directionVector.y) * HeightDifferenceMultiplier;
                heightDifference = Mathf.Clamp(heightDifference, -CompassRect.rect.height / 2 * CompassMarginRatio,
                    CompassRect.rect.height / 2 * CompassMarginRatio);

                distanceRatio = directionVector.magnitude / DistanceMinScale;
                distanceRatio = Mathf.Clamp01(distanceRatio);
            }

            if (angle > -VisibilityAngle / 2 && angle < VisibilityAngle / 2)
            {
                element.Value.CanvasGroup.alpha = 1;
                element.Value.CanvasGroup.transform.localPosition = new Vector2(_widthMultiplier * angle,
                    heightDifference + _heightOffset);
                element.Value.CanvasGroup.transform.localScale =
                    Vector3.one * Mathf.Lerp(1, MinScale, distanceRatio);
            }
            else
            {
                element.Value.CanvasGroup.alpha = 0;
            }

        }
    }

    public void RegisterCompassElement(Transform element, CompassMarker marker)
    {
        marker.transform.SetParent(CompassRect);

        _ElementsDictionnary.Add(element, marker);
    }

    public void UnregisterCompassElement(Transform element)
    {
        if (_ElementsDictionnary.TryGetValue(element, out CompassMarker marker) && marker.CanvasGroup != null)
            Destroy(marker.CanvasGroup.gameObject);
        _ElementsDictionnary.Remove(element);
    }
}

