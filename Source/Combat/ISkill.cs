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
        public string Description { get; set; } = "작지만 훌륭한 대화수단입니다.";
        public int BonusDamage { get; set; } = 10 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 10;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 3;
        public int DexNeeded { get; set; } = 1;
    }
    public class Raifle(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "스마트 어썰트 라이플";
        public string Description { get; set; } = "쉽게 접할 수 있는 저가형 라이플입니다.";
        public int BonusDamage { get; set; } = 15 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 15;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 5;
        public int DexNeeded { get; set; } = 2;
    }
    public class ShotGun(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "파워 더블 배럴 샷건";
        public string Description { get; set; } = "이름에 파워가 들어가는 이유가 있습니다.";
        public int BonusDamage { get; set; } = 20 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 20;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 7;
        public int DexNeeded { get; set; } = 3;
    }
    public class MachineGun(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "밀리테크 머신 건";
        public string Description { get; set; } = "원래는 거치형 중화기입니다.";
        public int BonusDamage { get; set; } = 25 + gameManager.GameData.Player.Str;
        public int SkillCost { get; set; } = 25;
        public string SkillType { get; set; } = "Human";
        public int StrNeeded { get; set; } = 9;
        public int DexNeeded { get; set; } = 4;
    }


    //Dex 스킬
    public class OverHeat(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "퀵핵: 과열";
        public string Description { get; set; } = "피가 끓어오르는 느낌이 착각이 아닐 때도 있습니다";
        public int BonusDamage { get; set; } = 10 + gameManager.GameData.Player.Dex;
        public int SkillCost { get; set; } = 10;
        public string SkillType { get; set; } = "Cybo";
        public int StrNeeded { get; set; } = 1;
        public int DexNeeded { get; set; } = 3;
    }
    public class ShortCircuit(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "퀵핵: 합선";
        public string Description { get; set; } = "피카츄 두 마리를 직렬연결한 수준의 전압입니다.";
        public int BonusDamage { get; set; } = 10 + gameManager.GameData.Player.Dex;
        public int SkillCost { get; set; } = 10;
        public string SkillType { get; set; } = "Robo";
        public int StrNeeded { get; set; } = 1;
        public int DexNeeded { get; set; } = 3;
    }
    public class Cyberpsychosis(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "퀵핵: 사이버 사이코시스";
        public string Description { get; set; } = "사이버 사이코 현상은 이제 신문의 단골소재입니다.";
        public int BonusDamage { get; set; } = 20 + gameManager.GameData.Player.Dex;
        public int SkillCost { get; set; } = 20;
        public string SkillType { get; set; } = "Robo";
        public int StrNeeded { get; set; } = 3;
        public int DexNeeded { get; set; } = 7;
    }
    public class Suicide(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "퀵핵: 자살";
        public string Description { get; set; } = "'엠마리쿠스'란 존재가 만든 프로그램이라고 합니다.";
        public int BonusDamage { get; set; } = 20 + gameManager.GameData.Player.Dex;
        public int SkillCost { get; set; } = 20;
        public string SkillType { get; set; } = "Cybo";
        public int StrNeeded { get; set; } = 3;
        public int DexNeeded { get; set; } = 7;
    }
    public class SystemCollapse(GameManager gameManager) : ISKill
    {
        public string SkillName { get; set; } = "퀵핵: 시스템 붕괴";
        public string Description { get; set; } = "넷러너들의 추적을 뿌리치는 데도 유용합니다.";
        public int BonusDamage { get; set; } = 20 + gameManager.GameData.Player.Dex;
        public int SkillCost { get; set; } = 20;
        public string SkillType { get; set; } = "Cybo";
        public int StrNeeded { get; set; } = 3;
        public int DexNeeded { get; set; } = 7;
    }
}
