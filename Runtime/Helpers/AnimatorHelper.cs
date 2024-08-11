using UnityEngine;

public static class AnimatorHelper
{
    /// <summary>
    /// Copies the avatar and runtime animator controller from one Animator to another and then rebinds the target Animator.
    /// </summary>
    /// <param name="target">The Animator that will receive the copied properties.</param>
    /// <param name="source">The Animator from which the properties will be copied.</param>
    public static void CopyFrom(this Animator target, Animator source)
    {
        target.avatar = source.avatar;
        target.runtimeAnimatorController = source.runtimeAnimatorController;
        target.Rebind();
    }
}