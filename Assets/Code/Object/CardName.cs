﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardName : MonoBehaviour
{
    public string sortingLayerName;
    public int sortingOrder;
    private string cost;
    private TextMesh textMesh;

    void Start () 
    {
        MeshRenderer mesh = GetComponent<MeshRenderer> ();
        textMesh = GetComponent<TextMesh>();
        mesh.sortingLayerName = sortingLayerName;
        mesh.sortingOrder = sortingOrder;
        ShowCard show = transform.parent.GetComponent<ShowCard>();
        if(show.card.cost == 0)
            cost = "X";
        else
            cost = "" + show.card.cost;
        textMesh.text = cost;
        transform.position += Vector3.back;
    }
}
