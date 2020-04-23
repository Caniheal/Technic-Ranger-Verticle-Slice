using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FontColour : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = new Color(0, 0, 0, 255);

    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<Text>().color = new Color(255, 255, 255, 255);

    }
}


