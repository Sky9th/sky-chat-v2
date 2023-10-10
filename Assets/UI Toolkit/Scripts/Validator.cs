using Sky9th;
using Sky9th.UIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class Validator <T> : Insertable
{
    public new class UxmlFactory : UxmlFactory<TextInputValidator, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription validator = new() { name = "Validator", defaultValue = "" };
        UxmlStringAttributeDescription errorMsgStr = new() { name = "errorMsgStr", defaultValue = "" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Validator<T>;
            ate.validator = validator.GetValueFromBag(bag, cc);
            ate.errorMsgStr = errorMsgStr.GetValueFromBag(bag, cc);
            ate.InitValidator();
        }
    }

    public string validator { get; set; }
    public string errorMsgStr { get; set; }


    public string[] errorMsg;
    public List<string> validatorCallback = new();
    public List<string> validatorMsg = new();
    public bool isDirty = false;
    public bool[] isError;
    public HashSet<string> errorMsgList = new();
    public T value;

    public Validator()
    {
        RegisterCallback<ChangeEvent<T>>(OnChange);
    }

    public void InitValidator()
    {
        errorMsg = errorMsgStr.Split(",");
        if (validator != null)
        {
            string[] validators = validator.Split(",");
            Type validatorClass = typeof(Validator);
            MethodInfo[] methodInfos = validatorClass.GetMethods();
            string[] methods = new string[methodInfos.Length];
            for (int i = 0; i < methodInfos.Length; i++)
            {
                MethodInfo methodInfo = methodInfos[i];
                methods[i] = methodInfo.Name;
            }
            for (int p = 0; p < validators.Length; p++)
            {
                if (validators[p] == "" || validators[p] == null) { continue; }
                if (!methods.Contains(validators[p]))
                {
                    Debug.Log("Unsupport validator");
                }
                else
                {
                    validatorCallback.Add(validators[p]);
                }
            }
        }
    }

    public void OnChange(ChangeEvent<T> evt)
    {
        Verify();
    }

    public bool Verify ()
    {
        RemoveFromClassList("danger");
        Type validatorClass = typeof(Validator);
        VisualElement errorMsgContainer = UIToolkitUtils.FindChildElement(this, ".errorMsg");
        errorMsgList = new();
        if (errorMsgContainer != null)
        {
            UIToolkitUtils.ClearChildrenElements(errorMsgContainer);
        }
        isError = new bool[validatorCallback.Count];
        validatorMsg = new();
        for (int i = 0; i < validatorCallback.Count; i++)
        {
            MethodInfo method = validatorClass.GetMethod(validatorCallback[i]);
            object obj = method.Invoke(null, new object[] { value });
            isError[i] = (bool)obj;
            if (!isError[i])
            {
                AddToClassList("danger");
                if (errorMsg.Length > 0 && errorMsg[i] != null)
                {
                    errorMsgList.Add(errorMsg[i]);
                }
            }
        }
        return errorMsgList.Count > 0;
    }
}
