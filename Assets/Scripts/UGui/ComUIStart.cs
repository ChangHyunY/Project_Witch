using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anchor;

namespace Witch
{
    public class ComUIStart : MonoBehaviour
    {
        public void OnClickEventGameStart()
        {
            ResourceHelper.LoadScene(SceneId.Stage);
        }
    }
}