using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonAnimation : MonoBehaviour
{
    [SerializeField] public Animator targetAnimator;     // The animator on the object you want to animate
    [SerializeField] public string triggerName = "isClicked"; // The trigger parameter name in the Animator

    // This function is called from the Button's OnClick()
    public void PlayAnimation()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogWarning("Target Animator not assigned.");
        }
    }
}
