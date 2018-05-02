using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Fixed
{
    public long data;
    public Fixed(Double value)
    {
        data = (long)Math.Round(value * 10000);
    }
    public Fixed(Single value)
    {
        data = (long)Math.Round(value * 10000);
    }
    public Double ToDouble()
    {
        return data * 0.0001;
    }
    public Single ToSingle()
    {
        return data * 0.0001f;
    }
}

public struct CellDataSetting
{
    public bool isEnable;
}

public class CellMapSetting
{
    public int mapSizeX = 1;
    public int mapSizeY = 1;
    public double spacing = 0.1;
    public CellDataSetting[] cellData = new CellDataSetting[1];
    
    public CellMapSetting()
    {

    }
    public CellMapSetting(int x, int y)
    {
        int count = x * y;
        mapSizeX = x;
        mapSizeY = y;
        cellData = new CellDataSetting[count];
        for (int i = 0; i < count; i++)
        {
            cellData[i].isEnable = true;
        }
    }
    public void resizeCellMap(int x, int y)
    {
        int count = mapSizeX * mapSizeY;
        int newCount = x * y;
        var newCellData = new CellDataSetting[newCount];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if(i < mapSizeX && j < mapSizeY)
                {
                    newCellData[i * y + j] = cellData[i * mapSizeY + j];
                }
                else
                {
                    newCellData[i * y + j].isEnable = true;
                }
            }
        }
        mapSizeX = x;
        mapSizeY = y;
        cellData = newCellData;
    }
}
