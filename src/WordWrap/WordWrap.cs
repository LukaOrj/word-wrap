 using System.Text;

namespace WordWrapLibrary;

public static class WordWrap
{
    private static readonly char[] WhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };

    public static string WrapUnFormatted(string text, int width)
    {
        var wrap = new StringBuilder();

        for (int i = 0; i < text.Length; i += width)
        {
            int remainingLength = Math.Min(width, text.Length - i);
            string chunk = text.Substring(i, remainingLength);

            wrap.Append(chunk);
            wrap.Append(Environment.NewLine);
        }

        return wrap.ToString();
    }

    public static string Wrap(string text, int width)
    {
        ValidateInput(text, width);

        if (text.Length == 0)
        {
            return string.Empty;
        }

        string[] words = ExtractWords(text);

        if (words.Length == 0)
        {
            return string.Empty;
        }

        return BuildWrappedText(words, width);
    }


    private static void ValidateInput(string text, int width)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text), "Input text cannot be null.");
        }

        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be a positive integer.");
        }
    }

    private static string[] ExtractWords(string text)
    {
        return text.Split(WhitespaceChars, StringSplitOptions.RemoveEmptyEntries);
    }

    private static string BuildWrappedText(string[] words, int width)
    {
        var wrappedTextBuilder = new StringBuilder();
        var currentLineBuilder = new StringBuilder();

        foreach (var word in words)
        {
            if (IsWordTooLong(word, width))
            {
                FlushCurrentLine(wrappedTextBuilder, currentLineBuilder);
                AppendLongWord(word, width, wrappedTextBuilder, currentLineBuilder);
            }
            else
            {
                if (!CanFitWordOnLine(word, width, currentLineBuilder))
                {
                    FlushCurrentLine(wrappedTextBuilder, currentLineBuilder);
                }

                AppendWordToLine(word, currentLineBuilder);
            }
        }

        AppendRemainingLine(wrappedTextBuilder, currentLineBuilder);

        return wrappedTextBuilder.ToString();
    }

    private static bool IsWordTooLong(string word, int width)
    {
        return word.Length > width;
    }

    private static bool CanFitWordOnLine(string word, int width, StringBuilder currentLineBuilder)
    {
        int lineLengthWithWord = currentLineBuilder.Length == 0
            ? word.Length 
            : currentLineBuilder.Length + 1 + word.Length;

        return lineLengthWithWord <= width;
    }

    private static void AppendWordToLine(string word, StringBuilder currentLineBuilder)
    {
        if (currentLineBuilder.Length > 0)
        {
            currentLineBuilder.Append(' ');
        }
        currentLineBuilder.Append(word);
    }

    private static void FlushCurrentLine(StringBuilder wrappedTextBuilder, StringBuilder currentLineBuilder)
    {
        if (currentLineBuilder.Length > 0)
        {
            wrappedTextBuilder.Append(currentLineBuilder.ToString());
            wrappedTextBuilder.Append(Environment.NewLine);
            currentLineBuilder.Clear();
        }
    }

    private static void AppendLongWord(string word, int width, StringBuilder wrappedTextBuilder, StringBuilder currentLineBuilder)
    {
        for (int i = 0; i < word.Length; i += width)
        {
            int remainingLength = Math.Min(width, word.Length - i);
            string chunk = word.Substring(i, remainingLength);
             
            if (i + width < word.Length)
            {
                wrappedTextBuilder.Append(chunk);
                wrappedTextBuilder.Append(Environment.NewLine);
            }
            else
            {
                currentLineBuilder.Append(chunk);
            }
        }
    }

    private static void AppendRemainingLine(StringBuilder wrappedTextBuilder, StringBuilder currentLineBuilder)
    {
        if (currentLineBuilder.Length > 0)
        {
            wrappedTextBuilder.Append(currentLineBuilder.ToString());
        }
    }
}