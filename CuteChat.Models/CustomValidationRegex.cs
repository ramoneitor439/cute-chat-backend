using System.Text.RegularExpressions;

namespace CuteChat.Models;
public static partial class CustomRegexPatterns
{
    public static Regex OnlyLetters => OnlyLettersRegex();
    public static Regex OnlyNumbers => OnlyNumbersRegex();

    [GeneratedRegex(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZáéíóúñÁÉÍÓÚÑ]*$", RegexOptions.Compiled)]
    private static partial Regex OnlyLettersRegex();
    [GeneratedRegex(@"^[0-9]+$", RegexOptions.Compiled)]
    private static partial Regex OnlyNumbersRegex();
}