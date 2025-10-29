using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : MonoBehaviour
{
    private void Start()
    {
        Animator.EndAnime += UseAnime;
    }

    public void UseAnime(Action _endAction, List<IAnimeData> _animeDatas)
    {
        if (_animeDatas.Count != 0)
        {

        }

        _endAction?.Invoke();
    }
}
