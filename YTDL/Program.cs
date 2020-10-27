using System;
using System.Diagnostics;
using YTDL;

namespace YTDL
{
    class Program
    {
        static void Main(string[] args)
        {
            //If index OOB we need 'h' as the args[0] to access main menu for the switch below.
            if (args.Length == 0)
            {
                args = new string[] {"h"};
            }

            /*
             * Takes in command line arguments to direct to specific main menu requests.
             * Example usages:
             * 'ytdl' goes to main menu
             * 'ytdl a' goes to the audio request menu
             * 'ytdl v' goes to the video request menu
             * 'ytdl /' goes to the change directory menu
             */
            switch (args[0])
            {
                case CmdLineArg.AUDIO:
                    DownloadService.ExecuteAudioRequest();
                    break;
                case CmdLineArg.VIDEO:
                    DownloadService.GetVideoRequest();
                    break;
                case CmdLineArg.CHANGE_DIRECTORY:
                    DownloadService.ChangeOutputDirectory();
                    break;
                case CmdLineArg.MAIN_MENU:
                    DownloadService.MainMenu();
                    break;
                default:
                    DownloadService.MainMenu();
                    break;
            }
            
        }
    }
}
