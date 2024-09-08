using System;
using System.Text;

		namespace blkrse
						{
 
public class RanNumGen
{
    private static readonly string AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789/*-+=:;,ùµ$^-)àç!è§('é&|@#{[^{}[]`´~<>\\¶¥¤¼»º§¦©ª«¬­®¯±²Þß÷ö";
    public static string Generate(int length)
    {
        StringBuilder sb = new StringBuilder(length);
        Random random = new Random();
        for (int i = 0; i < length; i++)
        {
            int index = random.Next(AllowedChars.Length);
            sb.Append(AllowedChars[index]);
        }
        return sb.ToString();
    }
}