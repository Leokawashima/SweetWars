using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor_Data_Template
{
    public string NickName { get; private set; }//ニックネーム
    public ulong Exp { get; private set; }//経験値
    public byte Level { get; private set; }//レベル
    public uint IndividualValue { get; private set; }//個体値
    //装備品スロット
    public CharactorState_Data_SO State { get; private set; }//キャラクタ
}
