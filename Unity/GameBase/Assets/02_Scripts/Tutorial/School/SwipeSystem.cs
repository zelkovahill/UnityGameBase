using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSystem : MonoBehaviour
{
    private Vector2 initialPosition;
    public GameObject Character;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Calculate(Input.mousePosition);
        }
    }

    private void Calculate(Vector3 finalPosition)
    {
        float disX = Mathf.Abs(initialPosition.x - finalPosition.x);
        float disY = Mathf.Abs(initialPosition.y - finalPosition.y);

        if (disX > 0 || disY > 0)
        {
            if (disX > disY)
            {
                if (initialPosition.x > finalPosition.x)
                {
                    Character.transform.position += new Vector3(-1.0f, 0.0f, 0.0f);
                }
            }
            else
            {
                Character.transform.position += new Vector3(1.0f, 0.0f, 0.0f);
            }
        }
        else
        {
            if (initialPosition.y > finalPosition.y)
            {
                Character.transform.position += new Vector3(0.0f, 0.0f, -1.0f);
            }
            else
            {
                Character.transform.position += new Vector3(0.0f, 0.0f, 1.0f);
            }
        }
    }
}
