using System;
using System.Collections.Generic;

// DoublyLinkedListNode 클래스는 데이터와 이증 연결 리스트의 이전 및 다음 노드에 대한 참조를 관리
public class DoublyLinkedListNode<T>
{
    public T Data { get; }  // 노드가 보유한 데이터
    public DoublyLinkedListNode<T>? Next { get; set; }  // 다음 노드에 대한 참조
    public DoublyLinkedListNode<T>? Previous { get; set; }  // 이전 노드에 대한 참조

    public DoublyLinkedListNode(T data) => Data = data; // 생성자 - 데이터를 설정
}

// 이중 연결 리스트 구현, 노드 추가 , 제거, 검색 등의 작업을 제공
public class DoublyLinkedList<T>
{
    public int Count { get; private set; }  // 리스트의 노드 수
    private DoublyLinkedListNode<T>? Head { get; set; } // 리스트의 첫 번째 노드
    private DoublyLinkedListNode<T>? Tail { get; set; } // 리스트의 마지막 노드

    // 데이터 하나로 리스트를 초기화하는 생성자
    public DoublyLinkedList(T data)
    {
        Head = new DoublyLinkedListNode<T>(data);
        Tail = Head;
        Count = 1;
    }

    // IEnumerable<T>를 사용하여 여러 데이터를 초기화하는 생성자
    public DoublyLinkedList(IEnumerable<T> data)
    {
        foreach (var d in data)
        {
            Add(d);
        }
    }

    // 리스트의 헤드에 노드를 추가
    public DoublyLinkedListNode<T> AddHead(T data)
    {
        var node = new DoublyLinkedListNode<T>(data);

        // 리스트가 비어 있을 경우
        if (Head is null)
        {
            Head = node;
            Tail = node;
            Count = 1;
            return node;
        }

        // 새로운 노드를 헤드로 설정
        Head.Previous = node;
        node.Next = Head;
        Head = node;
        Count++;
        return node;
    }

    // 리스트의 마지막에 노드를 추가
    public DoublyLinkedListNode<T> Add(T data)
    {
        if (Head is null)
        {
            return AddHead(data);   // 비어 있을 경우 AddHead() 호출
        }

        var node = new DoublyLinkedListNode<T>(data);
        Tail!.Next = node;
        node.Previous = Tail;
        Tail = node;
        Count++;
        return node;
    }

    // 기존 노드 뒤에 새로운 노드를 추가
    public DoublyLinkedListNode<T> AddAfter(T data, DoublyLinkedListNode<T> existingNode)
    {
        // 기존 노드가 마지막 노드라면 Add() 호출
        if (existingNode == Tail)
        {
            return Add(data);
        }

        var node = new DoublyLinkedListNode<T>(data);
        node.Next = existingNode.Next;
        node.Previous = existingNode;
        existingNode.Next = node;

        if (node.Next is not null)
        {
            node.Next.Previous = node;
        }

        Count++;
        return node;
    }

    // 리스트의 데이터를 순차적으로 반환하는 열거자
    public IEnumerable<T> GetData()
    {
        var current = Head;

        while (current is not null)
        {
            yield return current.Data;  // 현재 노드의 데이터를 반환
            current = current.Next; // 다음 노드로 이동
        }
    }

    // 리스트의 데이터를 역순으로 반환하는 열거자
    public IEnumerable<T> GetDataReversed()
    {
        var current = Tail;
        while (current is not null)
        {
            yield return current.Data;  // 현재 노드의 데이터를 반환
            current = current.Previous; // 이전 노드로 이동
        }
    }

    // 리스트를 반대로 뒤집음
    public void Reverse()
    {
        var current = Head;
        DoublyLinkedListNode<T>? temp = null;

        while (current is not null)
        {
            temp = current.Previous;
            current.Previous = current.Next;
            current.Next = temp;
            current = current.Previous;
        }

        Tail = Head;

        if (temp is not null)
        {
            Head = temp.Previous;
        }
    }

    // 특정 데이터를 가진 노드를 검색
    public DoublyLinkedListNode<T> Find(T data)
    {
        var current = Head;

        while (current is not null)
        {
            if (current.Data is null && data is null || current.Data is not null && current.Data.Equals(data))
            {
                return current;
            }
            current = current.Next;
        }

        throw new ItemNotFoundException();  // 데이터를 찾을 수 없을 경우 예외 발생
    }

    // 지정된 위치의 노드를 검색
    public DoublyLinkedListNode<T> GetAt(int position)
    {
        if (position < 0 || position >= Count)
        {
            throw new ArgumentOutOfRangeException($"Max count is {Count}");
        }

        var current = Head;

        for (var i = 0; i < position; i++)
        {
            current = current!.Next;
        }

        return current ?? throw new ArgumentOutOfRangeException($"{nameof(position)} must be an index in the list");
    }

    // 헤드 노드를 제거
    public void RemoveHead()
    {
        if (Head is null)
        {
            throw new InvalidOperationException();
        }

        Head = Head.Next;

        // 리스트가 비게 되었을 경우
        if (Head is null)
        {
            Tail = null;
            Count = 0;
            return;
        }

        Head.Previous = null;
        Count--;
    }

    // 마지막 노드를 제거
    public void Remove()
    {
        if (Tail is null)
        {
            throw new InvalidOperationException("Cannot prune empty list");
        }

        Tail = Tail.Previous;

        // 리스트가 비게 되었을 경우
        if (Tail is null)
        {
            Head = null;
            Count = 0;
            return;
        }

        Tail.Next = null;
        Count--;
    }

    // 특정 노드를 제거
    public void RemoveNode(DoublyLinkedListNode<T> node)
    {
        if (node == Head)
        {
            RemoveHead();
            return;
        }

        if (node == Tail)
        {
            Remove();
            return;
        }

        if (node.Previous is null || node.Next is null)
        {
            throw new ArgumentException(
                $"{nameof(node)} cannot have Previous or Next null if it's an internal node");
        }

        node.Previous.Next = node.Next;
        node.Next.Previous = node.Previous;
        Count--;
    }

    // 특정 데이터를 가진 노드를 제거
    public void Remove(T data)
    {
        var node = Find(data);
        RemoveNode(node);
    }

    // 특정 데이터의 인덱스를 반환
    public int IndexOf(T data)
    {
        var current = Head;
        var index = 0;

        while (current is not null)
        {
            if (current.Data is null && data is null || current.Data is not null && current.Data.Equals(data))
            {
                return index;
            }

            index++;
            current = current.Next;
        }

        return -1;  // 데이터를 찾지 못한 경우 -1 반환
    }

    // 특정 데이터가 리스트에 존재하는 확인
    public bool Contains(T data) => IndexOf(data) != -1;
}
