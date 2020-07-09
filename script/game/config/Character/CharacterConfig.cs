using System.Collections.Generic;
using System.Drawing;
using Core;

namespace Game.Config.Character
{
    public enum Job
    {
        swordman,   // 鬼劍士
        fighter,    // 格斗家
        gunner,     // 神槍手
        mage,       // 魔法師
        priest,     // 聖職者
        at_gunner,  // 女神槍手
        thief,      // 暗夜使者
        at_fighter, // 男格斗家
        at_mage,    // 男魔法師
        demonic_swordman,   // 黑暗武士
        creator_mage,       // 締造者
    }
    public class Skill
    {
        public int sid;
        public int lv;
    }
    public class GrowBase
    {
        public string name;
        public int hp_max, mp_max;
        public int physical_attack, physical_defense;
        public int magical_attack, magical_defense;
        public int InventoryLimit; // 负重
        public int mp_regen_speed;
        public int move_speed, attack_speed, cast_speed, jump_speed;
        public int hit_recovery;    // 硬直
        public int weight;          // 体重

        public int dark_resistance, light_resistance;
        public Skill[] Skills;
    }
    // 装备損壞率
    public class ModuelDamageRate
    {
        public int[] module_damage_rate;
    }
    public class GrowValue : GrowBase
    {
        public ModuelDamageRate ModuelDamage; // 装备損壞率
        public GrowValue[] Awakens;     // 觉醒
    }
    public class Config : SampleConfig
    {
        public Job job { get; }
        public Size Width { get; } // unkown : 40, 10

        public GrowBase Initial;    // 出生
        public GrowValue[] Grows;   // 职业

        public string body_image_path; // 皮肤

        public Dictionary<string, AnimeConfig> Animes;
    }
}