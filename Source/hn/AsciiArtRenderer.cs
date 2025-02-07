using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TextRPG.Source
{
    public class AsciiArtRenderer
    {
        private static readonly string AsciiChars = "@%#*+=-:. "; // 밝기별 ASCII 문자
        private static int artHeight;

        public static string ConvertBmpToAscii(string imagePath, int width)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine("파일을 찾을 수 없습니다.");
                return string.Empty;
            }

            byte[] bmpBytes = File.ReadAllBytes(imagePath); // 이제 파일 바이트로 다 읽어옴
            int offset = BitConverter.ToInt32(bmpBytes, 10); // 픽셀 데이터 시작위치
            int imgWidth = BitConverter.ToInt32(bmpBytes, 18); // 너비
            int imgHeight = BitConverter.ToInt32(bmpBytes, 22); // 높이 
            bool isFlipped = imgHeight > 0; // 음수면 방향 반전됨 미리 bool 저장
            imgHeight = Math.Abs(imgHeight); // 절대값으로 변환

            short bitDepth = BitConverter.ToInt16(bmpBytes, 28); // 비트 뎊스

            int bytesPerPixel = bitDepth / 8; // 24비트 -> 3바이트, 32비트 -> 4바이트
            int rowSize = (int)Math.Ceiling((imgWidth * bytesPerPixel) / 4.0) * 4; // 24비트는 4바이트 패딩이 필요함 32비트는 필요없음
            int height = (int)(imgHeight / (double)imgWidth * width * 0.5); // 비율 유지
            artHeight = height; // 높이 저장해서 ui에 쓰려고 <<<<<< 딴데 쓰려면 이거 빼야됨
            StringBuilder asciiArt = new StringBuilder();

            for (int y = 0; y < height; y++)
            {
                int row = isFlipped ? (imgHeight - 1 - (y * imgHeight / height)) : (y * imgHeight / height);    // 행 위치 찾기

                for (int curX = 0; curX < width; curX++)
                {
                    int col = curX * imgWidth / width;     // 열 = (x위치) * 원본 이미지 너비 / 출력할 아스키 아트 너비
                    int pixelIndex = offset + (row * rowSize) + (col * bytesPerPixel);  // 해당 픽셀 위치 찾기 = 픽셀 데이터 시작 위치 + ( 현재 행 위치 * 한 행 크기) + (현재 열 위치 * 픽셀 크기)

                    byte blue = bmpBytes[pixelIndex];   // bmp는 bgr 순서로 저장됨
                    byte green = bmpBytes[pixelIndex + 1];
                    byte red = bmpBytes[pixelIndex + 2];

                    if (bitDepth == 32)     // 32비트는 알파값 있으니까
                    {
                        byte alpha = bmpBytes[pixelIndex + 3];

                        if (alpha == 0) // 완전 투명한 경우
                        {
                            asciiArt.Append(" "); // 공백 출력
                            continue;
                        }
                    }

                    int grayValue = (red + green + blue) / 3; // 그레이스케일 변환 = 흑백으로 변환해야됨
                    int index = grayValue * (AsciiChars.Length - 1) / 255;  // 255단계의 밝기를 10단계(0~8)"@%#*+=-:. "로 줄임
                    asciiArt.Append(AsciiChars[index]);
                }
                asciiArt.Append("\n");
            }

            return asciiArt.ToString();
        }

        public static void PrintAsciiArt(int x, int y, string ascii)
        {
            string[] lines = ascii.Split('\n');
            y = Math.Max(y, 0);
            for (int i = 0; i < lines.Length; i++)
            {
                int lineX = Math.Max((Console.WindowWidth - lines[i].Length) / 2, 0);
                int lineY = y + i;

                if (lineY >= Console.WindowHeight) break;
                Console.SetCursorPosition(lineX, lineY);
                Console.WriteLine(lines[i]);
            }
        }

        public static void DrawMenu(int x, int y, int width, int height)
        {
            string horizontalLine = new string('─', width);

            // 상단 테두리
            PrintAtPosition(x, y, "│" + horizontalLine + "│");

            // 메뉴 제목
            PrintAtPosition(x, y + 1, "│" + CenterText("[ 메인 메뉴 ]", width) + "│");

            // 중간 구분선 -> 걍 없는게 낫다
            PrintAtPosition(x, y + 2, "│" + " " + "│");

            // 메뉴 옵션
            string[] menuOptions = { "1. 새로 시작하기", "2. 이어서 하기", "3. 크레딧", "4. 종료" };
            for (int i = 0; i < menuOptions.Length; i++)
            {
                PrintAtPosition(x, y + 2 + i, "│" + CenterText(menuOptions[i], width) + "│");
            }

            // 하단 테두리
            PrintAtPosition(x, y + height - 1, "│" + horizontalLine + "│");
        }
        public static void PrintAtPosition(int x, int y, string text)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(text);
            }
            catch (ArgumentOutOfRangeException)
            {
                //ㄴㄴ 초과해도 걍 그리셈
            }
        }

        private static string CenterText(string text, int width)
        {
            int textWidth = text.Sum(t => (t >= '\uAC00' && t <= '\uD7A3') ? 2 : 1);
            int padding = (width - textWidth) / 2;
            if (padding < 0) padding = 0;

            int extraPadding = (width - textWidth) % 2;
            return new string(' ', padding) + text + new string(' ', padding + extraPadding);
        }
    }
}
