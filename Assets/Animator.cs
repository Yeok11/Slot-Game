using System;
using System.Collections.Generic;

public static class Animator
{
    private static List<IAnimeData> animeDatats = new();
    public static void AddAnime(IAnimeData _animeData) => animeDatats.Add(_animeData);


    public static Action<Action, List<IAnimeData>> EndAnime;
    public static void CallAnime(Action _end)
    {
        EndAnime?.Invoke(_end, animeDatats);
        animeDatats.Clear();
    }
}
