namespace Anchor.Unity.UGui.Dialog
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DialogManager
    {
        // 캔버스는 Panel과 Dialog로 나눠진다.
        // Panel은 유일창(거래소, 장비창 등)
        // Dialog는 중복가능 창(알림, 아이템획득 메시지 등)

        // Open 함수를 통해 Component를 전달하면 해당 캔버스가 열린다.
        // Close 함수를 통해 Component를 전달하면 해당 캔버스가 닫힌다.
        // 단 Close 함수를 통해 캔버스를 열고 닫을때 OnEnable, DisEnable을 사용하지 않고 Canvas 컴포넌트를 비활성화 시킨다.

        // 캔버스 네비게이션 기능
        // 캔버스는 Open 될때 고유 ID를 스택에 저장하고 닫을 때 Pop을 통해 Close를 시행한다.

        public static void CloseFromNavigation()
        {

        }
    }
}