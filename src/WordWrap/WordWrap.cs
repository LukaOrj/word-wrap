using System.Text;

namespace WordWrapLibrary;

public static class WordWrap
{
    private static readonly char[] WhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };

    public static string WrapFormatted(string text, int width)
    {
        var wrap = new StringBuilder();

        for (int i = 0; i < text.Length; i += width)
        {
            int remainingLength = Math.Min(width, text.Length - i);
            string chunk = text.Substring(i, remainingLength);

            wrap.Append(chunk);
            wrap.Append('\n');
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
        var wrappedText = new StringBuilder(); // wrappedTextBuilder
        var currentLine = new StringBuilder(); // currentLineBuilder

        foreach (var word in words)
        {
            if (IsWordTooLong(word, width))
            {
                FlushCurrentLine(wrappedText, currentLine);
                AppendLongWord(word, width, wrappedText, currentLine);
            }
            else
            {
                if (CanFitWordOnLine(word, width, currentLine))
                {
                    AppendWordToLine(word, currentLine);
                }
                else
                {
                    FlushCurrentLine(wrappedText, currentLine);
                    currentLine.Append(word);
                }
            }
        }

        AppendRemainingLine(wrappedText, currentLine);

        return wrappedText.ToString();
    }

    private static bool IsWordTooLong(string word, int width)
    {
        return word.Length > width;
    }

    private static bool CanFitWordOnLine(string word, int width, StringBuilder currentLine)
    {
        int lineLengthWithWord = currentLine.Length == 0
            ? word.Length 
            : currentLine.Length + 1 + word.Length;

        return lineLengthWithWord <= width;
    }

    private static void AppendWordToLine(string word, StringBuilder currentLine)
    {
        if (currentLine.Length > 0)
        {
            currentLine.Append(' ');
        }
        currentLine.Append(word);
    }

    private static void FlushCurrentLine(StringBuilder wrappedText, StringBuilder currentLine)
    {
        if (currentLine.Length > 0)
        {
            wrappedText.Append(currentLine.ToString());
            wrappedText.Append(Environment.NewLine);
            currentLine.Clear();
        }
    }

    private static void AppendLongWord(string word, int width, StringBuilder wrappedText, StringBuilder currentLine)
    {
        for (int i = 0; i < word.Length; i += width)
        {
            int remainingLength = Math.Min(width, word.Length - i);
            string chunk = word.Substring(i, remainingLength);
             
            if (i + width < word.Length)
            {
                wrappedText.Append(chunk);
                wrappedText.Append(Environment.NewLine);
            }
            else
            {
                currentLine.Append(chunk);
            }
        }
    }

    private static void AppendRemainingLine(StringBuilder wrappedText, StringBuilder currentLine)
    {
        if (currentLine.Length > 0)
        {
            wrappedText.Append(currentLine.ToString());
        }
    }
}