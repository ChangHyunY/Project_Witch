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

public enum SoundId
{
    eft_ui_click_blop,
}

public enum SoundType
{
    BGM,
    EFT,
}

public partial class Define
{
    private static readonly string[] k_LoginAssets =
    {
        "Assets/Resource/Login/Assets/Canvas_Login.prefab",
    };

    private static readonly string[] k_StartAssets =
    {
        
    };

    private static readonly string[] k_StageAssets =
    {
        "Assets/Resource/Stage/Assets/Canvas_Stage.prefab",
        "Assets/Resource/Stage/Assets/Canvas_Shop.prefab",
    };

    private static readonly string[] k_SoundAssets =
    {
        "Assets/Resource/Sound/Assets/eft_ui_click_blop.mp3",
    };

    public static string[] LoginAssets => k_LoginAssets;

    public static string[] StartAssets => k_StartAssets;

    public static string[] StageAssets => k_StageAssets;

    public static string[] SoundAssets => k_SoundAssets;


    private const string k_SoundPath = "Assets/Resource/Sound/Assets/";

    public static string SoundPath => k_SoundPath;
}