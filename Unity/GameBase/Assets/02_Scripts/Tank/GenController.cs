using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenController : MonoBehaviour
{
    public GameObject MonsterTemp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray cast = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(cast, out hit))
            {
                if (hit.collider.tag == "Ground")
                {
                    GameObject temp = (GameObject)Instantiate(MonsterTemp);
                    temp.transform.position = hit.point + new Vector3(0.0f, 1.0f, 0.0f); // 몬스터 레이 위치 좌표에 생성
                }
                Debug.DrawLine(cast.origin, hit.point, Color.red, 2.0f);    // 디버그 빨간 라인을 2초 동안 그려준다.

                Debug.Log("Hit => " + hit.collider.name);
            }
        }
    }
}
