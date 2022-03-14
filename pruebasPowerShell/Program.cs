using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;


namespace pruebasPowerShell
{
  
    class Program
    {
        public static async Task<string> ejecucion()
        {
            const string fileName = "ipPower_Async.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            // "Invoke-RestMethod ipinfo.io/ip"
            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = "Invoke-RestMethod ipinfo.io/ip",
                CreateNoWindow = true
            };

            using var process = Process.Start(start);
            using var reader = process.StandardOutput;

            process.EnableRaisingEvents = true;

            var ipAddressResult = reader.ReadToEnd();

            await File.WriteAllTextAsync(fileName, ipAddressResult);
            await process.WaitForExitAsync();

            return await File.ReadAllTextAsync(fileName);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(ejecucion().Result);
            Console.ReadLine();
        }
    }
}
