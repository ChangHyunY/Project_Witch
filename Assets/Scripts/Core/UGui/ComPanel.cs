using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anchor.Unity.UGui.Dialog;

public class ComPanel<T> : MonoBehaviour
{
    // 패널은 생성되었을 때, 네비게이션 기능을 사용할지 채크하고 DialogManager에 저장된다.

    [SerializeField] private bool m_Navigated = false;
}
