using System;
using System.Collections.Generic;
using System.IO;
using Antlr4.Runtime;
using MetaGram.Antlr;

namespace MetaGram
{
    static internal class Program
    {
        static private void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }

            FileInfo grammarFile = null;
            DirectoryInfo outputDirectory = null;
            bool defaultTarget = false;
            var targets = new List<string>();

            foreach (string arg in args)
            {
                if (arg.StartsWith("--", StringComparison.Ordinal))
                {
                    switch (arg)
                    {
                        case "--default":
                            defaultTarget = true;
                            continue;
                    }
                }
                else if (grammarFile == null)
                    grammarFile = new FileInfo(arg);
                else if (outputDirectory == null)
                    outputDirectory = new DirectoryInfo(arg);
                else
                    targets.Add(arg);
            }

            if (grammarFile?.Directory == null)
            {
                PrintUsage();
                return;
            }

            var inputStream = new AntlrFileStream(grammarFile.FullName);

            var lexer = new MetaGramLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MetaGramParser(tokens);
            MetaGramParser.ParseContext context = parser.parse();

            DirectoryInfo directory = grammarFile.Directory.CreateSubdirectory("metagram");

            if (defaultTarget)
            {
                string result = new TargetVisitor().Visit(context);
                DirectoryInfo targetDirectory = directory.CreateSubdirectory("default");
                File.WriteAllText(Path.Combine(targetDirectory.FullName, grammarFile.Name), result);
            }

            foreach (string target in targets)
            {
                string result = new TargetVisitor(target).Visit(context);
                DirectoryInfo targetDirectory = directory.CreateSubdirectory(target);
                File.WriteAllText(Path.Combine(targetDirectory.FullName, grammarFile.Name), result);
            }
        }

        static private void PrintUsage()
        {
            Console.WriteLine("Usage: MetaGram grammarFile outputDirectory --default (targetName)*");
        }
    }
}
