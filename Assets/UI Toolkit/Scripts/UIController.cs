using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{

    private UIDocument doc;
    private VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        doc = GetComponent<UIDocument>();
        root = doc.rootVisualElement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
