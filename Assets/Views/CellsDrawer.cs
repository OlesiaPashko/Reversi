using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsDrawer : MonoBehaviour
{
    public bool isBlack = false;

    public GameObject cell;

    public void ChangeColor()
    {
        if (isBlack)
        {
            cell.GetComponent<SpriteRenderer>().material.color = Color.white;
        }
        else
        {
            cell.GetComponent<SpriteRenderer>().material.color = Color.black;
        }
        isBlack = !isBlack;
    }
}
