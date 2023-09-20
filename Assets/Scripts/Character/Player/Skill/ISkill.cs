using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    bool CanUse { get; set; }
    float CoolTime { get; set; }
    float RemainTime { get; set; }

    //인터페이스에서는 가상의 선언만 되고 초기화는 안되기때문에
    void Work(Player player);
}
