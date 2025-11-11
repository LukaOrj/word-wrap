using WordWrapLibrary;

namespace WordWrapLibrary.Tests;

public class WordWrapTests
{
    private static readonly string NewLine = Environment.NewLine;

    [Fact]
    public void Wrap_MediumRareSteak_WrapsCorrectly()
    {
        // Arrange
        string input = "No utensils please, and I want my steak cooked medium-rare.";
        int width = 15;
        string expected = $"No utensils{NewLine}please, and I{NewLine}want my steak{NewLine}cooked{NewLine}medium-rare.";

        // Act
        string result = WordWrap.Wrap(input, width);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Wrap_SuperLongWord_BreaksWordCorrectly()
    {
        // Arrange
        string input = "Please make sure the supercalifragilisticexpialidocious sauce is on the side.";
        int width = 15;
        string expected = $"Please make{NewLine}sure the{NewLine}supercalifragil{NewLine}isticexpialidoc{NewLine}ious sauce is{NewLine}on the side.";

        // Act
        string result = WordWrap.Wrap(input, width);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Wrap_MultipleSpaces_CollapsesWhitespace()
    {
        // Arrange
        string input = "Extra             sauce        on the         side        please";
        int width = 10;
        string expected = $"Extra{NewLine}sauce on{NewLine}the side{NewLine}please";

        // Act
        string result = WordWrap.Wrap(input, width);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Wrap_EmptyString_ReturnsEmpty()
    {
        // Arrange
        string input = "";
        int width = 10;

        // Act
        string result = WordWrap.Wrap(input, width);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Wrap_OnlyWhitespace_ReturnsEmpty()
    {
        // Arrange
        string input = "     \t\n\r   ";
        int width = 10;

        // Act
        string result = WordWrap.Wrap(input, width);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Wrap_SingleWordThatFits_ReturnsSameWord()
    {
        // Arrange
        string input = "Hello";
        int width = 10;

        // Act
        string result = WordWrap.Wrap(input, width);

        // Assert
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void Wrap_SingleWordTooLong_BreaksWord()
    {
        // Arrange
        string input = "HelloWorld";
        int width = 5;
        string expected = $"Hello{NewLine}World";

        // Act
        string result = WordWrap.Wrap(input, width);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Wrap_WidthOfOne_BreaksEveryCharacter()
    {
        // Arrange
        string input = "Hi there";
        int width = 1;
        string expected = $"H{NewLine}i{NewLine}t{NewLine}h{NewLine}e{NewLine}r{NewLine}e";

        // Act
        string result = WordWrap.Wrap(input, width);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Wrap_MultipleWordsFitOnLine_KeepsThemTogether()
    {
        // Arrange
        string input = "one two three";
        int width = 20;

        // Act
        string result = WordWrap.Wrap(input, width);

        // Assert
        Assert.Equal("one two three", result);
    }

    [Fact]
    public void Wrap_NullInput_ThrowsArgumentNullException()
    {
        // Arrange
        string input = null;
        int width = 10;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => WordWrap.Wrap(input, width));
    }

    [Fact]
    public void Wrap_ZeroWidth_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        string input = "test";
        int width = 0;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => WordWrap.Wrap(input, width));
    }

    [Fact]
    public void Wrap_NegativeWidth_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        string input = "test";
        int width = -5;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => WordWrap.Wrap(input, width));
    }
}