using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    static public void LitJsonInit()
    {
        JsonMapper.RegisterExporter<float>((obj, writerFloat) => writerFloat.Write(Convert.ToDouble(obj)));
        JsonMapper.RegisterImporter<double, float>(inputFlot => Convert.ToSingle(inputFlot));
        JsonMapper.RegisterExporter<Vector3>((obj, writerVector3) => GameConvert.ToJsonWritter(writerVector3, obj));
        JsonMapper.RegisterImporter<double[], Vector3>(inputVector3 => new Vector3(Convert.ToSingle(inputVector3[0]), Convert.ToSingle(inputVector3[1]), Convert.ToSingle(inputVector3[2])));
        JsonMapper.RegisterExporter<Vector2Int>((obj, writerVector2Int) => GameConvert.ToJsonWritter(writerVector2Int, obj));
        JsonMapper.RegisterImporter<int[], Vector2Int>(inputVector2Int => new Vector2Int(inputVector2Int[0], inputVector2Int[1]));
    }
}
