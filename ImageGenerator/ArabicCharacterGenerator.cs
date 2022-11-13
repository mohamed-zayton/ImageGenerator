namespace ImageGenerator;

internal static class ArabicCharacterGenerator
{
    private readonly static Random s_random = new();
    private readonly static char[] s_arabicCharacters = new[]
    {
        'ا', 'أ', 'إ', 'ب', 'ت', 'ث' , 'ج' ,'ح','خ', 'د', 'ذ' , 'ر' ,'ز','س', 'ش', 'ص' , 'ض' ,'ط','ظ', 'ع', 'غ' , 'ف' ,'ق','ك', 'ل', 'م' , 'ن' ,'ه','و', 'ي', 'ى'
    };

    private static bool s_previousWasSpace = true;

    public static char Generate()
    {
        // We don't want to generate two consecutive spaces. So we track whether the last generated char was a space or not.
        if (!s_previousWasSpace)
        {
            var spaceVersusLetterIndicator = s_random.NextDouble();
            // spaceVersusLetterIndicator takes a value from 0 (inclusive) to 1 (exclusive) with uniform distribution.
            // The probability of the value to be <= 0.2 is exactly 0.2.
            // So, we generate a space with probability = 0.2, and a letter with 0.8 probability.
            if (spaceVersusLetterIndicator <= 0.2)
            {
                s_previousWasSpace = true;
                return ' ';
            }
        }

        s_previousWasSpace = false;
        var randomLetterIndex = s_random.Next(s_arabicCharacters.Length);
        return s_arabicCharacters[randomLetterIndex];
    }
}
