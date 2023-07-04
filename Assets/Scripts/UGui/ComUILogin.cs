using System;
using Anchor.Unity;
using Anchor.Unity.UGui.Panel;

public class ComUILogin : ComPanel<ComUILogin>
{
    protected override void OnInit()
    {
    }

    protected override void OnClose()
    {
    }    

    protected override void OnOpen()
    {
    }

    protected override void OnSetBenText(string[] text)
    {
    }

    protected override void OnSetData(EventArgs args)
    {
    }

    public void OnClickGoogle()
    {
        SoundManager.Play(SoundId.eft_ui_click_blop, SoundType.EFT);
        ResourceHelper.LoadScene(SceneId.Start);
    }

    public void OnClickGuest()
    {
        SoundManager.Play(SoundId.eft_ui_click_blop, SoundType.EFT);
        ResourceHelper.LoadScene(SceneId.Start);
    }
}
