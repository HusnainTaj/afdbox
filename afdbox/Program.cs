using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace afdbox
{
    internal class Program
    {
        static readonly string exePath = Path.GetDirectoryName(Environment.ProcessPath)!;
        static readonly string dosboxPath = Path.Combine(exePath, "DOSBoxPortable", "DOSBoxPortable.exe");
        static void Main(string[] args)
        {
            if(SetFileAssociation(".asm", "asmfile", Environment.ProcessPath!) != 0)
            {
                Console.WriteLine("Automatic File Association Failed.");
                Console.WriteLine("Try running afdbox as Admin.");
                Console.WriteLine("Or Associate .asm files with afdbox manually through properties by changing 'opens with' for .asm files.\n");
            }

            bool debugMode = (Config.Get("debug") ?? "debug") == "debug";

            UpdateConfig(debugMode);

            AsmFile? asmFile = AsmFile.FromArgs(args);
            if (asmFile is not null)
            {
                try
                {
                    File.Copy(asmFile.Path, Path.Combine(exePath, "program.asm"), true);
                }
                catch (IOException)
                {
                    Console.WriteLine("Error");
                    Console.WriteLine("asm file is being used by another process.");
                    Console.WriteLine("Use an editor like VS Code that does not lock the file while editing.");
                    Console.WriteLine("\nPress any key to exit...");
                    Console.ReadKey();
                    return;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Error");
                    Console.WriteLine("asm file is stored in a protected directory.");
                    Console.WriteLine("This can usually be fixed by moving both labsetup folder and asm files in drives other than C: ");
                    Console.WriteLine("\nPress any key to exit...");
                    Console.ReadKey();
                    return;
                }

                RunExe(dosboxPath, $"-noconsole -userconf -conf \"{Path.Combine(exePath, "config.conf")}\"", false);

                return;
            }
            else
            {
                Console.WriteLine("afdbox setup successful.");
                Console.WriteLine("\nsource code: https://github.com/HusnainTaj/afdbox");

                Console.WriteLine($"\nRunning in {(debugMode ? "Debug" : "Run")} mode.");

                Console.WriteLine("\nPress any key change mode...");

                while (true)
                {
                    Console.ReadKey();

                    debugMode = !debugMode;

                    Config.Set("debug", debugMode ? "debug" : "run");

                    Console.WriteLine($"\nSwitched to {(debugMode ? "Debug" : "Run")} mode.");
                }
            }
        }
        static void UpdateConfig(bool debug)
        {
            File.WriteAllLines(Path.Combine(exePath, "config.conf"), new string[] { "[autoexec]", $"mount c: \"{exePath}\"", "c:","nasm program.asm -o program.com", debug ? "afd program.com" : "program.com" });
        }

        public static int SetFileAssociation(string extension, string fileType, string programPath)
        {
            ExecuteCMDCommand($"/c ftype {fileType}=\"{programPath}\" \"%1\"");
            int v = ExecuteCMDCommand($"/c assoc {extension}={fileType}");
            if (v != 0) return v;
            return v;
        }

        public static int ExecuteCMDCommand(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = "cmd";
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
        public static int RunExe(string path, string arguments, bool waitForExit = true)
        {
            ProcessStartInfo startInfo = new(path, arguments);

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    if (waitForExit)
                        exeProcess.WaitForExit();
                    else return -1;

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