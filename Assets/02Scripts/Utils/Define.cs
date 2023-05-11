public class Define
{
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
