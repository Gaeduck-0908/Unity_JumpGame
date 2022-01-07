using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Size : MonoBehaviour
{
    private Vector2 ObjectSize;
    BoxCollider2D BoxC;
    RectTransform Target;
    private void Start()
    {
        BoxC = GetComponent<BoxCollider2D>();
        Target = GetComponent<RectTransform>();
    }

    private void Update()
    {
        BoxC.size = Target.sizeDelta;
    }
}
