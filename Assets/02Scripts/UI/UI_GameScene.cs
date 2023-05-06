using UnityEngine.UI;
using TMPro;

public class UI_GameScene : UI_Scene
{

    private void Awake()
    {
        // Bind<Button>(typeof(Buttons));
        // Bind<TextMeshProUGUI>(typeof(TextMeshs));
        // Bind<Image>(typeof(Images));
        base.Init();
    }
}
