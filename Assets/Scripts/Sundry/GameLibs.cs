using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using LitJson;

public class GameConvert
{
    static public void ToJsonWritter(JsonWriter writer, Vector3 data)
    {
        writer.WriteArrayStart();
        writer.Write(Convert.ToDouble(data.x));
        writer.Write(Convert.ToDouble(data.y));
        writer.Write(Convert.ToDouble(data.z));
        writer.WriteArrayEnd();
    }
    static public void ToJsonWritter(JsonWriter writer, Vector2Int data)
    {
        writer.WriteArrayStart();
        writer.Write(data.x);
        writer.Write(data.y);
        writer.WriteArrayEnd();
    }
}
