﻿using System.Diagnostics;
using System.Reflection;

namespace afdbox
{
    internal class Program
    {
        static string dosboxPath = "";
        static string afdPath = "";
        static string nasmPath = "";

        static string cwd = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "afdbox");
        static void Main(string[] args)
        {
            setFileAssociation(new string[] { ".asm" }, "ASM File", $"{Environment.ProcessPath}");

            setupPaths();  

            setupDir();

            AsmFile? asmFile = AsmFile.FromArgs(args);
            if (asmFile is not null)
            {
                Process.Start(nasmPath, $"\"{asmFile.Path}\" -o \"{Path.Combine(cwd, "out.com")}\"");

                Process.Start(dosboxPath, $"-conf {Path.Combine(cwd, "conf.txt")}");
                
                return;
            }

            Console.WriteLine("Press 'r' to reset afdbox. Press anything else to exit.");
            if(Console.ReadKey().Key == ConsoleKey.R)
            {
                reset();
                Console.WriteLine("afdbox has been reset!");
            }

            Console.WriteLine("\nExiting!");
        }


        static void setupPaths()
        {
            Directory.CreateDirectory(Config.configPath);

            try
            {
                string GetPath(string key, string msg)
                {
                    bool isUpdated = false;
                    string? current = Config.Get(key);

                    while (string.IsNullOrEmpty(current) || !File.Exists(current))
                    {
                        Console.Write(msg);
                        current = Console.ReadLine() ?? "";
                        current = current.Replace("\"", "");
                        isUpdated = true;
                    }

                    if (isUpdated) Config.Set(key, current);

                    return current;
                }

                dosboxPath = GetPath(Config.KEY_DOSBOX_PATH, "DOSBox Path: ");
                afdPath = GetPath(Config.KEY_AFD_PATH, "AFD Path: ");
                nasmPath = GetPath(Config.KEY_NASM_PATH, "NASM Path: ");
            }
            catch (Exception)
            {
                Console.ReadKey();
                throw;
            }
        }

        static void setupDir()
        {
            Directory.CreateDirectory(cwd);
            if (!File.Exists(Path.Combine(cwd, "afd.exe"))) File.Copy(afdPath, Path.Combine(cwd, "afd.exe"));
            File.WriteAllLines(Path.Combine(cwd, "conf.txt"), new string[] { "[autoexec]", $"mount c: \"{cwd}\"", "c:", "afd out.com" });
        }

        static void reset()
        {
            if (Directory.Exists(cwd)) Directory.Delete(cwd, true);
            if (Directory.Exists(Config.configPath)) Directory.Delete(Config.configPath, true);
        }



        public static int setFileAssociation(string[] extensions, string fileType, string openCommandString)
        {
            int v = execute("cmd", "/c ftype " + fileType + "=" + openCommandString);
            foreach (string ext in extensions)
            {
                v = execute("cmd", "/c assoc " + ext + "=" + fileType);
                if (v != 0) return v;
            }
            return v;
        }
        public static int execute(string exeFilename, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = exeFilename;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = arguments;
            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                    return exeProcess.ExitCode;
                }
            }
            catch
            {
                return 1;
            }
        }
    }
}