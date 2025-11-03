using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    private int animeCnt, checkedAnimeCnt;
    private Action OnAnimeFin;

    private void Start()
    {
        Animator.OnCallAnime += UseAnime;
    }

    private void OnDestroy()
    {
        Animator.OnCallAnime -= UseAnime;
    }

    public void UseAnime(Action _endAction, List<IAnimeInfo> _animeDatas)
    {
        checkedAnimeCnt = 0;
        OnAnimeFin = _endAction;

        if (_animeDatas.Count != 0)
        {
            animeCnt = _animeDatas.Count;

            for (int i = 0; i < animeCnt; i++)
            {
                SwitchAnime(_animeDatas[i]);
            }
        }
        else
        {
            AllAnimeFinCheck();
        }
    }

    private void AllAnimeFinCheck()
    {
        if (++checkedAnimeCnt < animeCnt) return;

        OnAnimeFin?.Invoke();
    }

    private void SwitchAnime(IAnimeInfo _animeInfo)
    {
        switch (_animeInfo.GetAnimeType())
        {
            case AnimeType.Bounce:
                break;
            case AnimeType.Move: StartCoroutine(MoveCoroutine((MoveAnimeInfo)_animeInfo));
                break;
            case AnimeType.CountDown:
                break;
            case AnimeType.Fade:
                break;
        }
    }

    private IEnumerator MoveCoroutine(MoveAnimeInfo _info)
    {
        WaitForSeconds _t = new WaitForSeconds(_info.animeTime * 0.1f);
        Vector3 vec = (_info.destination - _info.target.position) * 0.1f;

        for (int i = 0; i < 10; i++)
        {
            _info.target.position += vec;
            yield return _t;
        }

        AllAnimeFinCheck();
    }
}