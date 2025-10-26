using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public bool IsHoldingForkSpoon;

    private void Awake()
    {
        Instance = this;
    }
}
