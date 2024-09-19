using DG.Tweening;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace RacerVsCops
{
    public class AnimationPopup : MonoBehaviour
    {
        [SerializeField] private RectTransform itemToAnimateRect;
        [SerializeField] private float startTime = 1f;
        [SerializeField] private float endTime = 1f;

        [SerializeField] private Ease startEaseType;
        [SerializeField] private Ease endEaseType;

        [SerializeField] private Vector2 initialPos;
        [SerializeField] private Vector2 desiredPos;

        [SerializeField] private TweenType tweenType;

        [SerializeField] private bool canPlayOnUpdate = false;

        [SerializeField] private List<AnimationConfig> otherItemsToAnimateList = new List<AnimationConfig>();

        internal void StartTween()
        {
            switch (tweenType)
            {
                case TweenType.MOVE:
                    DOTweenMOVE(itemToAnimateRect, startTime, startEaseType, desiredPos, canPlayOnUpdate);
                    if (!Equals(otherItemsToAnimateList.Count, 0))
                    {
                        foreach (AnimationConfig config in otherItemsToAnimateList)
                            DOTweenMOVE(config.ItemToAnimateRect, config.StartTime, config.StartEaseType, config.DesiredPos, config.CanPlayOnUpdate);
                    }
                    break;

                case TweenType.SCALE:
                    DOTweenSCALE(itemToAnimateRect, startTime, startEaseType, desiredPos, canPlayOnUpdate);
                    break;
            }
        }

        internal void StopTween(Action onComplete)
        {
            switch (tweenType)
            {
                case TweenType.MOVE:
                    DOTweenMOVE(itemToAnimateRect, endTime, endEaseType, initialPos, canPlayOnUpdate, onComplete);
                    if (!Equals(otherItemsToAnimateList.Count, 0))
                    {
                        foreach (AnimationConfig config in otherItemsToAnimateList)
                            DOTweenMOVE(config.ItemToAnimateRect, config.EndTime, config.EndEaseType, config.InitialPos, config.CanPlayOnUpdate);
                    }
                    break;

                case TweenType.SCALE:
                    DOTweenSCALE(itemToAnimateRect, endTime, endEaseType, initialPos, canPlayOnUpdate, onComplete);
                    break;
            }
        }

        private void DOTweenMOVE(RectTransform rectTransform, float duration, Ease easeType, Vector2 position, bool canPlayOnUpdate, Action OnCompleteTween = null)
        {
            rectTransform.DOAnchorPos(position, duration).SetEase(easeType, 0.8f).SetUpdate(canPlayOnUpdate).OnComplete(() => OnCompleteTween?.Invoke()).OnComplete(() => rectTransform.DOKill());
        }

        private void DOTweenSCALE(RectTransform rectTransform, float duration, Ease easeType, Vector2 position, bool canPlayOnUpdate, Action OnCompleteTween = null)
        {
            rectTransform.DOScale(position, duration).SetEase(easeType, 0.8f).SetUpdate(canPlayOnUpdate).OnComplete(() => OnCompleteTween?.Invoke()).OnComplete(() => rectTransform.DOKill());
        }

        [Serializable]
        internal sealed class AnimationConfig
        {
            [SerializeField] private RectTransform itemToAnimateRect;
            [SerializeField] private float startTime = 1f;
            [SerializeField] private float endTime = 1f;
            [SerializeField] private Ease startEaseType;
            [SerializeField] private Ease endEaseType;
            [SerializeField] private Vector2 initialPos;
            [SerializeField] private Vector2 desiredPos;
            [SerializeField] private bool canPlayOnUpdate = false;

            internal RectTransform ItemToAnimateRect => itemToAnimateRect;
            internal float StartTime => startTime;
            internal float EndTime => endTime;
            internal Ease StartEaseType => startEaseType;
            internal Ease EndEaseType => endEaseType;
            internal Vector2 InitialPos => initialPos;
            internal Vector2 DesiredPos => desiredPos;
            internal bool CanPlayOnUpdate => canPlayOnUpdate;
        }
    }
}
