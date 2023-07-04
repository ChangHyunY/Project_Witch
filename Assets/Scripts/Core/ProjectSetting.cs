using UnityEngine;
using Anchor.Unity;

namespace Anchor
{
    public class ProjectSetting
    {
        public static void Initalize()
        {
            Application.targetFrameRate = 60;

            ResourceHelper.Initalize();
            DialogManager.Initialize();
            SoundManager.Initalize();
        }
    }
}