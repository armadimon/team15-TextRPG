using _15TextRPG.Source.State;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.Combat
{


    public class JHNCombat
    {


        private int combatCount = 5;
        private int revealedLetters = 0; // 플레이어가 알아낸 적의 글자 수
        private HashSet<int> revealedIndex = new HashSet<int>(); // 공개된 글자들 위치 저장

        public JHNCombat()
        {

        }

        public void ScanEnemy(NPC enemy)
        {
            // 이미 모든 정보를 얻었다면 추가 스캔 불가
            if (revealedLetters >= enemy.Name.Length)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|  이미 모든 정보 알아냄!         |");
                Console.WriteLine("|  더이상 얻을 정보가 없다        |");
                Console.WriteLine("+---------------------------------+\n");
                Console.ResetColor();
                Console.WriteLine("이미 모든 정보를 알아냈습니다: " + enemy.Name);
                Console.WriteLine("아무 키나 입력해주세요");
                Console.ReadKey();
                return;
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine("|  적 탐지됨!                     |");
            Console.WriteLine("|  암호 시스템에 접근 중...       |");
            Console.WriteLine("+---------------------------------+\n");
            Console.ResetColor();


            Console.Write("\n적의 시스템을 스캔합니다...[");
            Console.ForegroundColor = ConsoleColor.Green;

            int scanTime = 1; // 스캔 시간
            int barLength = 10; // 게이지 길이
            for (int i = 0; i <= barLength; i++)
            {

                Console.Write("■"); // 게이지 증가
                Thread.Sleep(scanTime * 100);
            }
            Console.ResetColor();
            Console.WriteLine("]\n완료!");

            Console.Write($"\n적의 보안 설명:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{enemy.Desc}");

            Console.Write($"{combatCount}");
            Console.ResetColor();
            Console.WriteLine($"초 내에 정확히 입력하세요");


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.Write("\n보안 키 입력: ");
            string playerInput = ReadInputWithTimeout(combatCount); // combatCount동안 입력 받기


            if (string.IsNullOrWhiteSpace(playerInput))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|  스캔 실패!                     |");
                Console.WriteLine("|  시스템에서 빠져나오는중...     |");
                Console.WriteLine("+---------------------------------+\n");
                Console.ResetColor();

                Console.WriteLine("\n시간을 초과했습니다! 스캔 실패...");
                Console.WriteLine("아무 키나 입력해주세요");
                Console.ReadKey();
                return;
            }

            // 입력 정확도를 계산
            double accuracy = CalculateTextAccuracy(playerInput, enemy.Desc);

            // 정확도에 비례하여 적의 이름 공개량 증가
            int revealedAmount = (int)(accuracy * enemy.Name.Length);
            if (revealedAmount < 1) revealedAmount = 1; // 최소 1글자 공개

            revealedLetters += revealedAmount;
            if (revealedLetters > enemy.Name.Length)
                revealedLetters = enemy.Name.Length; // 초과 방지

            // revealedAmount만큼 적 이름 공개
            RevealRandomName(revealedAmount, enemy);

            Console.WriteLine($"\n입력 정확도: ");
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"{accuracy * 100:F1}%");
            Console.ResetColor();

            if (accuracy > 0.6)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n정보 확보 완료!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n불완전한 정보를 확보");
                Console.ResetColor();
            }
            enemy.RevealedName = GetRevealedEnemyName(enemy);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{GetRevealedEnemyName(enemy)}");
            Console.ResetColor();

            Console.WriteLine("\n엔터를 눌러 정보를 저장합니다.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine("|  정보를 저장합니다...           |");
            Console.WriteLine("|  시스템에서 빠져나오는중...     |");
            Console.WriteLine("+---------------------------------+\n");
            Console.ResetColor();

            Console.WriteLine("아무 키나 입력해주세요");
            Console.ReadKey();

        }

        public void Hack(Player player, ChapterData chap, NPC enemy)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("+--------------------------------+");
            Console.WriteLine("|  해킹 시스템 접근 중...        |");
            Console.WriteLine("|  적의 보안 시스템 분석 중...   |");
            Console.WriteLine("+--------------------------------+\n");
            Console.ResetColor();

            int scanTime = 1; // 스캔 시간
            int barLength = 10; // 게이지 길이

            Console.Write("\n해킹을 시도합니다...[");
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i <= barLength; i++)
            {

                Console.Write("■"); // 게이지 증가
                Thread.Sleep(scanTime * 100);
            }
            Console.ResetColor();
            Console.Write("]");

            Console.Write("\n적의 이름을 입력하세요: ");
            string playerInput = ReadInputWithTimeout(5); // 5초 제한

            // 적의 이름과 유사도를 계산
            double accuracy = CalculateNameSimilarity(playerInput, enemy.Name);

            // 정확하게 맞췄다면 즉시 처치
            if (accuracy == 1.0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|  해킹 성공!                     |");
                Console.WriteLine("|  적의 보안을 완전히 무너뜨렸다  |");
                Console.WriteLine("+---------------------------------+\n");
                Console.ResetColor();
                Console.WriteLine($"\n{player.Name}이(가) {enemy.Name}의 보안을 완전히 무너뜨렸습니다! 적이 즉시 무력화되었습니다!");
                enemy.Health = 0;
                // npc의 isHack을 true로 변경
                enemy.IsHacked = true;
                enemy.RevealedName = enemy.Name;
            }
            else
            {
                int totalDamage = CalculateDamage(player.AttackDamage, enemy.DefensePoint, accuracy);


                enemy.Health -= totalDamage;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|  해킹 실패...                   |");
                Console.WriteLine("|  적의 보안에 타격을 주었다      |");
                Console.WriteLine("+---------------------------------+\n");
                Console.ResetColor();
                Console.WriteLine($"\n{player.Name}이(가) {GetRevealedEnemyName(enemy)}의 시스템을 해킹하여 {totalDamage}만큼 해킹 데미지를 주었습니다!");
                Console.WriteLine("적의 보안이 약해지고 있습니다...");
            }

            Console.WriteLine("아무 키나 입력해주세요");
            Console.ReadKey();

            if (enemy.Health > 0)
                DefendHack(player, enemy);
        }

        private double CalculateNameSimilarity(string input, string enemyName)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0.0; // 입력x -> 유사도 0%

            input = input.ToUpper();
            enemyName = enemyName.ToUpper();

            int matchCount = 0;
            int minLength = Math.Min(input.Length, enemyName.Length);

            // 같은 위치에 있는 글자가 몇 개나 맞았는지 비교
            for (int i = 0; i < minLength; i++)
            {
                if (input[i] == enemyName[i])
                    matchCount++;
            }
            return (double)matchCount / enemyName.Length; // 정확도 비율 반환 -> 0~1
        }

        public void DefendHack(Player player, NPC enemy)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("+---------------------------------+");
            Console.WriteLine("|  방화벽에 적 감지됨!            |");
            Console.WriteLine("|  적이 방화벽에 침투했다         |");
            Console.WriteLine("+---------------------------------+\n");
            Console.ResetColor();

            // 적의 랜덤 방향 리스트 생성
            List<char> enemyText = GenerateRandomText(5);
            Console.WriteLine($"적이 해킹 공격을 시도합니다. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{string.Join(" -> ", enemyText)}");
            Console.ResetColor();

            Console.WriteLine("방어하세요: ");

            string playerInput = ReadInputWithTimeout(5); // 5초 제한
            double accuracy = CalculateNameSimilarity(playerInput, new string(enemyText.ToArray()));

            // 정확도 비례 데미지 계산
            int totalDamage = CalculateDamage(enemy.DefensePoint, player.AttackDamage, accuracy);
            Console.WriteLine($"\n방어 정확도: {accuracy * 100:F1}%");

            player.Health -= totalDamage;
            Console.Write($"{GetRevealedEnemyName(enemy)}이(가) {player.Name}에게 ");
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.Write($"{totalDamage}");
            Console.ResetColor(); 
            Console.WriteLine("만큼 데미지를 주었습니다");


            // 플레이어 체력 확인
            if (player.Health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("+---------------------------------+");
                Console.WriteLine("|  방어 실패!                     |");
                Console.WriteLine("|  적이 시스템 내부에 침투했다    |");
                Console.WriteLine("+---------------------------------+\n");
                Console.ResetColor();

                Console.WriteLine("\n방어 실패! 플레이어가 쓰러집니다...");
                Console.WriteLine("아무 키나 입력하면 메인 화면으로 돌아갑니다.");
                Console.ReadKey();

                // 메인 메뉴로 이동
                //GameManager.Instance.ChangeState(new MainMenuState());

            }

            Console.WriteLine("아무키나 입력해주세요");
            Console.ReadKey();


        }

        private List<char> GenerateRandomText(int count)
        {
            Random random = new Random();
            List<char> selectedChar = new List<char>();

            for (int i = 0; i < count; i++)
            {
                int choice = random.Next(26);
                selectedChar.Add((char)('a' + choice)); // 'a'부터 'z'까지 선택
            }

            return selectedChar;
        }
    
        private string ReadInputWithTimeout(int timeoutSeconds)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string input = "";
            int remainingTime = timeoutSeconds;

            int barLength = timeoutSeconds; // 타이머 게이지 길이

            Console.WriteLine("\n사용자 입력 대기 중...");
            Console.Write("타이머: ["); 
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < barLength; i++)
                Console.Write("■");
            Console.ResetColor();
            Console.WriteLine("]"); // 게이지 바 끝

            int timerCurTop = Console.CursorTop - 1; // 타이머 바 
      
            Console.Write("\n사용자 입력: ");
            int playerCurTop = Console.CursorTop; // 입력 줄 저장
            int playerCurLeft = Console.CursorLeft; // 입력 위치 저장

            while (stopwatch.Elapsed.TotalSeconds < timeoutSeconds)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (input.Length > 0) // 빈 상태에서 백스페이스를 누르면 아무 동작 안 함
                        {
                            input = input.Substring(0, input.Length - 1);

                            // 커서가 0보다 크면 이동 가능
                            if (Console.CursorLeft > 0)
                            {
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);   //왼쪽 한 칸 옮기고
                                Console.Write(" "); // 삭제 하고
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);   // 다시 옮겨서 입력 위치 조정
                            }
                        }
                    }
                    else
                    {
                        input += key.KeyChar;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(key.KeyChar);
                        Console.ResetColor();
                    }

                    playerCurTop = Console.CursorTop;
                    playerCurLeft = Console.CursorLeft;

                }

                // 1초마다 카운트다운 갱신
                int newRemainingTime = timeoutSeconds - (int)stopwatch.Elapsed.TotalSeconds;
                if (newRemainingTime < remainingTime)
                {
                    remainingTime = newRemainingTime;

                    // 타이머 게이지 색상 변화 (시간이 줄어들수록 경고)
                    Console.SetCursorPosition(0, timerCurTop);
                    Console.Write("타이머: [");

                    for (int i = 0; i < barLength; i++)
                    {
                        if (i < (remainingTime * barLength / timeoutSeconds))
                        {
                            if (remainingTime > 3)
                                Console.ForegroundColor = ConsoleColor.Green;
                            else if (remainingTime > 1)
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            else
                                Console.ForegroundColor = ConsoleColor.Red;

                            Console.Write("■");
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.Write("□");
                        }
                    }
                    Console.ResetColor();
                    Console.WriteLine("]");

                    // 타이머 갱신 후, 플레이어의 마지막 입력 위치 복구
                    Console.SetCursorPosition(playerCurLeft, playerCurTop);
                }
            }
            stopwatch.Stop();
            return input;
        }

        private double CalculateTextAccuracy(string input, string target)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0.0;

            input = input.ToLower();
            target = target.ToLower();

            int matchCount = 0;
            int minLength = Math.Min(input.Length, target.Length);

            for (int i = 0; i < minLength; i++)
            {
                if (input[i] == target[i])
                    matchCount++;
            }

            return (double)matchCount / target.Length; // 정확도 비율 (0.0 ~ 1.0)
        }

        private void RevealRandomName(int count, NPC enemy)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = random.Next(enemy.Name.Length); 
                    if (revealedIndex.Count == enemy.Name.Length)
                        break;
                }
                while (revealedIndex.Contains(randomIndex));

                revealedIndex.Add(randomIndex);
            }
        }

        public void Run()
        {
            Console.WriteLine("도망쳤수");
            Console.WriteLine("아무키나 입력해주세요");
            Console.ReadKey();
        }

        private void EnemyTurn(Player player, NPC enemy)
        {
            //기본 데미지 계산(공격력과 방어력의 비율 적용)
            double attackDefenseRatio = (double)player.AttackDamage / enemy.DefensePoint;

            int baseDamage = (int)player.AttackDamage;
            baseDamage = (int)(baseDamage * attackDefenseRatio);

            if (baseDamage < 0) baseDamage = 0; // 최소 데미지 보장

            player.Health -= baseDamage;
            Console.Write($"{GetRevealedEnemyName(enemy)}이(가) {player.Name}의 시스템을 해킹하여 ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{baseDamage}");
            Console.ResetColor();
            Console.WriteLine(" 만큼 해킹 데미지를 주었습니다");

            // 플레이어 체력 확인
            if (player.Health <= 0)
            {
                Console.WriteLine("\n해킹 실패! 플레이어의 시스템이 다운됩니다...");
                Console.WriteLine("아무 키나 입력하면 메인 화면으로 돌아갑니다.");
                Console.ReadKey();

                // 메인 메뉴로 이동
                //GameManager.Instance.ChangeState(new MainMenuState());

            }

            Console.WriteLine("아무키나 입력해주세요");
            Console.ReadKey();

        }

        public string GetRevealedEnemyName(NPC enemy)
        {
            if (revealedLetters >= enemy.Name.Length)
                return enemy.Name; // 모든 문자가 공개됨

            char[] maskedName = new string('*', enemy.Name.Length).ToCharArray();

            // 이미 공개된 위치만 반영
            foreach (int index in revealedIndex)
            {
                maskedName[index] = enemy.Name[index];
            }

            return new string(maskedName);
        }
        public int CalculateDamage(double attackerPower, double defenderDefense, double accuracy)
        {
            // 방어력 비례 데미지 감소 적용
            double attackDefenseRatio = attackerPower / defenderDefense;
            int baseDamage = (int)(attackerPower * attackDefenseRatio);

            // 정확도에 따른 최종 데미지 적용
            int totalDamage = (int)(baseDamage * accuracy);

            // 최소 1 데미지 보장
            return totalDamage < 1 ? 1 : totalDamage;
        }
    }
}
