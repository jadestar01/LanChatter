using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI nickName;
    public TextMeshProUGUI text;

    [Button]
    public void Init(string nickName, string str)
    {
        this.nickName.text = "";
        this.text.text = "";

        this.nickName.text = nickName;
        text.DOText(str, str.Length * 0.015f)
            .OnUpdate(() =>
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)text.transform);
            });
    }
}
