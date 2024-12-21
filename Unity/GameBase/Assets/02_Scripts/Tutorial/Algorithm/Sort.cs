using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sort : MonoBehaviour
{

    /// <summary>
    /// 버블 정렬
    /// </summary>
    /// <param name="arr"></param>
    public void BubbleSortArray(int[] arr)
    {
        int n = arr.Length;

        for (int i = 0; i < n - 1; i++)
        {
            // n-1번 순회하며 현재 원소가 바로 뒤의 원소보다 크면 자리를 바꿈.
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }


    /// <summary>
    /// 선택 정렬
    /// </summary>
    /// <param name="arr"></param>
    public void SelectionSortArray(int[] arr)
    {
        int n = arr.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                int temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }
        }
    }


    /// <summary>
    /// 삽입 정렬
    /// </summary>
    /// <param name="arr"></param>
    public void InsertionSortArray(int[] arr)
    {
        int n = arr.Length;

        for (int i = 1; i < n; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            arr[j + 1] = key;
        }
    }

    /// <summary>
    /// 쿽 정렬 0, arrayToSort.Length - 1이 각각 low, high로 최초 입력
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="low"></param>
    /// <param name="high"></param>
    public void QuickSortArray(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int partitionIndex = Partition(arr, low, high);

            QuickSortArray(arr, low, partitionIndex - 1);
            QuickSortArray(arr, partitionIndex + 1, high);
        }
    }

    public int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;

                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        int temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;
        return i + 1;
    }


    /// <summary>
    /// 병합 정렬 0, arrayToSort.Length - 1이 각각 low, high로 최초 입력
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public void MergeSortArray(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;

            MergeSortArray(arr, left, mid);
            MergeSortArray(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }

    public void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;
        int[] leftArray = new int[n1];
        int[] rightArray = new int[n2];

        int i;
        for (i = 0; i < n1; i++)
        {
            leftArray[i] = arr[left + i];
        }

        for (i = 0; i < n2; i++)
        {
            rightArray[i] = arr[mid + 1 + i];
        }

        int k = left;
        int j = 0;
        i = 0;

        while (i < n1 && j < n2)
        {
            if (leftArray[i] <= rightArray[j])
            {
                arr[k] = leftArray[i];
                i++;
            }
            else
            {
                arr[k] = rightArray[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            arr[k] = leftArray[i];
            i++;
            k++;
        }

        while (j < 2)
        {
            arr[k] = rightArray[j];
            j++;
            k++;
        }
    }


    public void HeapSortArray(int[] arr)
    {
        int n = arr.Length;

        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Heapify(arr, n, i);
        }

        for (int i = n - 1; i >= 0; i--)
        {
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
            Heapify(arr, i, 0);
        }
    }

    public void Heapify(int[] arr, int n, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && arr[left] > arr[largest])
        {
            largest = left;
        }

        if (right < n && arr[right] > arr[largest])
        {
            largest = right;
        }

        if (largest != i)
        {
            int temp = arr[i];
            arr[i] = arr[largest];
            arr[largest] = temp;
            Heapify(arr, n, largest);
        }
    }

}
