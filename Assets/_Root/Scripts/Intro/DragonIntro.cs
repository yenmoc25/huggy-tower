using Spine.Unity;
using UnityEngine;

public class DragonIntro : MonoBehaviour, IHasSkeletonDataAsset
{
    public SkeletonAnimation SkeletonAnimation;
    [SerializeField] private SkeletonDataAsset skeletonDataAsset;
    public SkeletonDataAsset SkeletonDataAsset => skeletonDataAsset;

    [SpineAnimation] public string Fly;
    [SpineAnimation] public string Attack;
    [SpineAnimation] public string FlyUp;

    public void PlayFly()
    {
        PlayAnim(Fly, true);
    }

    public void PlayAttack()
    {
        PlayAnim(Attack, true);
    }

    public void PlayFlyUp()
    {
        PlayAnim(FlyUp, false);
    }

    private void PlayAnim(string name, bool isLoop)
    {
        SkeletonAnimation.AnimationState.SetAnimation(0, name, isLoop);
    }
}
