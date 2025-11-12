using WordWrapLibrary;

string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec magna turpis, dapibus vel faucibus id, venenatis in nulla. Donec ac hendrerit metus, sit amet consequat quam. Pellentesque nec mi sodales, suscipit nunc ut, laoreet erat. Vestibulum sed porttitor arcu, ac mattis turpis. Nullam vitae sodales felis. Maecenas aliquet, eros sollicitudin.";
string wrapped = WordWrap.Wrap(text, 20);
Console.WriteLine(wrapped);
