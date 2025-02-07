using _15TextRPG.Source.State;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public class Combat
    {
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }

        private int combatCount = 5;
        private int revealedLetters = 0; // 플레이어가 알아낸 적의 글자 수
        private HashSet<int> revealedIndex = new HashSet<int>(); // 공개된 글자들 위치 저장

        public Combat(Player player, Enemy enemy)
        {
            Player = player;
            Enemy = enemy;
        }

        public void Hack()
        {
            Console.Write("\n해킹을 시도합니다! 적의 이름을 입력하세요: ");
            string playerInput = Console.ReadLine() ?? "";

            // 적의 이름과 유사도를 계산
            double similarity = CalculateNameSimilarity(playerInput, Enemy.Name);

            // 정확하게 맞췄다면 즉시 처치
            if (similarity == 1.0)
            {
                Console.WriteLine($"{Player.Name}이(가) {Enemy.Name}의 보안을 완전히 무너뜨렸습니다! 적이 즉시 무력화되었습니다!");
                Enemy.Health = 0;
            }
            else
            {
                //기본 데미지 계산(공격력과 방어력의 비율 적용)
                double attackDefenseRatio = (double)Player.AttackDamage / Enemy.DefensePoint;

                int baseDamage = (int)Player.AttackDamage;
                baseDamage = (int)(baseDamage * attackDefenseRatio);



                int additionalDamage = (int)(baseDamage * similarity);   // 유사도에 비례하여 데미지 증가

                int totalDamage = baseDamage + additionalDamage;
                if (totalDamage < 1) totalDamage = 1; // 최소 데미지 보장

                Enemy.Health -= totalDamage;
                Console.WriteLine($"{Player.Name}이(가) {GetRevealedEnemyName()}의 시스템을 해킹하여 {totalDamage}만큼 해킹 데미지를 주었습니다!");
                Console.WriteLine("적의 보안이 약해지고 있습니다...");
            }

            Console.WriteLine("아무 키나 입력해주세요");
            Console.ReadKey();

            if (Enemy.Health > 0)
                EnemyTurn();
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

            Console.WriteLine((double)matchCount / enemyName.Length);
            return (double)matchCount / enemyName.Length; // 정확도 비율 반환 -> 0~1
        }


        public void Defend()
        {
            Console.WriteLine("플레이어 방어 구현");
            EnemyTurn();
        }

        public void ScanEnemy()
        {
            // 이미 모든 정보를 얻었다면 추가 스캔 불가
            if (revealedLetters >= Enemy.Name.Length)
            {
                Console.WriteLine("이미 모든 정보를 알아냈습니다: " + Enemy.Name);
                Console.WriteLine("아무 키나 입력해주세요");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n적의 시스템을 스캔합니다...");
            Console.WriteLine($"적의 보안 설명: \"{Enemy.Description}\"");
            Console.WriteLine($"{combatCount}초 내에 정확히 입력하세요");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.Write("\n보안 키 입력: ");
            string playerInput = ReadInputWithTimeout(combatCount); // combatCount동안 입력 받기


            if (string.IsNullOrWhiteSpace(playerInput))
            {
                Console.WriteLine("\n시간을 초과했습니다! 스캔 실패...");
                Console.WriteLine("아무 키나 입력해주세요");
                Console.ReadKey();
                return;
            }

            // 입력 정확도를 계산
            double accuracy = CalculateTextAccuracy(playerInput, Enemy.Description);

            // 정확도에 비례하여 적의 이름 공개량 증가
            int revealedAmount = (int)(accuracy * Enemy.Name.Length);
            if (revealedAmount < 1) revealedAmount = 1; // 최소 1글자 공개

            revealedLetters += revealedAmount;
            if (revealedLetters > Enemy.Name.Length)
                revealedLetters = Enemy.Name.Length; // 초과 방지

            // revealedAmount만큼 적 이름 공개
            RevealRandomName(revealedAmount);

            Console.WriteLine($"\n입력 정확도: {accuracy * 100:F1}%");
            Console.WriteLine($"일부 정보를 얻었습니다: {GetRevealedEnemyName()}");
            Console.WriteLine("아무 키나 입력해주세요");
            Console.ReadKey();
        }

        private string ReadInputWithTimeout(int timeoutSeconds)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string input = "";
            int remainingTime = timeoutSeconds;
            Console.WriteLine("");
            int timerCurTop = Console.CursorTop;      //타이머 커서 저장
            
            Console.WriteLine($"{remainingTime}초 남음..."); 
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
                        Console.Write(key.KeyChar);
                    }

                    playerCurTop = Console.CursorTop;  
                    playerCurLeft = Console.CursorLeft; 

                }

                // 1초마다 카운트다운 갱신
                int newRemainingTime = timeoutSeconds - (int)stopwatch.Elapsed.TotalSeconds;
                if (newRemainingTime < remainingTime) // 시간이 줄어들었을 때만 갱신
                {
                    remainingTime = newRemainingTime;
                    Console.SetCursorPosition(0, timerCurTop); // 타이머 줄로 이동
                    Console.WriteLine($"{remainingTime}초 남음...  "); // 기존 글자 덮어써야됨

                    // 타이머 갱신 후, 플레이어의 마지막 입력 위치 복구
                    Console.SetCursorPosition(playerCurLeft, playerCurTop);
                }

                
            }

            stopwatch.Stop();
            Console.WriteLine();
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

        private void RevealRandomName(int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = random.Next(Enemy.Name.Length);
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

        private void EnemyTurn()
        {
            //기본 데미지 계산(공격력과 방어력의 비율 적용)
            double attackDefenseRatio = (double)Player.AttackDamage / Enemy.DefensePoint;

            int baseDamage = (int)Player.AttackDamage;
            baseDamage = (int)(baseDamage * attackDefenseRatio);

            if (baseDamage < 0) baseDamage = 0; // 최소 데미지 보장

            Player.Health -= baseDamage;
            Console.WriteLine($"{GetRevealedEnemyName()}이(가) {Player.Name}의 시스템을 해킹하여 {baseDamage}만큼 해킹 데미지를 주었습니다");

            // 플레이어 체력 확인
            if (Player.Health <= 0)
            {
                Console.WriteLine("\n해킹 실패! 플레이어의 시스템이 다운됩니다...");
                Console.WriteLine("아무 키나 입력하면 메인 화면으로 돌아갑니다.");
                Console.ReadKey();

                // 메인 메뉴로 이동
                GameManager.Instance.ChangeState(new MainMenuState());
                return;
            }

            Console.WriteLine("아무키나 입력해주세요");
            Console.ReadKey();
        }

        public string GetRevealedEnemyName()
        {
            if (revealedLetters >= Enemy.Name.Length)
                return Enemy.Name; // 모든 문자가 공개됨

            char[] maskedName = new string('*', Enemy.Name.Length).ToCharArray();

            // 이미 공개된 위치만 반영
            foreach (int index in revealedIndex)
            {
                maskedName[index] = Enemy.Name[index];
            }

            return new string(maskedName);
        }

    }
}
