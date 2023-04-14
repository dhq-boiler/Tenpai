
Console.SetWindowSize(40, 40);
Console.SetBufferSize(40, 10011);

for (int i = 0; i < 10000; i++)
{
    if (i < 19)
    {
        System.Console.Write("true");
    }
    else
    {
        System.Console.Write("false");
    }
    System.Console.WriteLine(",");
}