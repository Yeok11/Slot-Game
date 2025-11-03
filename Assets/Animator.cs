using System;
using System.Collections.Generic;

public static class Animator
{
    private static List<IAnimeInfo> animeDatats = new();
    public static void AddAnime(IAnimeInfo _animeData) => animeDatats.Add(_animeData);


    public static Action<Action, List<IAnimeInfo>> OnCallAnime;
    public static void CallAnime(Action _end)
    {
        OnCallAnime?.Invoke(_end, animeDatats);
        animeDatats.Clear();
    }
}