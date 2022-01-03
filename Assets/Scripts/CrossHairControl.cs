using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairControl : MonoBehaviour
{
    [SerializeField]
    private Animator crossHairAnimator;

    public float poseAccuracy = 0.0f;

    void Start()
    {
        crossHairAnimator = GetComponent<Animator>();
    }
    public void SetShotState(bool IsShot)
    {
        crossHairAnimator.SetBool("IsShot", IsShot);
        poseAccuracy = 0.04f;
    }
    public void SetRunState(bool IsRun)
    {
        crossHairAnimator.SetBool("IsRun", IsRun);
        poseAccuracy = 0.02f;
    }
}
