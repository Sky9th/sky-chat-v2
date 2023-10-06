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
            // 通过类名递归查找子元素
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
            // 通过名称递归查找子元素
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
    }

}