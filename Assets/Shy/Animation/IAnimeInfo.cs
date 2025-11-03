public enum AnimeType
{
    Bounce,
    Move,
    CountDown,
    Fade,
}

public interface IAnimeInfo
{
    public AnimeType GetAnimeType();
}