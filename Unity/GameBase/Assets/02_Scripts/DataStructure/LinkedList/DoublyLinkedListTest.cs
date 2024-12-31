using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublyLinkedListTest : MonoBehaviour
{
    // 이중 연결 리스트 객체 생성
    private DoublyLinkedList<int> doublyLinkedList = new DoublyLinkedList<int>(1);

    private void Start()
    {
        doublyLinkedList.Add(2);    // 리스트 마지막에 2 추가
        doublyLinkedList.Add(3);    // 리스트 마지막에 3 추가
        doublyLinkedList.Add(4);    // 리스트 마지막에 4 추가
        doublyLinkedList.AddHead(0);    // 리스트 헤드에 0 추가

        // 리스트의 데이터 출력
        Debug.Log("Forward Data : ");
        foreach (var item in doublyLinkedList.GetData())
        {
            Debug.Log(item);    // 0, 1, 2, 3, 4 출력
        }

        Debug.Log("Reversed Data : ");
        foreach (var item in doublyLinkedList.GetDataReversed())
        {
            Debug.Log(item);    // 4, 3, 2, 1, 0 출력
        }

        // 특정 노드 검색
        var node = doublyLinkedList.Find(3);    // 3을 가진 노드 검색
        Debug.Log($"Node found : {node.Data}");

        // 특정 노드 검색
        var nodeAtIndex = doublyLinkedList.GetAt(2);    // 2번째 위치의 노드 찾기
        Debug.Log($"Node at index 2 : {nodeAtIndex.Data}");

        // 리스트에서 특정 노드 제거
        doublyLinkedList.RemoveNode(node);  // 노드 3 제거
        Debug.Log("After removing node 3:");
        foreach (var item in doublyLinkedList.GetData())
        {
            Debug.Log(item);  // 0, 1, 2, 4
        }

        // 리스트 반전
        doublyLinkedList.Reverse();  // 리스트 뒤집기
        Debug.Log("After reversing the list:");
        foreach (var item in doublyLinkedList.GetData())
        {
            Debug.Log(item);  // 4, 2, 1, 0
        }

        // 데이터 포함 여부 확인
        Debug.Log($"Contains 2: {doublyLinkedList.Contains(2)}");
        Debug.Log($"Contains 3: {doublyLinkedList.Contains(3)}");

    }

}
