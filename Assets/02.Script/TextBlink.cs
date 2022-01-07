using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    Text BlinkText;

    private void Start()
    {
        BlinkText = GetComponent<Text>();
        StartCoroutine(Blink());
    }

    public IEnumerator Blink()
    {
        while (true)
        {
            BlinkText.text = "";
            yield return new WaitForSeconds(.5f);
            BlinkText.text = "SKIP : ESC";
            yield return new WaitForSeconds(.5f);
        }
    }
}
