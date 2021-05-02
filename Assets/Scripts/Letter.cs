using System.Collections.Generic;

public class Letter
{
    public Letter(char letter)
    {
        Slots = new bool[Alphabet[letter][0].Length, Alphabet[letter].Length];

        NonemptySlotsCount = 0;

        for (int row = 0; row < Alphabet[letter].Length; row++)
        {
            for (int col = 0; col < Alphabet[letter][row].Length; col++)
            {
                if (char.IsWhiteSpace(Alphabet[letter][row][col]))
                {
                    Slots[row, col] = false;
                }
                else
                {
                    Slots[row, col] = true;
                    NonemptySlotsCount++;
                }
            }
        }
    }

    public int NonemptySlotsCount { get; private set; }
    public bool[,] Slots { get; private set; }

    private static readonly Dictionary<char, string[]> Alphabet = new Dictionary<char, string[]>
    {
        { 'A', new string[] {
            "  0  ",
            " 0 0 ",
            " 000 ",
            "0   0",
            "0   0"} },
        { 'B', new string[] {
            "0000 ",
            "0   0",
            "0000 ",
            "0   0",
            "0000 "} },
        { 'C', new string[] {
            " 0000",
            "0    ",
            "0    ",
            "0    ",
            " 0000"} },
        { 'D', new string[] {
            "0000 ",
            "0   0",
            "0   0",
            "0   0",
            "0000 "} },
        { 'E', new string[] {
            "00000",
            "0    ",
            "0000 ",
            "0    ",
            "00000"} },
        { 'F', new string[] {
            "00000",
            "0    ",
            "0000 ",
            "0    ",
            "0    "} },
        { 'G', new string[] {
            "00000",
            "0    ",
            "0  00",
            "0   0",
            "00000"} },
        { 'H', new string[] {
            "0   0",
            "0   0",
            "00000",
            "0   0",
            "0   0"} },
        { 'I', new string[] {
            "00000",
            "  0  ",
            "  0  ",
            "  0  ",
            "00000"} },
        { 'J', new string[] {
            "00000",
            "  0  ",
            "  0  ",
            "  0  ",
            "00   "} },
        { 'K', new string[] {
            "0   0",
            "0  0 ",
            "000  ",
            "0  0 ",
            "0   0"} },
        { 'L', new string[] {
            "0    ",
            "0    ",
            "0    ",
            "0    ",
            "00000"} },
        { 'M', new string[] {
            "0   0",
            "00 00",
            "0 0 0",
            "0   0",
            "0   0"} },
        { 'N', new string[] {
            "0   0",
            "00  0",
            "0 0 0",
            "0  00",
            "0   0"} },
        { 'O', new string[] {
            " 000 ",
            "0   0",
            "0   0",
            "0   0",
            " 000 "} },
        { 'P', new string[] {
            "0000 ",
            "0   0",
            "0000 ",
            "0    ",
            "0    "} },
        { 'Q', new string[] {
            " 000 ",
            "0   0",
            "0 0 0",
            "0  00",
            " 0000"} },
        { 'R', new string[] {
            "0000 ",
            "0   0",
            "0000 ",
            "0  0 ",
            "0   0"} },
        { 'S', new string[] {
            " 0000",
            "0    ",
            " 000 ",
            "    0",
            "0000 "} },
        { 'T', new string[] {
            "00000",
            "  0  ",
            "  0  ",
            "  0  ",
            "  0  "} },
        { 'U', new string[] {
            "0   0",
            "0   0",
            "0   0",
            "0   0",
            " 000 "} },
        { 'V', new string[] {
            "0   0",
            "0   0",
            " 0 0 ",
            " 0 0 ",
            "  0  "} },
        { 'W', new string[] {
            "0   0",
            "0   0",
            "0 0 0",
            "00 00",
            "0   0"}  },
        { 'X', new string[] {
            "0   0",
            " 0 0 ",
            "  0  ",
            " 0 0 ",
            "0   0"} },
        { 'Y', new string[] {
            "0   0",
            " 0 0 ",
            "  0  ",
            "  0  ",
            "  0  "} },
        { 'Z', new string[] {
            "00000",
            "   0 ",
            "  0  ",
            " 0   ",
            "00000"}  },
        { ' ', new string[] {
            "     ",
            "     ",
            "     ",
            "     ",
            "     "}  }
    };
}