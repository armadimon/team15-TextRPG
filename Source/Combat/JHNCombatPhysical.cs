using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.Combat
{
    public class JHNCombatPhysical
    {
        public JHNCombat combat;
        public JHNCombatPhysical() { combat = new JHNCombat(); }
        //===================================================어택

        public void Attack(Player player, NPC enemy)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n================== 공격 시작 ==================");
            Console.ResetColor();
            Console.WriteLine("방향키를 5개 입력하세요!\n");

            // 적의 랜덤 방향 리스트 생성
            List<char> enemyDirections = GenerateRandomDirections(5);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("+-----------------------------------+");
            Console.Write("| 적이 선택한 방향: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(string.Join(" ", enemyDirections));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" |");
            Console.WriteLine("+-----------------------------------+");
            Console.ResetColor();

            // 플레이어 입력 리스트 생성 (5초 제한)
            List<char> playerInputs = GetPlayerAttackInput(5);

            // 정확도 비례 데미지 계산
            double accuracy = CalculateAttackAccuracy(playerInputs, enemyDirections, 5);
            int totalDamage = combat.CalculateDamage(player.AttackDamage, enemy.DefensePoint, accuracy);
            enemy.Health -= totalDamage;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=============== 공격 결과 ===============");
            Console.ResetColor();
            Console.WriteLine($"\n공격 정확도: {accuracy * 100:F1}%");
            Console.ResetColor();

            Console.Write($"{player.Name}이(가) ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(combat.GetRevealedEnemyName(enemy));
            Console.ResetColor();
            Console.Write("에게 ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{totalDamage}");
            Console.ResetColor();
            Console.WriteLine(" 의 피해를 입혔습니다!");

            Console.WriteLine("아무 키나 입력해주세요.");
            Console.ReadKey();
            DefendAttack(player, enemy);
        }

        private double CalculateAttackAccuracy(List<char> input, List<char> target, int maxCnt)
        {
            // 5개 미만 입력시 0 넣어줌
            while (input.Count < maxCnt)
            {
                input.Add('0');
            }

            // 정확도 계산 (일치하는 개수)
            int matchCount = 0;
            for (int i = 0; i < maxCnt; i++)
            {
                if (input[i] == target[i])
                    matchCount++;
            }

            double accuracy = (double)matchCount / maxCnt;

            return accuracy;
        }

        private List<char> GenerateRandomDirections(int count)
        {
            char[] possibleDirections = { '↑', '↓', '←', '→' };
            Random random = new Random();
            List<char> selectedDirections = new List<char>();

            for (int i = 0; i < count; i++)
            {
                selectedDirections.Add(possibleDirections[random.Next(possibleDirections.Length)]);
            }

            return selectedDirections;
        }

        private List<char> GetPlayerAttackInput(int count)
        {
            List<char> inputs = new List<char>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int timerBarLength = count; // 타이머 게이지 바 길이
            int remainingTime = count; // 총 5초 제한

            Console.WriteLine($"\n공격 대기 중... ({count}번 입력하세요)");

            // 타이머 UI 출력
            Console.Write("타이머: [");
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < timerBarLength; i++)
                Console.Write("■");
            Console.ResetColor();
            Console.WriteLine("]");

            int timerCurTop = Console.CursorTop - 1;  // 타이머 줄 위치 저장
            Console.Write("\n방향키 입력: ");
            int inputCurLeft = Console.CursorLeft;
            int inputCurTop = Console.CursorTop;

            while (stopwatch.Elapsed.TotalSeconds < count && inputs.Count < count)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow) inputs.Add('↑');
                    else if (key == ConsoleKey.DownArrow) inputs.Add('↓');
                    else if (key == ConsoleKey.LeftArrow) inputs.Add('←');
                    else if (key == ConsoleKey.RightArrow) inputs.Add('→');

                    // 입력된 값 표시 (커서를 뒤로 돌려 기존 값 덮어쓰기)
                    Console.SetCursorPosition(0, inputCurTop);
                    Console.ResetColor();
                    Console.Write("방향키 입력: ");
                    Console.ForegroundColor = ConsoleColor.Blue; // 입력 값만 파란색
                    Console.Write(string.Join(" ", inputs));
                    Console.ResetColor(); // 색상 초기화
                    Console.Write("     "); // 기존 값 덮어쓰기 위한 공백 추가
                }

                // 1초마다 카운트다운 갱신
                int newRemainingTime = count - (int)stopwatch.Elapsed.TotalSeconds;
                if (newRemainingTime < remainingTime)
                {
                    remainingTime = newRemainingTime;

                    Console.SetCursorPosition(0, timerCurTop);
                    Console.Write("타이머: [");

                    for (int i = 0; i < timerBarLength; i++)
                    {
                        if (i < (remainingTime * timerBarLength / count))
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

                    Console.SetCursorPosition(0, inputCurTop); // 입력 위치 복구
                }
            }

            stopwatch.Stop();
            Console.WriteLine();

            return inputs;
        }


        public void DefendAttack(Player player, NPC enemy)
        {
            Console.WriteLine("플레이어 방어 구현");

            // 적의 랜덤 방향 리스트 생성
            List<char> enemyDirections = GenerateRandomDirections(5);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("+-----------------------------------+");
            Console.Write("| 적이 선택한 방향: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(string.Join(" ", enemyDirections));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" |");
            Console.WriteLine("+-----------------------------------+");
            Console.ResetColor();

            // 플레이어 입력 리스트 생성 (5초 제한)
            List<char> playerInputs = GetPlayerAttackInput(5);

            // 정확도 비례 데미지 계산
            double accuracy = CalculateAttackAccuracy(playerInputs, enemyDirections, 5);
            int totalDamage = combat.CalculateDamage(enemy.AttackDamage, player.DefensePoint, accuracy);
            player.Health -= totalDamage;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=============== 공격 결과 ===============");
            Console.ResetColor();

            Console.Write($"\n방어 정확도: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{accuracy * 100:F1}%");
            Console.ResetColor();


            Console.Write($"{player.Name}이(가) ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(combat.GetRevealedEnemyName(enemy));
            Console.ResetColor();
            Console.Write("에게 ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{totalDamage}");
            Console.ResetColor();
            Console.WriteLine(" 의 피해를 입혔습니다!");

            // 플레이어 체력 확인
            if (player.Health <= 0)
            {
                Console.WriteLine("\n방어 실패! 플레이어가 쓰러집니다...");
                Console.WriteLine("아무 키나 입력하면 메인 화면으로 돌아갑니다.");
                Console.ReadKey();

                // 메인 메뉴로 이동
                //GameManager.Instance.ChangeState(new MainMenuState());

            }

            Console.WriteLine("아무키나 입력해주세요");
            Console.ReadKey();

        }
    }
}
