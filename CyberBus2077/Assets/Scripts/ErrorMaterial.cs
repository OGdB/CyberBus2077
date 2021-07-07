using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ErrorMaterial : MonoBehaviour
{
    public Material mat;
    public Color originalColor;
    public Color errorColor;
    public float timer;
    public float lerpValue;
    public Image img;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            mat.SetColor("_EmissionColor", Color.Lerp(mat.GetColor("_EmissionColor"), originalColor, lerpValue));
            img.color = Color.Lerp(img.color, Color.white, lerpValue);
        }
    }

    public void Error ()
    {
        mat.SetColor("_EmissionColor", errorColor);
        img.color = errorColor;
        timer = 2f;
    }
}
