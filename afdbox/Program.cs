using System.Diagnostics;
using System.Text;

namespace afdbox
{
    internal class Program
    {
        static readonly string exePath = Path.GetDirectoryName(Environment.ProcessPath)!;
        static readonly string dosboxPath = Path.Combine(exePath, "DOSBoxPortable", "DOSBoxPortable.exe");
        static void Main(string[] args)
        {
            SetFileAssociation(".asm", "ASM File", Environment.ProcessPath!);

            UpdateConfig();

            AsmFile? asmFile = AsmFile.FromArgs(args);
            if (asmFile is not null)
            {
                File.Copy(asmFile.Path, Path.Combine(exePath, "program.asm"), true);

                RunExe(dosboxPath, $"-noconsole -userconf -conf \"{Path.Combine(exePath, "config.conf")}\"", false);

                return;
            }
            else
            {
                Console.WriteLine("afdbox setup successful.");
                Console.WriteLine("\nsource code: https://github.com/HusnainTaj/afdbox");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
        static void UpdateConfig()
        {
            File.WriteAllLines(Path.Combine(exePath, "config.conf"), new string[] { "[autoexec]", $"mount c: \"{exePath}\"", "c:","nasm program.asm -o program.com", "afd program.com" });
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