using Sky9th.UIT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Insertable : VisualElement
{

    private List<VisualElement> moveEle = new();

    public int originalCount = -1;
    public VisualElement insertNode;

    public Insertable () 
    {
        RegisterCallback<AttachToPanelEvent>(GetMoveElement);
    }

    public void GetMoveElement (AttachToPanelEvent evt)
    {
        insertNode = UIToolkitUtils.FindChildElement(this, "InsertNode");
        if (moveEle.Count == 0)
        {
            int count = 0;
            // ����������Ԫ��
            foreach (VisualElement child in Children())
            {
                if (count > originalCount - 1)
                {
                    moveEle.Add(child);
                }
                count++;
            }
            MoveElementIntoInsertNode();
        }
    }

    public void MoveElementIntoInsertNode()
    {
        if (insertNode != null)
        {
            for (int i = 0; i < moveEle.Count; i++)
            {
                insertNode.Add(moveEle[i]);
            }
        }
    }

}
