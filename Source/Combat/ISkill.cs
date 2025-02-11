using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.Combat
{
    public interface ISKill
    {
            public string SkillName { get; set; }
            public string Description { get; set; }
            public int BonusDamage { get; set; }
            public int SkillCost { get; set; }
            public string SkillType { get; set; }
            public int StrNeeded { get; set; }
            public int DexNeeded { get; set; }
    }

    // Str 스킬
    public class HandGun(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "테크 피스톨";
        public string Description { get; set; } = "언제나 당신의 꼬마 도우미입니다.";
        public int BonusDamage { get; set; } = 10 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 10;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 3;
        public int DexNeeded { get; set; } = 1;
    }
    public class Raifle(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "스마트 어썰트 라이플";
        public string Description { get; set; } = "요즘에는 쉽게 접할 수 있는 저가형 라이플입니다.";
        public int BonusDamage { get; set; } = 15 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 15;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 5;
        public int DexNeeded { get; set; } = 2;
    }
    public class ShotGun(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "파워 더블 배럴 샷건";
        public string Description { get; set; } = "대상을 원거리에서 공격합니다.";
        public int BonusDamage { get; set; } = 20 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 20;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 7;
        public int DexNeeded { get; set; } = 3;
    }
    public class MachineGun(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "핸드건";
        public string Description { get; set; } = "대상을 원거리에서 공격합니다.";
        public int BonusDamage { get; set; } = 25 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 25;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 9;
        public int DexNeeded { get; set; } = 4;
    }


    //Dex 스킬
    public class OverHeat(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "핸드건";
        public string Description { get; set; } = "대상을 원거리에서 공격합니다.";
        public int BonusDamage { get; set; } = 25 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 25;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 9;
        public int DexNeeded { get; set; } = 4;
    }
    public class ShortCircuit(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "핸드건";
        public string Description { get; set; } = "대상을 원거리에서 공격합니다.";
        public int BonusDamage { get; set; } = 25 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 25;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 9;
        public int DexNeeded { get; set; } = 4;
    }
    public class Cyberpsychosis(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "핸드건";
        public string Description { get; set; } = "대상을 원거리에서 공격합니다.";
        public int BonusDamage { get; set; } = 25 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 25;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 9;
        public int DexNeeded { get; set; } = 4;
    }
    public class Suicide(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "핸드건";
        public string Description { get; set; } = "대상을 원거리에서 공격합니다.";
        public int BonusDamage { get; set; } = 25 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 25;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 9;
        public int DexNeeded { get; set; } = 4;
    }
    public class SystemCollapse(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "핸드건";
        public string Description { get; set; } = "대상을 원거리에서 공격합니다.";
        public int BonusDamage { get; set; } = 25 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 25;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 9;
        public int DexNeeded { get; set; } = 4;
    }
}
