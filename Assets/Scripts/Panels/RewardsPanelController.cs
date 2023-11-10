using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Items;
using WheelOfFortune.Pools;
using WheelOfFortune.Settings;

namespace WheelOfFortune.Panels
{
    public class RewardsPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private RewardsPanelSettings _settings;

        [Header("References")]
        [SerializeField] private RectTransform _rectContentHolder;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Transform _transformRewPartsParent;
        [SerializeField] private Button _buttonExit;
        [SerializeField] private RectTransform _rectMask;
        [SerializeField] private GridLayoutGroup _gridLayout;

        private Dictionary<WheelItem, RewardController> _rewardsDictionary
            = new Dictionary<WheelItem, RewardController>();
        private Tweener _tweenCollect;
        private Vector2 _rectMaskMaxOffset;
        private int _totalGold = 0;
        private RewardController _rewardControllerGold;
        private float _initialRectSizeDeltaY;
        private float _initalRectAnchoredPosY;
        private Vector3 _initalRectAnchoredPos;
        private Vector2 _initalAnchorMin;
        private Vector2 _initalAnchorMax;

        public event System.Action OnButtonClickExit;

        private void OnValidate()
        {
            if (_buttonExit == null)
                _buttonExit = GetComponentInChildren<Button>();
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }
        private void Awake()
        {
            _buttonExit.onClick.AddListener(HandleOnExitButtonClick);
            _rectMaskMaxOffset = _rectMask.offsetMax;
            _rectMask.offsetMin = Vector2.zero;
            _initialRectSizeDeltaY = _rectTransform.sizeDelta.y;
            _initalRectAnchoredPos = _rectTransform.anchoredPosition;
            _initalRectAnchoredPosY = _initalRectAnchoredPos.y;
            _initalAnchorMin = _rectTransform.anchorMin;
            _initalAnchorMax = _rectTransform.anchorMax;
        }

