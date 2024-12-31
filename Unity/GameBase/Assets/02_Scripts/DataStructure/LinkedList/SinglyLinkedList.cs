using System.Collections.Generic;
using System;

// 단일 연결 리스트의 노드 정의
public class SinglyLinkedListNode<T>
{
    public T Data;  // 노드에 저장된 데이터
    public SinglyLinkedListNode<T>? Next { get; set; }  // 다음 노드를 가리키는 포인터

    // 생성자 - 데이터를 받아서 노드를 생성
    public SinglyLinkedListNode(T data)
    {
        Data = data;
        Next = null;
    }
}

// 단일 연결 리스트 정의
public class SinglyLinkedList<T>
{
    // 리스트의 첫 번째 노드를 가리키는 포인터
    private SinglyLinkedListNode<T>? Head { get; set; }

    // 리스트의 앞에 새로운 노드를 추가하는 메서드
    public SinglyLinkedListNode<T> AddFirst(T data)
    {
        var newListElement = new SinglyLinkedListNode<T>(data)
        {
            Next = Head,    // 새 노드의 Next는 기존의 첫 번째 노드를 가리킴
        };

        Head = newListElement;  // 새 노드를 Head로 설정
        return newListElement;
    }

    // 리스트의 끝에 새로운 노드를 추가하는 메서드
    public SinglyLinkedListNode<T> AddLast(T data)
    {
        var newListElement = new SinglyLinkedListNode<T>(data);

        // 리스트가 비어있다면 Head에 새 노드를 할당
        if (Head is null)
        {
            Head = newListElement;
            return newListElement;
        }

        // 리스트의 마지막 노드를 찾기 위해 반복
        var tempElement = Head;

        while (tempElement.Next is not null)
        {
            tempElement = tempElement.Next;
        }

        tempElement.Next = newListElement;  // 마지막 노드의 Next를 새 노드로 설정
        return newListElement;
    }

    // 특정 인덱스의 데이터를 반환하는 메서드
    public T GetElementByIndex(int index)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be greater than or equal to 0");
        }

        var tempElement = Head;

        // 인덱스까지 이동하며 노드를 찾는다.
        for (int i = 0; tempElement is not null && i < index; i++)
        {
            tempElement = tempElement.Next;
        }

        // 범위를 벗어난 인덱스일 경우 예외 처리
        if (tempElement is null)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");
        }

        return tempElement.Data;    // 해당 노드의 데이터를 반환
    }

    // 리스트의 길이를 구하는 메서드
    public int Length()
    {
        if (Head is null)
        {
            return 0;
        }

        var tempElement = Head;
        var Length = 1;

        // 리스트 끝까지 순회하며 길이를 셈
        while (tempElement.Next is not null)
        {
            tempElement = tempElement.Next;
            Length++;
        }

        return Length;
    }

    // 리스트의 데이터를 순차적으로 반환하는 IEnumerator
    public IEnumerable<T> GetListData()
    {
        var tempElement = Head;

        while (tempElement is not null)
        {
            yield return tempElement.Data;  // 현재 노드의 데이터를 반환
            tempElement = tempElement.Next; // 다음 노드로 이동
        }
    }

    // 특정 데이터를 리스트에서 삭제하는 메서드
    public bool DeleteElement(T element)
    {
        var currentElement = Head;
        SinglyLinkedListNode<T>? previousElement = null;

        // 리스트를 순회하며 삭제할 노드를 찾음
        while (currentElement is not null)
        {
            // 삭제할 노드가 일치하는지 확인 (null 처리 포함)
            if (currentElement.Data is null && element is null ||
            currentElement.Data is not null && currentElement.Data.Equals(element))
            {
                // 삭제할 노드가 Head일 경우
                if (currentElement.Equals(Head))
                {
                    Head = Head.Next;   // Head를 다음 노드로 설정
                    return true;
                }

                if (previousElement is not null)
                {
                    // 이전 노드의 Next를 삭제할 노드의 다음 노드로 설정
                    previousElement.Next = currentElement.Next;
                    return true;
                }
            }

            previousElement = currentElement;
            currentElement = currentElement.Next;   // 다음 노드로 이동
        }

        return false;
    }

    // 리스트의 마지막 노드를 삭제하는 메서드
    public bool DeleteLast()
    {
        if (Head is null)
        {
            return false;   // 리스트가 비어있다면 삭제할 수 없음
        }

        if (Head.Next is null)
        {
            Head = null;    // 리스트에 하나만 있는 경우, Head를 null로 설정
            return true;
        }

        SinglyLinkedListNode<T>? secondlast = Head;

        // 마지막에서 두 번째 노드를 찾기 위해 반복F
        while (secondlast.Next?.Next is not null)
        {
            secondlast = secondlast.Next;
        }

        secondlast.Next = null; // 마지막 노드의 Next를 null로 설정하여 설정
        return true;
    }
}
