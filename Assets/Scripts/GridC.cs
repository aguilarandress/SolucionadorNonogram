using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridC 
{
    private int height;
    private int width;
    private float CellSize;
    private int [,] gridArray;
    public GridC(int height,int width,float CellSize)
    {
        this.width = width;
        this.height = height;
        this.CellSize = CellSize;

        this.gridArray = new int[width, height];
        GameObject parent = GameObject.Find("Container");
        for (int i=0;i<gridArray.GetLength(0);i++)
        {
            for (int x = 0; x < gridArray.GetLength(1); x++)
            {
                {
                    UtilsClass.CreateWorldText(gridArray[i, x].ToString(),parent.transform, getWorldPosition(i, x), 900, Color.white, TextAnchor.LowerLeft);   
                    
                }
            }
        }
    }
    private Vector3 getWorldPosition(int x , int y)
    {
        return new Vector3(x, y) * this.CellSize;
    }
}
