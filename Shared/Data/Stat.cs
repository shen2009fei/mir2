using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ComponentModel;
public sealed class Stats : IEquatable<Stats>
{
    public SortedDictionary<Stat, int> Values { get; set; } = new SortedDictionary<Stat, int>();
    public int Count => Values.Sum(pair => Math.Abs(pair.Value));

    public int this[Stat stat]
    {
        get
        {
            return !Values.TryGetValue(stat, out int result) ? 0 : result;
        }
        set
        {
            if (value == 0)
            {
                if (Values.ContainsKey(stat))
                {
                    Values.Remove(stat);
                }

                return;
            }

            Values[stat] = value;
        }
    }

    public Stats() { }

    public Stats(Stats stats)
    {
        foreach (KeyValuePair<Stat, int> pair in stats.Values)
            this[pair.Key] += pair.Value;
    }

    public Stats(BinaryReader reader)
    {
        int count = reader.ReadInt32();

        for (int i = 0; i < count; i++)
            Values[(Stat)reader.ReadByte()] = reader.ReadInt32();
    }

    public void Add(Stats stats)
    {
        foreach (KeyValuePair<Stat, int> pair in stats.Values)
            this[pair.Key] += pair.Value;
    }

    public void Save(BinaryWriter writer)
    {
        writer.Write(Values.Count);

        foreach (KeyValuePair<Stat, int> pair in Values)
        {
            writer.Write((byte)pair.Key);
            writer.Write(pair.Value);
        }
    }

    public void Clear()
    {
        Values.Clear();
    }

    public bool Equals(Stats other)
    {
        if (Values.Count != other.Values.Count) return false;

        foreach (KeyValuePair<Stat, int> value in Values)
            if (other[value.Key] != value.Value) return false;

        return true;
    }
}

public enum StatFormula : byte
{
    Health,
    Mana,
    Weight,
    Stat
}


public enum Stat : byte
{
    [Description("防御下限")]
    MinAC = 0,
    [Description("防御上限")]
    MaxAC = 1,
    [Description("魔御下限")]
    MinMAC = 2,
    [Description("魔御上限")]
    MaxMAC = 3,
    [Description("攻击下限")]
    MinDC = 4,
    [Description("攻击上限")]
    MaxDC = 5,
    [Description("魔法下限")]
    MinMC = 6,
    [Description("魔法上限")]
    MaxMC = 7,
    [Description("道术下限")]
    MinSC = 8,
    [Description("道术上限")]
    MaxSC = 9,

    [Description("准确")]
    Accuracy = 10,
    [Description("敏捷")]
    Agility = 11,
    [Description("体力值")]
    HP = 12,
    [Description("魔法值")]
    MP = 13,
    [Description("攻速")]
    AttackSpeed = 14,
    [Description("幸运")]
    Luck = 15,
    [Description("背包重量")]
    BagWeight = 16,
    [Description("腕力")]
    HandWeight = 17,
    [Description("穿戴重量")]
    WearWeight = 18,
    [Description("反射")]
    Reflect = 19,
    [Description("强度")]
    Strong = 20,
    [Description("神圣")]
    Holy = 21,
    [Description("石化")]
    Freezing = 22,
    [Description("毒伤")]
    PoisonAttack = 23,

    [Description("魔法抵抗")]
    MagicResist = 30,
    [Description("毒药抵抗")]
    PoisonResist = 31,
    [Description("体力恢复")]
    HealthRecovery = 32,
    [Description("法术恢复")]
    SpellRecovery = 33,
    [Description("毒药恢复")]
    PoisonRecovery = 34, //TODO - Should this be in seconds or milliseconds??
    [Description("暴击率")]
    CriticalRate = 35,
    [Description("暴击伤害")]
    CriticalDamage = 36,

    [Description("最大防御率百分比")]
    MaxACRatePercent = 40,
    [Description("最大魔御率百分比")]
    MaxMACRatePercent = 41,
    [Description("最大攻击率百分比")]
    MaxDCRatePercent = 42,
    [Description("最大魔法率百分比")]
    MaxMCRatePercent = 43,
    [Description("最大道术率百分比")]
    MaxSCRatePercent = 44,
    [Description("攻速率百分比")]
    AttackSpeedRatePercent = 45,
    [Description("体力值率百分比")]
    HPRatePercent = 46,
    [Description("魔法值率百分比")]
    MPRatePercent = 47,
    [Description("体力值消耗率百分比")]
    HPDrainRatePercent = 48,

    [Description("经验率百分比")]
    ExpRatePercent = 100,
    [Description("物品掉落率百分比")]
    ItemDropRatePercent = 101,
    [Description("金币掉落率百分比")]
    GoldDropRatePercent = 102,
    [Description("挖矿成功率百分比")]
    MineRatePercent = 103,
    [Description("宝石成功率百分比")]
    GemRatePercent = 104,
    [Description("钓鱼成功率百分比")]
    FishRatePercent = 105,
    [Description("制造成功率百分比")]
    CraftRatePercent = 106,
    [Description("技能增益倍数")]
    SkillGainMultiplier = 107,
    [Description("攻击加成")]
    AttackBonus = 108,

    [Description("情侣经验率百分比")]
    LoverExpRatePercent = 120,
    [Description("师徒伤害率百分比")]
    MentorDamageRatePercent = 121,
    [Description("师徒经验率百分比")]
    MentorExpRatePercent = 123,
    [Description("伤害降低百分比")]
    DamageReductionPercent = 124,
    [Description("能量护盾百分比")]
    EnergyShieldPercent = 125,
    [Description("能量护盾体力值增益")]
    EnergyShieldHPGain = 126,
    [Description("法力值惩罚百分比")]
    ManaPenaltyPercent = 127,
    [Description("传送法力值惩罚百分比")]
    TeleportManaPenaltyPercent = 128,

    [Description("未知")]
    Unknown = 255
}