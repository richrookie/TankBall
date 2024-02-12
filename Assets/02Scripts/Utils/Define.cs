public class Define
{
    public static float xBoundary = 5f;
    public static UnityEngine.Vector3 LeftCornerVec = new UnityEngine.Vector3(-5f, 1.25f, 0);
    public static UnityEngine.Vector3 RightCornerVec = new UnityEngine.Vector3(5f, 1.25f, 0);
    public static UnityEngine.LayerMask LayerTouch = 1 << 9;

    public enum eGameState : byte
    {
        Ready,
        Play,
        End,
    }

    public enum eSound : byte
    {
        Bgm,
        MaxCount
    }

    public enum eBlockType : byte
    {
        Pingpong,
        None
    }

    public enum eColorType : byte
    {
        Red,
        Blue
    }

    public enum ePingpongType : byte
    {
        Plus,
        Muliply,
        None
    }
}
