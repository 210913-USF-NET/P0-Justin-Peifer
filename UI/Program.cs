using System;
using Serilog;
using System.Threading;
using System.Runtime.InteropServices;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .WriteTo.File("../logs/logs.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

        Log.Information("Application Starting...");
        

        new StartMenu().Start(); 
        if (OperatingSystem.IsWindows()){//soundplayer is only available in windows, so this will prevent the soundplayer from trying to load in other OS

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\Justin Peifer\Desktop\bye-bye.wav");
            player.Play();
            Thread.Sleep(3000);//so that the audio can play before the application completely closes
        }
        Log.Information("Application Closing...");
        Log.CloseAndFlush();
        }
    }
}