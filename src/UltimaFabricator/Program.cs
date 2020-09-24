using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using UltimaFabricator.Native;

namespace UltimaFabricator
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            SetupDllPaths();
            using var game = new Game();
            using var graphicsDeviceManager = new GraphicsDeviceManager(game)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080,
                PreferMultiSampling = true
            };

            graphicsDeviceManager.ApplyChanges();

            game.Run();
        }

        private static void SetupDllPaths()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return;
            }

            try
            {
                Kernel32.SetDefaultDllDirectories(Kernel32.LoadLibrarySearchDefaultDirs);
                Kernel32.AddDllDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    Environment.Is64BitProcess ? "x64" : "x86"
                ));
            }
            catch
            {
                // Pre-Windows 7, KB2533623
                Kernel32.SetDllDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    Environment.Is64BitProcess ? "x64" : "x86"));
            }
        }
    }
}
