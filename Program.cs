using System;
using System.IO;
using System.Threading;

class Program
{
    public static void Main()
    {
        string srcPath = $@"C:\Users\Acer\source\repos\ConsoleApp5\ConsoleApp5\text1.txt";
        string destPath = $@"C:\Users\Acer\source\repos\ConsoleApp5\ConsoleApp5\text2.txt";

        if (!File.Exists(srcPath))
        {
            Console.WriteLine("File not exists");
            return;
        }

        using (FileStream fsRead = new FileStream(srcPath, FileMode.Open, FileAccess.Read))
        {
            long totalSize = fsRead.Length;
            Console.WriteLine($"Size: {totalSize} bytes");

            using (FileStream fsWrite = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                int len = 10; 
                byte[] buffer = new byte[len];
                long copied = 0;

                do
                {
                    len = fsRead.Read(buffer, 0, buffer.Length);
                    if (len > 0)
                    {
                        fsWrite.Write(buffer, 0, len);

                        copied += len;
                        double progress = (double)copied / totalSize;

                        DrawProgressBar(progress, 40); 
                    }

                    Thread.Sleep(5);

                } while (len != 0);

            }
        }

        Console.WriteLine("\nKopyalama tamamlandi!");
    }

    static void DrawProgressBar(double progress, int barSize)
    {
        int filled = (int)(progress * barSize);
        Console.CursorLeft = 0;
        Console.Write("[");
        Console.Write(new string('#', filled));
        Console.Write(new string('-', barSize - filled));
        Console.Write($"] {progress:P0}");
    }
}
