using Sky9th.UIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Form : Insertable
{
    public delegate void OnSubmit(object data);
    public new class UxmlFactory : UxmlFactory<Form, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Form;

            ate.Init();
        }
    }

    private Button confirmBtn;
    private Button resetBtn;

    public OnSubmit onSubmitEvent;

    public Form()
    {
        confirmBtn = UIToolkitUtils.FindChildElement(this, "Confirm") as Button;
        resetBtn = UIToolkitUtils.FindChildElement(this, "Reset") as Button;

        confirmBtn.RegisterCallback<ClickEvent>(OnConfirm);
        resetBtn.RegisterCallback<ClickEvent>(OnReset);
    }

    private void OnReset(ClickEvent evt)
    {
        Debug.Log("Reset");
    }

    private void OnConfirm(ClickEvent evt)
    {
        bool verify = true;
        Debug.Log("Confirm");
        VisualElement formNode = UIToolkitUtils.FindChildElement(this, "InsertNode");

        if (formNode.childCount > 0)
        {
            foreach (VisualElement formControl in formNode.Children())
            {
                StringFormControl f = formControl as StringFormControl;
                VisualElement insertNode = UIToolkitUtils.FindChildElement(f, "InsertNode");
                if( insertNode != null && insertNode.childCount > 0 )
                {
                    VisualElement inputField = insertNode.Children().First();
                    Debug.Log(inputField);
                    Type inputType = inputField.GetType();
                    if (inputType == typeof(TextInputField))
                    {
                        TextInputField textInputField = inputField as TextInputField;
                        verify = textInputField.Verify();
                    }
                    else if (inputType == typeof(Select))
                    {
                        Select textInputField = inputField as Select;
                        verify = textInputField.Verify();
                    }
                    else if (inputType == typeof(CheckBox))
                    {
                        CheckBox textInputField = inputField as CheckBox;
                        verify = textInputField.Verify();
                    }
                    else if (inputType == typeof(Radio))
                    {
                        Radio textInputField = inputField as Radio;
                        verify = textInputField.Verify();
                    }
                    else if (inputType == typeof(Slider))
                    {
                        Slider textInputField = inputField as Slider;
                        verify = textInputField.Verify();
                    }
                    else if (inputType == typeof(Switch))
                    {
                        Switch textInputField = inputField as Switch;
                        verify = textInputField.Verify();
                    }
                }
            }
        }

        if (!verify)
        {
            return;
        }

        if (onSubmitEvent != null)
        {
            onSubmitEvent.Invoke("test");
        }
    }

    public void Init ()
    {
    }
}
