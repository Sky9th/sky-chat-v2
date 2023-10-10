using Sky9th.UIT;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using WebSocketSharp;

public class Slider : Validator <int>
{
    public new class UxmlFactory : UxmlFactory<Slider, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : Validator<int>.UxmlTraits
    {

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Slider;

            ate.Init();
        }
    }

    private VisualTreeAsset uxml;
    private VisualElement point;
    private VisualElement bar;
    private VisualElement checkedBar;
    private Label tips;
    private VisualElement barContainer;
    private VisualElement backdrop;

    private bool isDragging;
    private float percent;

    public Slider()
    {

        point = UIToolkitUtils.FindChildElement(this, "Point");
        bar = UIToolkitUtils.FindChildElement(this, "Bar");
        checkedBar = UIToolkitUtils.FindChildElement(this, "Checked");
        tips = UIToolkitUtils.FindChildElement(this, "Tips") as Label;
        barContainer = UIToolkitUtils.FindChildElement(this, "BarContainer");
        backdrop = UIToolkitUtils.CreateBackDrop(this);

        backdrop.RegisterCallback<MouseMoveEvent>(MovePoint);
        barContainer.RegisterCallback<MouseMoveEvent>(MovePoint);
        barContainer.RegisterCallback<ClickEvent>(ClickPoint);

        point.RegisterCallback<MouseDownEvent>(StartMovePoint);
        point.RegisterCallback<MouseUpEvent>(EndMovePoint);

        backdrop.RegisterCallback<MouseDownEvent>(StartMovePoint);
        backdrop.RegisterCallback<MouseUpEvent>(EndMovePoint);
    }

    private void SetPoint(Vector2 mousePos)
    {
        Vector2 startPos = bar.worldBound.position;
        Vector2 endPos = new Vector2(startPos.x + bar.worldBound.width, startPos.y + bar.worldBound.width);
        if (startPos.x < mousePos.x && mousePos.x < endPos.x && isDragging)
        {
            percent = (mousePos.x - startPos.x) / (endPos.x - startPos.x - 3) * 100;
        } else if (mousePos.x > endPos.x && isDragging)
        {
            percent = 100;
        }
        checkedBar.style.width = new StyleLength(new Length(percent, LengthUnit.Percent));
        point.style.left = new StyleLength(new Length(percent, LengthUnit.Percent));
        tips.text = ((int)percent).ToString();
        value = (int)percent;
    }

    private void ClickPoint(ClickEvent evt)
    {
        isDragging = true;
        Vector2 mousePos = evt.position;
        SetPoint(mousePos);
        isDragging = false;
    }

    private void MovePoint (MouseMoveEvent evt)
    {
        Vector2 mousePos = evt.mousePosition;
        SetPoint(mousePos);
    }

    private void StartMovePoint (MouseDownEvent evt)
    {
        isDragging = true;
        backdrop.style.display = DisplayStyle.Flex;
    }

    private void EndMovePoint (MouseUpEvent evt)
    {
        isDragging = false;
        backdrop.style.display = DisplayStyle.None;
    }

    public void Init ()
    {
        tips.text = percent.ToString();
    }

    public void Update()
    {
    }
}
