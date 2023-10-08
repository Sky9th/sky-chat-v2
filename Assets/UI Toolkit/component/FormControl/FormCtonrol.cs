using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FormControl : VisualElement
{
    public new class UxmlFactory : UxmlFactory<FormControl, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as FormControl;

            ate.Init();
        }
    }

    private VisualTreeAsset uxml;

    public FormControl()
    {
        uxml = Resources.Load<VisualTreeAsset>("Uxml/FormControl");
        uxml.CloneTree(this);
    }

    public void Init ()
    {
    }
}
