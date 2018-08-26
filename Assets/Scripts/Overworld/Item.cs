using UnityEngine;

public struct ItemType
{
    public const int DOUGH = 0;
    public const int ONION = 1;
    public const int PORK = 2;
    public const int BUN = 3;
}

public class Item : ScriptableObject {
    public int type;
}
