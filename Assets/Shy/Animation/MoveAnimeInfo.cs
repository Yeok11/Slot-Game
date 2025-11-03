using UnityEngine;

public struct MoveAnimeInfo : IAnimeInfo
{
    public float animeTime { get; private set; }
    public Vector3 destination { get; private set; }
    public Transform target { get; private set; }

    public MoveAnimeInfo(Transform _target, float _t, Vector3 _des)
    {
        target = _target;
        animeTime = _t;
        destination = _des;
    }

    AnimeType IAnimeInfo.GetAnimeType() => AnimeType.Move;
}