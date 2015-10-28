using UnityEngine;
using UnityEditor;
using System;

public class WaveAsset
{
    [MenuItem("Assets/Create/Wave")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Wave>();
    }
}