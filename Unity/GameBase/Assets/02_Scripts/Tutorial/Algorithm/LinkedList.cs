using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LinkedListNode
{
    // 현재 노드의 데이터
    public int Data;

    // 현재 노드가 가리킬 다음 노드
    public LinkedListNode Next;

    // 노드의 생성
    public LinkedListNode(int data)
    {
        Data = data;
        Next = null;
    }
}

public class LinkedList
{
    public LinkedListNode Head;

    public LinkedList()
    {
        Head = null;
    }

    // 링크드 리스트의 삽입
    public void Insert(int data)
    {
        var newNode = new LinkedListNode(data);

        if (Head == null)
        {
            Head = newNode;
        }
        // 링크드 리스트의 노드가 하나라도 존재하면
        // 끝 노드의 next에 새 노드를 연결
        else
        {
            var current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }


    // 링크드 리스트 전체 출력
    public void Display()
    {
        var current = Head;
        while (current != null)
        {
            // current.Data 출력
            Debug.Log(current.Data.ToString());
            // 다음 데이터로 이동
            current = current.Next;
        }
    }

}