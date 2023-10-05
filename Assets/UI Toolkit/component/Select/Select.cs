using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Select : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Select, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription placeholder = new() { name = "placeholder", defaultValue = "" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Select;

            ate.placeholder = placeholder.GetValueFromBag(bag, cc);
            ate.update();
        }

    }

    private string placeholder;
    private VisualElement container;
    private Label placeholderLabel;
    private Label textLabel;
    private VisualElement c;
    private VisualElement iImg;

    private VisualElement menu;

    public Select()
    {
        container = new VisualElement();
        container.name = "Container";
        Add(container);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/UI Toolkit/component/Select/Select.uss");
        container.styleSheets.Add(styleSheet);
        container.name = "Container";

        placeholderLabel = new Label();
        placeholderLabel.name = "Placeholder";
        container.Add(placeholderLabel);

        textLabel = new Label();
        textLabel.name = "TextLabel";
        container.Add(textLabel);

        c = container.Q<VisualElement>();
        VisualElement icon = new();
        icon.name = "Icon";
        c.Add(icon);

        iImg = new VisualElement();
        iImg.name = "IconImg";
        Sprite dSprite = Resources.Load<Sprite>("Images/dropdown");
        StyleBackground backgroundImage = new StyleBackground(dSprite);
        iImg.style.backgroundImage = backgroundImage;
        icon.Add(iImg);

        menu = new VisualElement();
        menu.name = "Menu";
        container.Add(menu);

        menu.Add(CreateMenuItem());
        menu.Add(CreateMenuItem());
        menu.Add(CreateMenuItem());

        container.RegisterCallback<ClickEvent>(OnClick);

    }

    private void OnClick(ClickEvent evt)
    {
        Debug.Log(evt);
    }

    public void update()
    {
        placeholderLabel.text = placeholder;
    }

    private void OnDropdownValueChanged(ChangeEvent<string> eventValue)
    {
        Debug.Log("Selected Value: " + eventValue.newValue);
    }

    private VisualElement CreateMenuItem()
    {
        VisualElement menuItemContainer = new();
        menuItemContainer.name = "MenuItemContainer";

        VisualElement menuItem = new();
        menuItem.name = "MenuItem";

        Label menuItemLabel = new();
        menuItemLabel.text = "test";
        menuItemLabel.name = "MenuItemLabel";

        menuItem.Add(menuItemLabel);
        menuItemContainer.Add(menuItem);

        return menuItemContainer;
    }



}
