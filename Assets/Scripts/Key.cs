using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct KeyType
{
    public const int UNLOCKED = 0;
    public const int KITCHEN = 1;
    public const int STORAGE = 2;
    public const int YARD = 3;
}

public class Key : MonoBehaviour {
    public int type;
}
