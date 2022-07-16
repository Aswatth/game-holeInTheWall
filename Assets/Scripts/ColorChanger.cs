using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [Range(0f, 1f)]public float lerpTime;
    public Color[] colors;

    [HideInInspector]
    public Color playerColor;
    private int id;
    private int length;
    float t = 0f;

    private void Start()
    {
        id = 0;
        length = colors.Length;
    }
    private void Update()
    {
        playerColor = Color.Lerp(playerColor,colors[id],lerpTime*Time.deltaTime);
        t = Mathf.Lerp(t,1f,lerpTime*Time.deltaTime);
        if (t > 0.9f) {
            t = 0f;
            id++;
            id = id >= length ? 0 : id;
        }
    }
}
