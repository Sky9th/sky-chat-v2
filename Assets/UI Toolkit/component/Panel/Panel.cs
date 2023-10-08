using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class Panel : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Panel, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlFloatAttributeDescription width = new() { name = "Width", defaultValue = 300f };
        UxmlFloatAttributeDescription height = new() { name = "Height", defaultValue = 300f };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Panel;

            ate.width = width.GetValueFromBag(bag, cc);
            ate.height = height.GetValueFromBag(bag, cc);

            ate.Init();
        }
    }

    private VisualTreeAsset panelUxml;
    private float width { get; set; }
    private float height { get; set; }

    private VisualElement panelContainer;

    public Panel()
    {
        panelUxml = Resources.Load<VisualTreeAsset>("Uxml/Panel");
        panelUxml.CloneTree(this);
    }

    public void Init ()
    {
        style.width = width;
        style.height = height;

    }

}
