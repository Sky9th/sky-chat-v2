using Sky9th.UIT;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class Radio : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Radio, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription placeholder = new() { name = "placeholder", defaultValue = "" };
        UxmlEnumAttributeDescription<TypeEnum> type = new() { name = "Type" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Radio;

            ate.placeholder = placeholder.GetValueFromBag(bag, cc);
            ate.type = type.GetValueFromBag(bag, cc);
            ate.update();
        }

    }

    [SerializeField]
    private string placeholder { get; set; }
    [SerializeField]
    private TypeEnum type { get; set; }


    private VisualTreeAsset uxml;

    public Radio()
    {
        uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI Toolkit/component/Radio/Radio.uxml");
        uxml.CloneTree(this);

    }

    public void update()
    {
    }
}