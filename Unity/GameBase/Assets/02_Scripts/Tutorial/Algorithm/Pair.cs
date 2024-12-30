using UnityEngine;

public class Pairrrrrr<T1, T2>
{
    public T1 First { get; set; }
    public T2 Second { get; set; }

    // public Pairrrrrr(T1 first, T2 second)
    // {
    //     First = first;
    //     Second = second;
    // }

    public void Swap()
    {
        T1 temp = First;
        First = (T1)(object)Second;
        Second = (T2)(object)temp;
    }

    public void Print()
    {
        Debug.Log($"First: {First}, Second: {Second}");
    }
}