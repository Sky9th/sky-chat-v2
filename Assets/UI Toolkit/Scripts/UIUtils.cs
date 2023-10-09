using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sky9th.UIT
{
    public class UIToolkitUtils
    {
        public static VisualElement FindChildElement(VisualElement parent, string searchString)
        {
            if (string.IsNullOrEmpty(searchString) || parent == null)
                return null;
            
            if (parent.name == searchString || parent.ClassListContains(searchString.Substring(1)))
                return parent;

            return searchString[0] == '.' ?
                FindChildByClassName(parent, searchString.Substring(1)) :
                FindChildByName(parent, searchString);
        }

        private static VisualElement FindChildByClassName(VisualElement parent, string className)
        {
            // ͨ�������ݹ������Ԫ��
            foreach (var child in parent.Children())
            {
                if (child.ClassListContains(className))
                    return child;

                var result = FindChildByClassName(child, className);
                if (result != null)
                    return result;
            }

            return null;
        }

        private static VisualElement FindChildByName(VisualElement parent, string name)
        {
            // ͨ�����Ƶݹ������Ԫ��
            foreach (var child in parent.Children())
            {
                if (child.name == name)
                    return child;

                var result = FindChildByName(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }

        public static VisualElement CreateBackDrop(VisualElement parent)
        {
            VisualElement backdrop = new VisualElement();
            backdrop.name = "Backdrop";
            backdrop.AddToClassList("Backdrop");
            backdrop.style.position = Position.Absolute;
            backdrop.style.left = new StyleLength(new Length(-999, LengthUnit.Percent));
            backdrop.style.top = new StyleLength(new Length(-999, LengthUnit.Percent));
            backdrop.style.width = new StyleLength(new Length(99999, LengthUnit.Percent));
            backdrop.style.height = new StyleLength(new Length(99999, LengthUnit.Percent));
            backdrop.style.backgroundColor = new Color(0, 0, 0, 0.35f);
            backdrop.style.display = DisplayStyle.None;

            parent.Insert(0, backdrop);
            return backdrop;
        }
        public static void ClearChildrenElements(VisualElement parent)
        {
            while (parent.childCount > 0)
            {
                // �Ƴ���Ԫ��
                VisualElement child = parent.ElementAt(0);
                parent.Remove(child);
            }
        }
    }

}