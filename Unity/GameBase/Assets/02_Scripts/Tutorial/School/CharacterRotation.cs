using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    private float rotSpeed = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            this.rotSpeed = 10000;
        }
        transform.Rotate(0, this.rotSpeed * Time.deltaTime, 0);

        rotSpeed *= 0.99f;
    }
}
