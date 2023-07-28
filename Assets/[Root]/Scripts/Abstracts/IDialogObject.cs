using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogObject
{
    List<string> Dialogs { get; }
    Sprite Icon { get; }
    GameObject GM { get; }

    Transform CameraPoint { get; }

    bool IsBegineClipShip { get; }


    void SetAnimation();
}
public enum AnimationObjectType
{
    Idle,
    Hello,

}
