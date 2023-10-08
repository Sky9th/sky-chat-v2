using Sky9th.UIT;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckBox : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CheckBox, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription choice = new() { name = "Choice", defaultValue = "" };
        UxmlEnumAttributeDescription<TypeEnum> type = new() { name = "Type" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as CheckBox;

            ate.choice = choice.GetValueFromBag(bag, cc);
            ate.type = type.GetValueFromBag(bag, cc);
            ate.Init();
        }

    }

    [SerializeField]
    private string choice { get; set; }
    [SerializeField]
    private TypeEnum type { get; set; }


    private VisualTreeAsset uxml;
    private VisualTreeAsset itemUxml;
    private VisualElement checkBox;
    private VisualElement item;
    private VisualElement btn;
    private VisualElement round;
    private VisualElement point;
    private Label textLabel;

    private string[] choiceList;
    public CheckBox()
    {
        uxml = Resources.Load<VisualTreeAsset>("Uxml/CheckBox");
        uxml.CloneTree(this);

        itemUxml = Resources.Load<VisualTreeAsset>("Uxml/CheckBoxItem");
        item = itemUxml.Instantiate();

        checkBox = UIToolkitUtils.FindChildElement(this, "CheckBox");
        btn = UIToolkitUtils.FindChildElement(item, "Btn");
        round = UIToolkitUtils.FindChildElement(item, "Round");
        point = UIToolkitUtils.FindChildElement(item, "Point");
        textLabel = UIToolkitUtils.FindChildElement(item, "Label") as Label;

        UIToolkitUtils.ClearChildrenElements(checkBox);
    }

    public void Init()
    {
        checkBox.AddToClassList(type.ToString().ToLower());

        choiceList = choice.Split(",");

        VisualElement newItem;
        for (int i = 0; i < choiceList.Length; i++)
        {
            newItem = CreateItem(choiceList[i]);
            checkBox.Add(newItem);
        }
    }

    private VisualElement CreateItem(string text)
    {
        VisualElement newItem = itemUxml.Instantiate();
        Label textLabel = UIToolkitUtils.FindChildElement(newItem, "Label") as Label;
        textLabel.text = text;

        newItem.RegisterCallback<ClickEvent>(OnItemClick);

        return newItem;
    }

    private void OnItemClick(ClickEvent evt)
    {
        Debug.Log(evt.currentTarget);
        VisualElement target = evt.currentTarget as VisualElement;
        VisualElement item = UIToolkitUtils.FindChildElement(target, "CheckBoxItem");
        // ͨ�����Ƶݹ������Ԫ��
        if (item.ClassListContains("checked"))
        {
            item.RemoveFromClassList("checked");

        } else
        {
            item.AddToClassList("checked");
        }

    }
}