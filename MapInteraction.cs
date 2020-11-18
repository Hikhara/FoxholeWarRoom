using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapInteraction : MonoBehaviour
{
    //public Image image;
    public float zoom;
    //public GameObject image;
    public Canvas image;

    Vector3 minScale = new Vector3(1, 1, 1);
    Vector3 maxScale = new Vector3(11, 11, 1);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float zoomValue = Input.GetAxis("Mouse ScrollWheel");

        if (zoomValue != 0 && zoom <= 11)
        {
            //zoomValue sometimes adds or removes 0.00001
            image.transform.localScale += Vector3.one * zoomValue;
            image.transform.localScale = Vector3.Max(transform.localScale, minScale);
            image.transform.localScale = Vector3.Min(transform.localScale, maxScale);
            zoom = zoom + zoomValue;
            Debug.Log(zoom);
        }
        else
        {
            zoom = zoom + zoomValue;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position += (Vector3)eventData.delta;
    }
}
