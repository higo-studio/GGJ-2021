/**
 * 机关接口
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGear
{

    //触发
    void Triggering();

    //被其他机关调用触发
    void Invoke(IGear from);

    //机关失活,自己调用
    void ShutDown();

    //调用数减少
    void UnInvoke();

    //该机关的触发目标
    List<IGear> Targets { get; }

    //该机关的触发源机关
    List<IGear> Triggers { get; }



    bool IsTriggering { get; }

    void AddTarget(IGear gear);

    void RemoveTarget(IGear gear);

    void AddTrigger(IGear gear);

    void RemoveTrigger(IGear gear);

}
