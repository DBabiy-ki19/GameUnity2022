using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerRender : MonoBehaviour
{
    public bool isStatic = false;
    [SerializeField]
    private float offset = 0;
    [SerializeField]
    private int sortingOrderBase = 5000;
    private new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        renderer.sortingOrder = (int)(sortingOrderBase - transform.position.y + offset);

        if (isStatic)
            Destroy(this);
    }
}
