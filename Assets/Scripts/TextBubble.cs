using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Sirenix.OdinInspector;

public class TextBubble : MonoBehaviour
{
    public TextMeshPro text;
    bool isTalk = false;
    Tweener twn;

    [Button]
    public void Talk(string str)
    {
        text.text = "";
        Sequence seq = DOTween.Sequence()
        .Append(twn = text.DOText(str, str.Length * 0.015f).SetEase(Ease.Linear))
        .AppendInterval(1.0f)
        .AppendCallback(() => text.text = "");
    }
}
