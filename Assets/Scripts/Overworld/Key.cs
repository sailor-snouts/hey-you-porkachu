using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct KeyType
{
    public const int UNLOCKED = 0;
    public const int RESTAURANT = 1;
    public const int KITCHEN = 2;
    public const int STORAGE = 3;
    public const int YARD = 4;
}

public class Key : ScriptableObject {
    public int type;
}
