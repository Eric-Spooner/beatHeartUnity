using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCollission : MonoBehaviour
{

    private static Image controlKross;
    public static Text hitValue;
    public static Text goodValue;
    public static Text perfectValue;
    private Canvas canvas;

    public static int hits;
    public static int good;
    public static int perfect;
    public static float controlPosYTop;


    // Use this for initialization
    void Start()
    {
        controlKross = GameObject.Find("controlKross").GetComponent<Image>();
        hitValue = GameObject.Find("HitValue").GetComponent<Text>();
        goodValue = GameObject.Find("GoodValue").GetComponent<Text>();
        perfectValue = GameObject.Find("PerfectValue").GetComponent<Text>();

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        hits = 0;
        good = 0;
        perfect = 0;

        controlPosYTop = controlKross.gameObject.transform.localPosition.y + ((RectTransform)controlKross.gameObject.transform).rect.height / 2.0f;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (controlKross.fillAmount > 0)
        {
            if (Mathf.Abs(controlKross.transform.position.y - collider.gameObject.transform.position.y) <= 5)
            {
                var perfectAnimVar = Instantiate((GameObject)Resources.Load("Perfect"));
                perfectAnimVar.transform.SetParent(canvas.transform);
                perfect++;
                perfectValue.text = perfect.ToString();
            }
            else if (Mathf.Abs(controlKross.transform.position.y - collider.gameObject.transform.position.y) <= 8)
            {
                var goodAnimVar = Instantiate((GameObject)Resources.Load("Good"));
                goodAnimVar.transform.SetParent(canvas.transform);
                good++;
                goodValue.text = good.ToString();
            }
            else
            {
                var hitAnimVar = Instantiate((GameObject)Resources.Load("Hit"));
                hitAnimVar.transform.SetParent(canvas.transform);
                hits++;
                hitValue.text = hits.ToString();
            }
            Destroy(collider.gameObject);
        }
    }
    
}