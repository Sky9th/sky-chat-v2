using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TextInputField : TextField
{
    public new class UxmlFactory : UxmlFactory<TextInputField, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : TextField.UxmlTraits
    {
        UxmlStringAttributeDescription placeholder = new() { name = "placeholder", defaultValue = "" };
        UxmlEnumAttributeDescription<IconEnum> icon = new() { name = "Icon" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as TextInputField;

            ate.placeholder = placeholder.GetValueFromBag(bag, cc);
            ate.UpdatePlaceholder();

            ate.icon = icon.GetValueFromBag(bag, cc);
            ate.UpdateIcon();
        }
    }

    private void UpdatePlaceholder()
    {
        if (placeholder == null)
        {
            placeholderLabel.style.display = DisplayStyle.None;
        }
        else
        {
            placeholderLabel.style.display = DisplayStyle.Flex;
        }
    }

    // Must expose your element class to a { get; set; } property that has the same name 
    // as the name you set in your UXML attribute description with the camel case format
    public string placeholder
    {
        get { return placeholderLabel.text; }
        set { placeholderLabel.text = value; }
    }
    public IconEnum icon { get; set; }

    private VisualElement iconContainer;


    public Texture2D BackgroundImage
    {
        get
        {
            var image = iconContainer.style.backgroundImage.value.texture;
            return image != null ? (Texture2D)image : null;
        }
        set { iconContainer.style.backgroundImage = value != null ? value : Texture2D.blackTexture; }
    }

    private Label placeholderLabel;

    public TextInputField ()
    {
        VisualElement container = this.Children().First();

        placeholderLabel = new Label();
        placeholderLabel.AddToClassList("placeholder");
        placeholderLabel.name = "placeholder";

        iconContainer = new VisualElement();
        iconContainer.AddToClassList("icon");
        iconContainer.style.display = DisplayStyle.None;
        iconContainer.name = "icon";

        RegisterCallback<FocusInEvent>(OnFocusIn);
        RegisterCallback<FocusOutEvent>(OnFocusOut);
        container.Insert(0, iconContainer);
        container.Insert(1, placeholderLabel);
    }

    private void UpdateIcon()
    {
        string iconName = icon.ToString();
        if (iconName == "NONE")
        {
            iconContainer.style.display = DisplayStyle.None;
        }
        else
        {
            Sprite closeSprite = Resources.Load<Sprite>("Images/" + iconName);
            StyleBackground backgroundImage = new StyleBackground(closeSprite);
            iconContainer.style.backgroundImage = backgroundImage;
            iconContainer.style.display = DisplayStyle.Flex;
        }
    }

    private void OnFocusIn(FocusInEvent evt)
    {
        if (string.IsNullOrEmpty(text))
        {
            placeholderLabel.style.display = DisplayStyle.None;
        }
    }

    private void OnFocusOut(FocusOutEvent evt)
    {
        if (string.IsNullOrEmpty(text))
        {
            placeholderLabel.style.display = DisplayStyle.Flex;
        }
    }

}
