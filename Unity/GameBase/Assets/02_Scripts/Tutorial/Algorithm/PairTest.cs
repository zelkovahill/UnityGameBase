using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PairTest : MonoBehaviour
{
    Pairrrrrr<string, string> name;

    private void Start()
    {
        name = new Pairrrrrr<string, string>();
        name.First = "박사랑은";
        name.Second = "조용히해라";
        name.Print();
    }
}
