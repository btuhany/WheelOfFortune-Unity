using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Items;
using WheelOfFortune.Settings;

namespace WheelOfFortune.Panels
{
    public class RewardsPanelController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private RewardsPanelSettings _settings;

        [Header("References")]
        [SerializeField] private RectTransform _rewardsContentHolder;
        [SerializeField] private Transform _rewardPartsParent;

        private Dictionary<WheelItem, RewardsPanelContentController> _rewardsDictionary = new Dictionary<WheelItem, RewardsPanelContentController>();
        private Tweener _collectionTween;

        //To use Tweeners in async methods, they should be called from sync methods.
        private void RewardPartScaleAnim(Image rewardPart, RewardsPanelContentController targetRewardContent)
        {
            rewardPart.transform.DOScale(targetRewardContent.transform.localScale, _settings.MoveRewPartTime);
        }
        private List<Image> SpawnRewardParts(Transform startPosition, int count, Sprite rewardPartSprite)
        {
            List<Image> rewardsImgList = new List<Image>();
            for (int i = 0; i < count; i++)
            {
                //Spawn & set the reward part and add to the list.
                Image rewardPart = Instantiate(_settings.RewardContentImage,
                    startPosition.position,
                    startPosition.rotation, _rewardPartsParent);
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
            if (_collectionTween == null)
            {
                _collectionTween = targetReward.transform.DOPunchScale(
                    Vector3.one * _settings.CollectionReactScaleFactor,
                    _settings.CollectionReactTime)
                    .SetEase(_settings.CollectionReactEase);
                await _collectionTween.ToUniTask();
                _collectionTween = null;
            }
        }
        private async UniTask MoveAddRewardPart(Image rewardPart, RewardsPanelContentController targetRewardContent, int addCount)
        {
            RewardPartScaleAnim(rewardPart, targetRewardContent);
            await rewardPart.transform.DOMove(
                targetRewardContent.ItemImage.transform.position,
                _settings.MoveRewPartTime)
                .SetEase(_settings.MoveRewPartEase)
                .OnComplete(() => Destroy(rewardPart)).ToUniTask();
            targetRewardContent.IncreaseCount(addCount);
            await ReactToRewardPartCollection(targetRewardContent.ItemImage.transform);
        }
        private async UniTask GatherRewardPartsAnim(WheelItem item, Transform startPosition, RewardsPanelContentController targetRewardContent)
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
                int milliSecondsDelay = (int)Random.Range(_settings.MoveRewPartsMinDelay, _settings.MoveRewPartsMaxDelay);
                await UniTask.Delay(milliSecondsDelay);
                rewardPartTasks.Add(MoveAddRewardPart(rewardsImgList[i], targetRewardContent, rewardAddCountPerPart));
            }

            await UniTask.WhenAll(rewardPartTasks);
        }
        public async UniTask GetReward(WheelItem item, Transform animImgSpawnPoint)
        {
            RewardsPanelContentController rewardContent;

            //Check if item is already registered to the dictionary.
            if (_rewardsDictionary.ContainsKey(item))
            {
                rewardContent = _rewardsDictionary[item];
            }
            else
            {
                rewardContent = Instantiate(_settings.RewardsPanelContentPrefab, _rewardsContentHolder);
                rewardContent.SetReward(item);
                _rewardsDictionary.Add(item, rewardContent);
            }
            await UniTask.Delay(_settings.GatherAnimStartDelay);
            await GatherRewardPartsAnim(item, animImgSpawnPoint, rewardContent);
        }

    }
}

