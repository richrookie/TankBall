using UnityEngine.SceneManagement;

public class UI_PopupVictory : UI_Popup
{
    enum Buttons
    {
        Button_Next
    }

    private void Awake()
    {
        Bind<UnityEngine.UI.Button>(typeof(Buttons));

        Managers.Game.GameEnd();

        ButtonInit();
    }


    private void ButtonInit()
    {
        GetButton(Buttons.Button_Next).onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
        });
    }
}
