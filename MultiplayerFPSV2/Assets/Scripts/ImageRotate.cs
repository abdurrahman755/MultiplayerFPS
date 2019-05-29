using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRotate : MonoBehaviour {

    [SerializeField]
    private int RotateSpeed = 5;

    private void FixedUpdate()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.Rotate(new Vector3(0, 0, RotateSpeed));
    }

}
