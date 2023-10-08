using Sky9th.UIT;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Switch : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Switch, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription label = new() { name = "Label", defaultValue = "" };
        UxmlEnumAttributeDescription<TypeEnum> type = new() { name = "Type" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Switch;

            ate.label = label.GetValueFromBag(bag, cc);
            ate.type = type.GetValueFromBag(bag, cc);
            ate.Init();
        }

    }

    [SerializeField]
    private string label { get; set; }
    [SerializeField]
    private TypeEnum type { get; set; }


    private VisualTreeAsset uxml;
    private VisualElement _switch;
    private Label textLabel;

    public Switch()
    {
        uxml = Resources.Load<VisualTreeAsset>("Uxml/Switch");
        uxml.CloneTree(this);

        _switch = UIToolkitUtils.FindChildElement(this, "Switch");
        textLabel = UIToolkitUtils.FindChildElement(this, "Label") as Label;

        _switch.RegisterCallback<ClickEvent>(OnClick);
    }

    private void OnClick(ClickEvent evt)
    {
        Debug.Log(evt.currentTarget);
        if (_switch.ClassListContains("checked"))
        {
            _switch.RemoveFromClassList("checked");
        } else
        {
            _switch.AddToClassList("checked");
        }
    }

    public void Init()
    {
        textLabel.text = label;
        AddToClassList(type.ToString());
    }
}