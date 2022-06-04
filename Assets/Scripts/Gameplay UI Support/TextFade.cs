using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeOut");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2F);
        for (float f = 1f; f > -0.04f; f -= 0.04f)
        {
            Color c = gameObject.GetComponent<Text>().color;
            c.a = f;
            gameObject.GetComponent<Text>().color = c;
            yield return new WaitForSeconds(0.05F);

        }
    }
}
