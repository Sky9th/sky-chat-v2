using Sky9th.UIT;
using System;
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
        UxmlStringAttributeDescription choice = new() { name = "Choice", defaultValue = "" };
        UxmlBoolAttributeDescription multiple = new() { name = "Multiple", defaultValue = false };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Select;

            ate.placeholder = placeholder.GetValueFromBag(bag, cc);
            ate.choice = choice.GetValueFromBag(bag, cc);
            ate.multiple = multiple.GetValueFromBag(bag, cc);
            ate.type = type.GetValueFromBag(bag, cc);
            ate.Init();
        }

    }

    [SerializeField]
    private string placeholder { get; set; }

    [SerializeField]
    private bool multiple { get; set; }
    [SerializeField]
    private string choice { get; set; }
    [SerializeField]
    private TypeEnum type { get; set; }

    private VisualElement root;
    private VisualTreeAsset uxml;
    private VisualTreeAsset menuUxml;
    private VisualElement select;
    private VisualElement container;
    private VisualElement input;
    private Label placeholderLabel;
    private Label textLabel;
    private VisualElement icon;
    private VisualElement iconImg;
    private VisualElement menu;
    private VisualElement backdrop;
    private bool menuDisplay = false;

    private HashSet<string> valueList = new();
    private string valueStr = "";
    private string[] choiceList;

    public Action OnValueChange;

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

        menuUxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI Toolkit/component/Select/SelectMenu.uxml");
        menu = menuUxml.Instantiate();
        menu.name = "SelectMenu";
        UIToolkitUtils.ClearChildrenElements(menu);

        input.RegisterCallback<ClickEvent>(OnClick);
        backdrop = UIToolkitUtils.CreateBackDrop(this, OnClickBackDrop);

    }

    private void OnClickBackDrop(ClickEvent evt)
    {
        HideMenu();
    }

    private void OnClick(ClickEvent evt)
    {
        if (menuDisplay)
        {
            HideMenu();
        } else
        {
            ShowMenu();
        }
    }

    public void ShowMenu()
    {
        root = GameObject.FindFirstObjectByType<UIDocument>().rootVisualElement;
        root.Add(menu);
        menu.style.position = Position.Absolute;
        menu.style.left = select.worldBound.position.x;
        menu.style.top = select.worldBound.position.y + select.worldBound.height;
        menu.style.display = DisplayStyle.Flex;
        backdrop.style.display = DisplayStyle.Flex;
        menuDisplay = true;
    }

    public void HideMenu()
    {
        menu.style.display = DisplayStyle.None;
        backdrop.style.display = DisplayStyle.None;
        menuDisplay = false;
    }

    public void Init()
    {
        Debug.Log(root);
        choiceList = choice.Split(",");
        for (int i = 0; i < choiceList.Length; i++)
        {
            menu.Add(CreateMenuItem(choiceList[i]));
        }
        select.AddToClassList(type.ToString().ToLower());
        placeholderLabel.text = placeholder;
    }

    public void Update()
    {
        textLabel.text = valueStr;
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

        menuItemContainer.RegisterCallback<ClickEvent>(OnSelectItem, TrickleDown.TrickleDown);

        return menuItemContainer;
    }

    private void OnSelectItem(ClickEvent evt)
    {
        Label menuItemLabel = UIToolkitUtils.FindChildElement(evt.target as VisualElement, "MenuItemLabel") as Label;
        string value = menuItemLabel.text;
        if (multiple)
        {
            if (valueList.Contains(value))
            {
                valueList.Remove(value);
            }
            else
            {
                valueList.Add(value);
            }
            if (OnValueChange != null)
            {
                OnValueChange.Invoke();
            }
        } else
        {
            if (!valueList.Contains(value))
            {
                valueList = new()
                {
                    value
                };
                if (OnValueChange != null)
                {
                    OnValueChange.Invoke();
                }
            }
        }
        valueStr = string.Join(",", valueList);
        Update();
        HideMenu();
    }
}