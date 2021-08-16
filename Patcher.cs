/*!
 * Compile using `csc` on Linux, or Visual Studio on Windows.
 * Be sure to install the Mono.Cecil NuGet package first. (or on Linux just obtain the DLLs somehow...)
 */
using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;

public static class Program
{
    public static int Main(string[] args)
    {
        var EXIT_SUCCESS = 0;
        var EXIT_FAILURE = 1;

        try
        {
            if (args.Length <= 0)
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("Patcher.exe /path/to/DialogEditor.dll optional:version string to use");
                Console.WriteLine("Example:");
                Console.WriteLine("Patcher.exe /home/nikita/DialogEditor.dll 2.3.3.475");
                Console.WriteLine("Or drag and drop DialogEditor.dll onto the exe if you're on Windows.");
            }
            else
            {
                var defaultVersion = "2.3.3.574";
                var toPatchVersion = args.Length > 1 ? args[1] : defaultVersion;

                var ok = Version.TryParse(toPatchVersion, out Version result);
                if (!ok)
                {
                    throw new InvalidOperationException("Failed to parse the version string! ver=" + toPatchVersion);
                }

                Console.WriteLine("Will patch the version to " + toPatchVersion);

                var path = args[0];
                Console.WriteLine("Reading file " + path);

                var module = ModuleDefinition.ReadModule(path, new ReaderParameters(ReadingMode.Deferred));
                
                var yylist = new List<string> { "Graphics", "IDE", "OSCore", "Utils", "YYCodeEditor" };

                for (int i = 0; i < module.AssemblyReferences.Count; ++i)
                {
                    var mref = module.AssemblyReferences[i];
                    if (yylist.IndexOf(mref.Name) >= 0)
                    {
                        Console.WriteLine("Patching version of " + mref.Name);
                        mref.Version = result;
                    }
                }

                
                var fn = Path.GetFileNameWithoutExtension(path);
                fn = fn + ".modified" + Path.GetExtension(path);
                path = Path.Combine(Path.GetDirectoryName(path), fn);
                Console.WriteLine("Writing file as " + path);
                module.Write(path);
                module.Dispose();
                Console.WriteLine("Done! Place the dll into the IDE folder. A new 'DialogEditor' plugin should load.");
            }

            return EXIT_SUCCESS;
        }
        catch (Exception exc)
        {
            Console.WriteLine("We're sorry, an exception has occurred in this app:");
            Console.WriteLine(exc.ToString());
            Console.WriteLine();
            Console.WriteLine();
            return EXIT_FAILURE;
        }
    }
}
