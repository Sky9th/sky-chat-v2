using UnityEngine;
using UnityEngine.UIElements;

public class Component : VisualElement
{

    private readonly string componentPath = "Uxml/";
    private VisualTreeAsset uxml;


    public Component () {
        string className = GetType().ToString();
        uxml = Resources.Load<VisualTreeAsset>(componentPath + className);
        if (uxml)
        {
            uxml.CloneTree(this);
        }
        init();
    }

    internal void init ()
    {
    }

}
