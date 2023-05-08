using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameObjectBagId
{
    Normal,
}

public partial class Define
{
    private static readonly string[] k_StartAssets =
    {
        "Assets/Prefabs/Start/UI/Canvas_Start.prefab",
    };

    private static readonly string[] k_StageAssets =
    {
        "Assets/Prefabs/Stage/Actor/Player/Player.prefab",
        "Assets/Prefabs/Stage/Actor/Monsters/Slime/Slime_01.prefab",
    };

    private static readonly string[] k_StageDefaultAssets =
    {

    };

    public static string[] StartAssets => k_StartAssets;

    public static string[] StageAssets => k_StageAssets;
}