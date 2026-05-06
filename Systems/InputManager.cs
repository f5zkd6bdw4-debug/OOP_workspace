namespace OOPRpg.Systems;

public class InputManager
{
    public bool TryReadInt(string? input, out int value)
    {
        return int.TryParse(input, out value);
    }

    public int ReadMenuNumber(string prompt)
    {
        while (true)
        {
            try
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (!TryReadInt(input, out int value))
                {
                    throw new FormatException("숫자만 입력할 수 있습니다.");
                }

                return value;
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("입력 검사를 완료했습니다.");
            }
        }
    }

    public string ReadRequiredText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("빈 문자열은 사용할 수 없습니다.");
        }
    }
}
