using System.Diagnostics;

namespace AdventOfCode;

internal class BrainfuckInterpreter
{
    public void Execute(string code, string inputFilePath)
    {
        const int memorySize = 30000;
        var memory = new int[memorySize];
        var pointer = 0;
        var codeIndex = 0;

        var inputData = File.ReadAllText(inputFilePath);
        var inputIndex = 0;

        while (codeIndex < code.Length)
        {
            var command = code[codeIndex];
            switch (command)
            {
                case '>':
                    pointer = (pointer + 1) % memorySize;
                    break;
                case '<':
                    pointer = (pointer - 1 + memorySize) % memorySize;
                    break;
                case '+':
                    memory[pointer]++;
                    break;
                case '-':
                    memory[pointer]--;
                    break;
                case '.':
                    Console.WriteLine(pointer + ": " + memory[pointer]);
                    break;
                case ',':
                    if (inputIndex < inputData.Length)
                    {
                        memory[pointer] = inputData[inputIndex++] - 48;
                    }
                    else
                    {
                        memory[pointer] = (int)'\0';
                    }

                    break;
                case '[':
                    if (memory[pointer] == 0)
                    {
                        var loopEnd = FindLoopEnd(code, codeIndex);
                        if (loopEnd == -1)
                        {
                            Console.WriteLine("Unmatched '[' in the code.");
                            return;
                        }

                        codeIndex = loopEnd;
                    }

                    break;
                case ']':
                    if (memory[pointer] != 0)
                    {
                        var loopStart = FindLoopStart(code, codeIndex);
                        if (loopStart == -1)
                        {
                            Console.WriteLine("Unmatched ']' in the code.");
                            return;
                        }

                        codeIndex = loopStart;
                    }

                    break;
                case '_':
                    Debugger.Break();
                    break;
            }

            codeIndex++;
        }
    }

    private int FindLoopEnd(string code, int startIndex)
    {
        var depth = 1;
        for (var i = startIndex + 1; i < code.Length; i++)
        {
            if (code[i] == '[') depth++;
            if (code[i] == ']') depth--;
            if (depth == 0) return i;
        }

        return -1;
    }

    private int FindLoopStart(string code, int endIndex)
    {
        var depth = 1;
        for (var i = endIndex - 1; i >= 0; i--)
        {
            if (code[i] == ']') depth++;
            if (code[i] == '[') depth--;
            if (depth == 0) return i;
        }

        return -1;
    }
}