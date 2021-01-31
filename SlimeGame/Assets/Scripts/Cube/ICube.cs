using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICube
{
    bool Pollutable { get; }

    bool IsPolluted { get; }

    bool Hurtable { get; }

    //对玩家的伤害值
    float Verlies { get; }

    //被污染
    float PollutedByRun();

    void PollutedByShoot();

    //void Clean();
}
