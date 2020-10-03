using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CellsDrawer : MonoBehaviour
{
    public Sprite CellSprite;

    public Sprite AvailableCellSprite;

    private List<List<GameObject>> matrix;
    public GameManager GameManager { get; set; }

    private Color emptyCellColor;

    private Color availableCellColor;

    public void ListenTo(GameManager gameManager)
    {
        GameManager = gameManager;
        GameManager.GameStarted += GameStarted;
        GameManager.MoveMade += UpdateMatrix;
        gameManager.AvailableCellsCalculated += DisplayAvailableCells;
        //game.MatchDrawn += OnMatchDrawn;
    }

    public void GameStarted(List<List<Cell>> cells)
    {
        SetMatrix(cells);

        UpdateMatrix(cells);
    }

    public void DisplayAvailableCells(List<Tuple<int, int>> coords)
    {
        if(availableCellColor == new Color(0, 0, 0, 0))
            ColorUtility.TryParseHtmlString("#FD46E4", out availableCellColor);
        foreach (var coord in coords)
        {
            matrix[coord.Item1][coord.Item2].GetComponent<SpriteRenderer>().material.color = availableCellColor;
        }
    }

    private void SetMatrix(List<List<Cell>> cells)
    {
        matrix = new List<List<GameObject>>();
        for(int i = 0; i < cells.Count; i++)
        {
            matrix.Add(new List<GameObject>());
            for(int j = 0;j<cells.Count; j++)
            {
                GameObject element = new GameObject("Cell {" + i + ", " + j + "}");

                float topLeftX = -5.75f;
                float topLeftY = 5.65f;
                float yPos = topLeftY - 1.25f * (i + 1);
                float xPos = topLeftX + 1.25f * (j + 1);
                element.transform.position = new Vector2(xPos, yPos);

                SpriteRenderer spriteRenderer = element.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = CellSprite;
                Color color;
                ColorUtility.TryParseHtmlString("#CC5F5F", out color);
                emptyCellColor = color;
                spriteRenderer.material.color = color;

                matrix[i].Add(element);
            }
        }
    }

    private void UpdateMatrix(List<List<Cell>> cells)
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix.Count; j++)
            {
                SetAppropriateColor(cells[i][j], matrix[i][j]);
            }
        }
    }

    private void SetAppropriateColor(Cell cell, GameObject matrixElement)
    {
        if (cell.State == CellState.Black)
            matrixElement.GetComponent<SpriteRenderer>().material.color = Color.black;
        else if (cell.State == CellState.White)
            matrixElement.GetComponent<SpriteRenderer>().material.color = Color.white;
        else
            matrixElement.GetComponent<SpriteRenderer>().material.color = emptyCellColor;
    }
    public void ChangeColor()
    {
        //if (isBlack)
        //{
            //cell.GetComponent<SpriteRenderer>().material.color = Color.white;
        //}
        //else
        //{
            //cell.GetComponent<SpriteRenderer>().material.color = Color.black;
        //}
        //isBlack = !isBlack;
    }
}
