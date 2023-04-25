using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class ColorRandomizer : NetworkBehaviour
{

    private MeshRenderer meshRen;
    public void Start()
    {
        Initialize();
        if (Object.HasInputAuthority)
        {
            ChangeColor(Random.ColorHSV());
        }
    }
    public void Initialize()
    {
        meshRen = GetComponent<MeshRenderer>();
        
        
    }
    void ChangeColor(Color newColor)
    {
        newColor = Random.ColorHSV();
        meshRen.material.color = newColor;
    }
    static void FindColorChanger(Transform currentTransform, ref List<ColorRandomizer> colorChangers)
    {
        ColorRandomizer colorChanger = currentTransform.GetComponent<ColorRandomizer>();
        if(colorChanger != null)
        {
            colorChangers.Add(colorChanger);
            colorChanger.Initialize();
        }
    }
    static void ChangeColor(Color color,List<ColorRandomizer> colorChangers)
    {
        foreach (ColorRandomizer colorChanger in colorChangers)
        {
            colorChanger.ChangeColor(color);
        }
    }
}
