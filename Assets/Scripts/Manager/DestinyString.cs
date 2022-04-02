using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinyString : MonoBehaviour
{
    [SerializeField] private Mask mask;
    [SerializeField] private Image badVisu;

    [SerializeField, Range(0,1)] float value = 0;

    private void Update()
    {
        mask.rectTransform.offsetMax = new Vector2(mask.rectTransform.offsetMax.x, Mathf.MoveTowards(1080, 0, value * 1080));
        mask.rectTransform.offsetMin = new Vector2(mask.rectTransform.offsetMin.x, -Mathf.MoveTowards(-1080, 0, value * 1080));

        badVisu.rectTransform.offsetMin = - mask.rectTransform.offsetMin;
        badVisu.rectTransform.offsetMax = - mask.rectTransform.offsetMax;
    }

    public void SetStringValue(float _value)
    {
        value = _value;
    }
}
