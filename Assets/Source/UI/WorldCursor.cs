using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCursor : MonoBehaviour
{
    public static WorldCursor instance;
    public static Vector3 Position { get => instance.transform.position; }

    public LayerMask worldLayer;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, worldLayer))
        {
            transform.position = hit.point;
        }
    }
}
