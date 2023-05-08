using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anchor
{
    public class ProjectSetting
    {
        public static void Initalize()
        {
            Application.targetFrameRate = 60;

            ResourceHelper.Initalize();
        }
    }
}