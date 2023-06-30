using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameObjectBagId
{
    Normal,
}

public enum DialogId
{

}

public enum PanelId
{
    Stage,
    Shop,
}

public partial class Define
{
    private static readonly string[] k_StartAssets =
    {
        "Assets/Prefabs/Start/UI/Canvas_Start.prefab",
    };

    private static readonly string[] k_StageAssets =
    {
        "Assets/Resource/Stage/Assets/Canvas_Stage.prefab",
        "Assets/Resource/Stage/Assets/Canvas_Shop.prefab",
    };

    public static string[] StartAssets => k_StartAssets;

    public static string[] StageAssets => k_StageAssets;
}