        //To use Tweeners in async methods, they should be called from sync methods.
        private void RewardPartScaleAnim(Image rewardPart, RewardController targetRewardContent)
        {
            rewardPart.transform.DOScale(_settings.CollectionTargetScale, _settings.MoveRewPartTime);
        }
        private List<Image> SpawnRewardParts(Transform startPosition, int count, Sprite rewardPartSprite)
        {
            List<Image> rewardsImgList = new List<Image>();
            for (int i = 0; i < count; i++)
            {
                //Spawn & set the reward part and add to the list.
                //Image rewardPart = Instantiate(_settings.RewardContentImage,
                //    startPosition.position,
                //    startPosition.rotation, _transformRewPartsParent);
                Image rewardPart = UIRewardPartImgPool.Instance.GetObject(true);
                rewardPart.transform.SetParent(_transformRewPartsParent);
                rewardPart.transform.position = startPosition.position;
                rewardPart.transform.rotation = startPosition.rotation;

                rewardsImgList.Add(rewardPart);
                rewardPart.sprite = rewardPartSprite;

                //Scale animation
                rewardPart.transform.DOScale(
                    Vector3.one * _settings.SpawnRewPartScaleFactor,
                    _settings.SpawnRewPartScaleTime)
                    .SetEase(_settings.SpawnRewPartScaleEase);
            }
            return rewardsImgList;
        }
        private async UniTask ReactToRewardPartCollection(Transform targetReward)
        {
            if (_tweenCollect == null)
            {
                _tweenCollect = targetReward.transform.DOPunchScale(
                    Vector3.one * _settings.CollectionReactScaleFactor,
                    _settings.CollectionReactTime)
                    .SetEase(_settings.CollectionReactEase);
                await _tweenCollect.ToUniTask();
                _tweenCollect = null;
            }
        }
        private async UniTask MoveAddRewardPart(Image rewardPart, RewardController targetRewardContent, int addCount)
        {
            RewardPartScaleAnim(rewardPart, targetRewardContent);
            await rewardPart.transform.DOMove(
                targetRewardContent.ItemImage.transform.position,
                _settings.MoveRewPartTime)
                .SetEase(_settings.MoveRewPartEase)
                .OnComplete(() => UIRewardPartImgPool.Instance.ReturnObject(rewardPart))
                .ToUniTask();
            targetRewardContent.IncreaseCount(addCount);
            await ReactToRewardPartCollection(targetRewardContent.ItemImage.transform);
        }
        private async UniTask GatherRewardPartsAnim(WheelItem item, Transform startPosition, RewardController targetRewardContent)
        {
            Sprite rewardSprite = item.SpriteReward;
            int rewardPartCount;
            int rewardAddCountPerPart;

            if (item.Count > _settings.GatherMaxRewPart)
            {
                rewardAddCountPerPart = item.Count;
                rewardPartCount = 1;
            }
            else
            {
                rewardPartCount = item.Count;
                rewardAddCountPerPart = 1;
            }

            //Delay for waiting the grid layout positioning
            await UniTask.DelayFrame(_settings.GatherRewPartsDelayFrame);

            List<Image> rewardsImgList = SpawnRewardParts(startPosition, rewardPartCount, rewardSprite);

            List<UniTask> rewardPartStartTasks = new List<UniTask>();
            foreach (Image rewardPart in rewardsImgList)
            {
                //Move to random position
                Vector2 randomOffset = new Vector2(
                    Random.Range(_settings.SpawnMoveOffsetMinVector.x, _settings.SpawnMoveOffsetMaxVector.x),
                    Random.Range(_settings.SpawnMoveOffsetMinVector.y, _settings.SpawnMoveOffsetMaxVector.y));

                rewardPartStartTasks.Add(rewardPart.transform.DOMove(
                startPosition.position + new Vector3(randomOffset.x, randomOffset.y, 0),
                _settings.SpawnMoveOffsetTime)
                .SetEase(_settings.SpawnMoveOffsetEase).ToUniTask());
            }
            await UniTask.WhenAll(rewardPartStartTasks);


            List<UniTask> rewardPartTasks = new List<UniTask>();
            for (int i = 0; i < rewardPartCount; i++)
            {
                int milliSecondsDelay = (int)Random.Range(_settings.MoveRewPartsMillisecondMinDelay, _settings.MoveRewPartsMillisecondMaxDelay);
                await UniTask.Delay(milliSecondsDelay);
                rewardPartTasks.Add(MoveAddRewardPart(rewardsImgList[i], targetRewardContent, rewardAddCountPerPart));
            }

            await UniTask.WhenAll(rewardPartTasks);
        }
        private void CheckItemIsGold(WheelItem item, RewardController reward)
        {
            if (!item.IsGold) return;
            _totalGold += item.Count;

            _rewardControllerGold = reward;
        }
        private void HandleOnExitButtonClick()
        {
            OnButtonClickExit?.Invoke();
        }
        private List<UniTask> ShowEndRewardsTasks(float sizeDeltaY, float anchorPosY)
        {
            List<UniTask> exitTasks = new List<UniTask>();
            exitTasks.Add(_rectTransform.DOSizeDelta(new Vector2(_rectTransform.sizeDelta.x, sizeDeltaY), _settings.ExitShowRewsSizeDelta).ToUniTask());
            exitTasks.Add(_rectTransform.DOAnchorPosY(anchorPosY, _settings.ExitShowRewsSizeDelta).ToUniTask());
            return exitTasks;
        }
        private bool IsItemContained(WheelItem item, out RewardController tempReward)
        {
            tempReward = null;
            foreach (RewardController reward in _rewardsDictionary.Values)
            {
                if (reward.ItemImage.sprite == item.SpriteReward)
                {
                    tempReward = reward;
                    break;
                }
            }
            return tempReward != null;
        }
        public void HideExitButton(bool handleRewardsPos = false)
        {
            _buttonExit.enabled = false;

            _buttonExit.transform
                .DOScale(Vector3.zero, _settings.ExitBtnHideAnimTime)
                .SetEase(_settings.ExitBtnHideAnimEase)
                .onComplete = () => {
                    _buttonExit.gameObject.SetActive(false);

                    if(handleRewardsPos)
                        _rectMask.offsetMax = Vector2.zero;
                };
        }
        public void ShowExitButton(bool handleRewardsPos = false)
        {
            _buttonExit.gameObject.SetActive(true);

            if(handleRewardsPos)
                _rectMask.offsetMax = _rectMaskMaxOffset;

            _buttonExit.transform.
                DOScale(Vector3.one, _settings.ExitBtnUnhideAnimTime)
                .SetEase(_settings.ExitBtnUnhideAnimEase)
                .onComplete = () => {
                    _buttonExit.enabled = true;
                };
        }
        public async UniTask GetReward(WheelItem item, Transform animImgSpawnPoint)
        {
            RewardController rewardContent;

            //Check if item is already registered to the dictionary.
            if (IsItemContained(item, out RewardController reward))
            {
                rewardContent = reward;
            }
            else
            {
                //rewardContent = Instantiate(_settings.RewardsPanelContentPrefab, _rectContentHolder);
                rewardContent = UIRewardHolderPool.Instance.GetObject(true);
                rewardContent.transform.SetParent(_rectContentHolder);
                rewardContent.SetReward(item);
                _rewardsDictionary.Add(item, rewardContent);
            }
            await UniTask.Delay(_settings.GatherAnimStartMillisecondDelay);
            await GatherRewardPartsAnim(item, animImgSpawnPoint, rewardContent);

            CheckItemIsGold(item, rewardContent);

        }
        [ContextMenu("Reset")]
        public void ResetRewards()
        {
            foreach (RewardController rewardController in _rewardsDictionary.Values)
            {
                UIRewardHolderPool.Instance.ReturnObject(rewardController);
            }
            _rewardsDictionary.Clear();
            _totalGold = 0;
            ShowExitButton(true);
        }
        public void HandleOnRevived(int goldCost)
        {
            ShowExitButton(true);
            _totalGold -= goldCost;
            if(_rewardControllerGold != null)
                _rewardControllerGold.SetCount(_totalGold);
        }
        public bool IsGoldEnough(int value)
        {
            if (_totalGold < value)
                return false;
            return true;
        }
        public async UniTask ShowEndRewards(RectTransform targetRectTransfrom)
        {
            HideExitButton(true);
            _gridLayout.constraintCount = _settings.ExitShowRewsGridConstraint;
            List<UniTask> animTasksAnchor = new List<UniTask>();
            animTasksAnchor.Add(_rectTransform.DOAnchorMax(
               targetRectTransfrom.anchorMax,
                _settings.ExitMoveAnimTime)
                .SetEase(_settings.ExitMoveAnimEase)
                .ToUniTask());
            animTasksAnchor.Add(_rectTransform.DOAnchorMin(
              targetRectTransfrom.anchorMin,
                _settings.ExitMoveAnimTime)
                .SetEase(_settings.ExitMoveAnimEase)
                .ToUniTask());
            await UniTask.WhenAll(animTasksAnchor);
            await UniTask.WhenAll(ShowEndRewardsTasks(targetRectTransfrom.rect.height * _settings.HeightDividerSizeDeltaY, targetRectTransfrom.rect.height * _settings.HeightDividerAnchorPosY));
        }
        public async UniTask UnshowEndRewards(RectTransform targetRectTransfrom)
        {
            _gridLayout.constraintCount = 1;
            _rectMask.offsetMin = Vector2.zero;
            await UniTask.WhenAll(ShowEndRewardsTasks(_initialRectSizeDeltaY, _initalRectAnchoredPosY));
            List<UniTask> animTasksAnchor = new List<UniTask>();
            animTasksAnchor.Add(_rectTransform.DOAnchorMax(
                _initalAnchorMax,
                _settings.ExitMoveAnimTime)
                .SetEase(_settings.ExitMoveAnimEase)
                .ToUniTask());
            animTasksAnchor.Add(_rectTransform.DOAnchorMin(
                _initalAnchorMin,
                _settings.ExitMoveAnimTime)
                .SetEase(_settings.ExitMoveAnimEase)
                .ToUniTask());
            await UniTask.WhenAll(animTasksAnchor);
            ShowExitButton(true);
        }
    }
}

