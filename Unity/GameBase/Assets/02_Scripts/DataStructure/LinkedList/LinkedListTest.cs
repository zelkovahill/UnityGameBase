using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LinkedListTest : MonoBehaviour
{
    // 게임 내에서 사용할 단일 연결 리스트
    private SinglyLinkedList<int> list = new SinglyLinkedList<int>();

    private void Start()
    {
        // 리스트에 게임 오브젝트 순차적으로 추가
        list.AddFirst(1);
        list.AddFirst(2);
        list.AddFirst(3);

        // 리스트의 첫 번째 오브젝트 처리
        Debug.Log("First element: " + list.GetElementByIndex(0));

        // 리스트 길이 출력
        Debug.Log("Length of the list: " + list.Length());

        // 리스트에서 마지막 요소 삭제
        list.DeleteLast();
        Debug.Log("Length after delete last: " + list.Length());


        // 리스트의 모든 데이터를 순차적으로 출력
        foreach (var item in list.GetListData())
        {
            Debug.Log("Item: " + item);
        }

    }



}
