using Sky9th.UIT;
using UnityEngine;
using UnityEngine.UIElements;

public class FormControl : Insertable
{
    public new class UxmlFactory : UxmlFactory<FormControl, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {

        UxmlStringAttributeDescription label = new() { name = "Label", defaultValue = "" };


        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as FormControl;
                
            ate.label = label.GetValueFromBag(bag, cc);
            ate.Init();
        }
    }

    private string label { get; set; }

    private Label labelText;

    public FormControl()
    {
        labelText = UIToolkitUtils.FindChildElement(this ,"LabelText") as Label;
    }

    public void Init ()
    {
        labelText.text = label;
    }
}
