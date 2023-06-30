using UnityEngine;
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