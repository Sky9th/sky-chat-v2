using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Modal : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Modal, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription title = new() { name = "Title", defaultValue = "Panel" };
        UxmlFloatAttributeDescription width = new() { name = "Width", defaultValue = 300f };
        UxmlFloatAttributeDescription height = new() { name = "Height", defaultValue = 300f };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Modal;

            ate.title = title.GetValueFromBag(bag, cc);
            ate.width = width.GetValueFromBag(bag, cc);
            ate.height = height.GetValueFromBag(bag, cc);

            ate.Init();
        }
    }

    private VisualTreeAsset panelUxml;
    private string title { get; set; }
    private float width { get; set; }
    private float height { get; set; }

    private VisualElement panelContainer;
    private Label titleLabel;
    private VisualElement header;
    private VisualElement logo;
    private VisualElement closeImg;

    public Modal()
    {
        panelUxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI Toolkit/component/Modal/Modal.uxml");

        panelUxml.CloneTree(this);

        panelContainer = this.Q("panel");
        if (panelContainer != null)
        {
            header = panelContainer.Q("panelHeader");
            logo = header.Q("panelLogo");
            titleLabel = header.Q<Label>("panelTitle");

            closeImg = header.Q("panelClose");

            VisualElement footer = panelContainer.Q("panelFooter");
        }
    }

    public void Init ()
    {
        if (panelContainer != null)
        {
            titleLabel.text = title;
            panelContainer.style.width = width;
            panelContainer.style.height = height;

            header.style.height = width / 10;

            logo.style.width = width / 10;
            logo.style.height = width / 10;

            closeImg.style.width = width / 10;
            closeImg.style.height = width / 10;
        }
    }
}
