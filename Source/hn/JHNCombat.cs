using _15TextRPG.Source.State;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source.hn
{


    public class JHNCombat
    {


        private int combatCount = 5;
        private int revealedLetters = 0; // 플레이어가 알아낸 적의 글자 수
        private HashSet<int> revealedIndex = new HashSet<int>(); // 공개된 글자들 위치 저장

        public JHNCombat()
        {

        }

        public void Hack(Player player, Enemy enemy)
        {
            Console.Write("\n해킹을 시도합니다! 적의 이름을 입력하세요: ");
            string playerInput = Console.ReadLine() ?? "";

            // 적의 이름과 유사도를 계산
            double accuracy = CalculateNameSimilarity(playerInput, enemy.Name);

            // 정확하게 맞췄다면 즉시 처치
            if (accuracy == 1.0)
            {
                Console.WriteLine($"{player.Name}이(가) {enemy.Name}의 보안을 완전히 무너뜨렸습니다! 적이 즉시 무력화되었습니다!");
                enemy.Health = 0;
            }
            else
            {
                int totalDamage = CalculateDamage(player.AttackDamage, enemy.DefensePoint, accuracy);


                enemy.Health -= totalDamage;
                Console.WriteLine($"{player.Name}이(가) {GetRevealedEnemyName(enemy)}의 시스템을 해킹하여 {totalDamage}만큼 해킹 데미지를 주었습니다!");
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

            Console.WriteLine((double)matchCount / enemyName.Length);
            return (double)matchCount / enemyName.Length; // 정확도 비율 반환 -> 0~1
        }

        public void DefendHack(Player player, Enemy enemy)
        {
            Console.WriteLine("플레이어 방어 구현");

            // 적의 랜덤 방향 리스트 생성
            List<char> enemyText = GenerateRandomText(5);
            Console.WriteLine($"(적이 해킹 공격을 시도합니다. {string.Join(" ", enemyText)})");
            Console.WriteLine("방어하세요: ");

            string playerInput = Console.ReadLine() ?? "";
            double accuracy = CalculateNameSimilarity(playerInput, new string(enemyText.ToArray()));

            // 정확도 비례 데미지 계산
            int totalDamage = CalculateDamage(enemy.DefensePoint, player.AttackDamage, accuracy);
            Console.WriteLine($"\n방어 정확도: {accuracy * 100:F1}%");

            player.Health -= totalDamage;
            Console.WriteLine($"{GetRevealedEnemyName(enemy)}이(가) {player.Name}에게 {totalDamage}만큼 데미지를 주었습니다");

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

        public void ScanEnemy(Enemy enemy)
        {
            // 이미 모든 정보를 얻었다면 추가 스캔 불가
            if (revealedLetters >= enemy.Name.Length)
            {
                Console.WriteLine("이미 모든 정보를 알아냈습니다: " + enemy.Name);
                Console.WriteLine("아무 키나 입력해주세요");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n적의 시스템을 스캔합니다...");
            Console.WriteLine($"적의 보안 설명: \"{enemy.Description}\"");
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
            double accuracy = CalculateTextAccuracy(playerInput, enemy.Description);

            // 정확도에 비례하여 적의 이름 공개량 증가
            int revealedAmount = (int)(accuracy * enemy.Name.Length);
            if (revealedAmount < 1) revealedAmount = 1; // 최소 1글자 공개

            revealedLetters += revealedAmount;
            if (revealedLetters > enemy.Name.Length)
                revealedLetters = enemy.Name.Length; // 초과 방지

            // revealedAmount만큼 적 이름 공개
            RevealRandomName(revealedAmount, enemy);

            Console.WriteLine($"\n입력 정확도: {accuracy * 100:F1}%");
            Console.WriteLine($"일부 정보를 얻었습니다: {GetRevealedEnemyName(enemy)}");
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

        private void RevealRandomName(int count, Enemy enemy)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = random.Next(enemy.Name.Length);
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

        private void EnemyTurn(Player player, Enemy enemy)
        {
            //기본 데미지 계산(공격력과 방어력의 비율 적용)
            double attackDefenseRatio = (double)player.AttackDamage / enemy.DefensePoint;

            int baseDamage = (int)player.AttackDamage;
            baseDamage = (int)(baseDamage * attackDefenseRatio);

            if (baseDamage < 0) baseDamage = 0; // 최소 데미지 보장

            player.Health -= baseDamage;
            Console.WriteLine($"{GetRevealedEnemyName(enemy)}이(가) {player.Name}의 시스템을 해킹하여 {baseDamage}만큼 해킹 데미지를 주었습니다");

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

        public string GetRevealedEnemyName(Enemy enemy)
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


        //===================================================어택

        public void Attack(Player player, Enemy enemy)
        {
            Console.WriteLine("\n공격을 시작합니다! 방향키를 5개 입력하세요!");

            // 적의 랜덤 방향 리스트 생성
            List<char> enemyDirections = GenerateRandomDirections(5);
            Console.WriteLine($"(적이 선택한 방향: {string.Join(" ", enemyDirections)})");

            // 플레이어 입력 리스트 생성 (5초 제한)
            List<char> playerInputs = GetPlayerAttackInput(5);

            // 정확도 비례 데미지 계산
            double accuracy = CalculateAttackAccuracy(playerInputs, enemyDirections);

            int totalDamage = CalculateDamage(player.AttackDamage, enemy.DefensePoint, accuracy);

            enemy.Health -= totalDamage;
            Console.WriteLine($"\n공격 정확도: {accuracy * 100:F1}%");
            Console.WriteLine($"{player.Name}이(가) {GetRevealedEnemyName(enemy)}에게 {totalDamage}의 피해를 입혔습니다!");

            Console.WriteLine("아무 키나 입력해주세요.");
            Console.ReadKey();
            DefendAttack(player, enemy);
        }

        private double CalculateAttackAccuracy(List<char> input, List<char> target)
        {
            // 5개 미만 입력시 0 넣어줌
            while (input.Count < 5)
            {
                input.Add('0');
            }

            // 정확도 계산 (일치하는 개수)
            int matchCount = 0;
            for (int i = 0; i < 5; i++)
            {
                if (input[i] == target[i])
                    matchCount++;
            }

            double accuracy = (double)matchCount / 5;

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

            Console.WriteLine("\n방향키 입력 중... (5초 안에 5번 입력)");

            while (stopwatch.Elapsed.TotalSeconds < 5 && inputs.Count < count)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow) inputs.Add('↑');
                    else if (key == ConsoleKey.DownArrow) inputs.Add('↓');
                    else if (key == ConsoleKey.LeftArrow) inputs.Add('←');
                    else if (key == ConsoleKey.RightArrow) inputs.Add('→');

                    // 현재 입력된 값 표시
                    Console.Write(string.Join(" ", inputs) + "\r");
                }
            }

            stopwatch.Stop();
            Console.WriteLine();

            return inputs;
        }


        public void DefendAttack(Player player, Enemy enemy)
        {
            Console.WriteLine("플레이어 방어 구현");

            // 적의 랜덤 방향 리스트 생성
            List<char> enemyDirections = GenerateRandomDirections(5);
            Console.WriteLine($"(적이 선택한 방향: {string.Join(" ", enemyDirections)})");

            // 플레이어 입력 리스트 생성 (5초 제한)
            List<char> playerInputs = GetPlayerAttackInput(5);

            // 정확도 비례 데미지 계산
            double accuracy = CalculateAttackAccuracy(playerInputs, enemyDirections);
            int totalDamage = CalculateDamage(enemy.DefensePoint, player.AttackDamage, accuracy);
            Console.WriteLine($"\n방어 정확도: {accuracy * 100:F1}%");

            player.Health -= totalDamage;
            Console.WriteLine($"{GetRevealedEnemyName(enemy)}이(가) {player.Name}에게 {totalDamage}만큼 데미지를 주었습니다");

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

        private int CalculateDamage(double attackerPower, double defenderDefense, double accuracy)
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
