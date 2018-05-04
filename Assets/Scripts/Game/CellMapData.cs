using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMapData
{
    public CellMapData()
    {
        
    }
    public CellMapData(int x, int y)
    {
        int count = x * y;
        mapSizeX = x;
        mapSizeY = y;
        cellData = new CellData[count];
        for (int i = 0; i < count; i++)
        {
            cellData[i] = new CellData();
        }
    }
    public void resizeCellMap(int x, int y)
    {
        int newCount = x * y;
        var newCellDataSetting = new CellData[newCount];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                newCellDataSetting[i * y + j] = new CellData();
                if (i < mapSizeX && j < mapSizeY)
                {
                    newCellDataSetting[i * y + j] = cellData[i * mapSizeY + j];
                }
            }
        }
        mapSizeX = x;
        mapSizeY = y;
        cellData = newCellDataSetting;
    }

    public int mapSizeX = 1;
    public int mapSizeY = 1;
    public CellData[] cellData = new CellData[1];
}