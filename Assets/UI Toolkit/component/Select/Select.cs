using Sky9th.UIT;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class Select : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Select, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription placeholder = new() { name = "placeholder", defaultValue = "" };
        UxmlEnumAttributeDescription<TypeEnum> type = new() { name = "Type" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Select;

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
    private VisualElement select;
    private VisualElement container;
    private VisualElement input;
    private Label placeholderLabel;
    private Label textLabel;
    private VisualElement icon;
    private VisualElement iconImg;

    private VisualElement menu;

    private HashSet<string> valueList = new();
    private string valueStr = "";

    public Select()
    {
        uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI Toolkit/component/Select/Select.uxml");
        uxml.CloneTree(this);

        select = UIToolkitUtils.FindChildElement(this, "Select");

        container = UIToolkitUtils.FindChildElement(this, "Container");
        input = UIToolkitUtils.FindChildElement(this, "Input");
        placeholderLabel = UIToolkitUtils.FindChildElement(this, "Placeholder") as Label;
        textLabel = UIToolkitUtils.FindChildElement(this, "TextLabel") as Label;
        icon = UIToolkitUtils.FindChildElement(this, "Icon");
        iconImg = UIToolkitUtils.FindChildElement(this, "IconImg");
        /*Sprite dSprite = Resources.Load<Sprite>("Images/dropdown");
        StyleBackground backgroundImage = new StyleBackground(dSprite);
        iconImg.style.backgroundImage = backgroundImage;*/

        menu = UIToolkitUtils.FindChildElement(this, "Menu");

        menu.Add(CreateMenuItem("test1"));
        menu.Add(CreateMenuItem("test2"));
        menu.Add(CreateMenuItem("test3"));

        input.RegisterCallback<ClickEvent>(OnClick);

    }

    private void OnClick(ClickEvent evt)
    {
        menu.style.display = DisplayStyle.Flex;
    }

    public void update()
    {
        placeholderLabel.text = placeholder;
        textLabel.text = valueStr;
        select.AddToClassList(type.ToString().ToLower());
        if (valueList.Count > 0)
        {
            placeholderLabel.style.display = DisplayStyle.None;
        }
        else
        {
            placeholderLabel.style.display = DisplayStyle.Flex;
        }
    }

    private VisualElement CreateMenuItem(string name)
    {
        VisualElement menuItemContainer = new();
        menuItemContainer.name = "MenuItemContainer";

        VisualElement menuItem = new();
        menuItem.name = "MenuItem";

        Label menuItemLabel = new();
        menuItemLabel.text = name;
        menuItemLabel.name = "MenuItemLabel";

        menuItem.Add(menuItemLabel);
        menuItemContainer.Add(menuItem);

        menuItemContainer.RegisterCallback<ClickEvent>(OnSelectItem);

        return menuItemContainer;
    }

    private void OnSelectItem(ClickEvent evt)
    {
        Label menuItemLabel = UIToolkitUtils.FindChildElement(evt.target as VisualElement, "MenuItemLabel") as Label;
        string value = menuItemLabel.text;
        if (valueList.Contains(value)) {
            valueList.Remove(value);
        } else
        {
            valueList.Add(value);
        }
        valueStr = string.Join(",", valueList);
        update();
        menu.style.display = DisplayStyle.None;
    }
}