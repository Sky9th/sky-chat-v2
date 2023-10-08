using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Form : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Form, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Form;

            ate.Init();
        }
    }

    private VisualTreeAsset uxml;

    public Form()
    {
        uxml = Resources.Load<VisualTreeAsset>("Uxml/Form");
        uxml.CloneTree(this);
    }

    public void Init ()
    {
    }
}
