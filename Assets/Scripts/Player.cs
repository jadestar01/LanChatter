using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int index = -1;
    public TextBubble bubble;

    [Button]
    public void Texting(string str)
    {
        bubble.Talk(str);
    }
}
