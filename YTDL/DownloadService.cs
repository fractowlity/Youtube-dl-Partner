using System;
using System.Diagnostics;
using System.IO;

namespace YTDL
{
    public class DownloadService
    {
        public static void MainMenu()
        {
            //Get the initial user request - either Audio or Video
            Console.Write(
                "|| Main Menu ||\n\n" +
                "Choose from the following:\n" +
                "1) Audio\n" +
                "2) Video\n" +
                "/) View/Change Output Directory\n" +
                "') Exit (Apostrophe)\n");

            var menuInput = Console.ReadLine();

            switch (menuInput)
            {
                case MainMenuOptions.AUDIO:
                    ExecuteAudioRequest();
                    break;
                case MainMenuOptions.VIDEO:
                    GetVideoRequest();
                    break;
                case MainMenuOptions.CHANGE_DIRECTORY:
                    ChangeOutputDirectory();
                    MainMenu();
                    break;
                case MainMenuOptions.EXIT:
                    Environment.Exit(0);
                    break;
            }
        }

        public static string GetOutputDirectory()
        {
            //read OutputDirectory.txt and assign to the directory in the file
            string outputDirectoryFile = 
                Directory.GetParent(
                    Directory.GetParent(
                        Directory.GetParent(Environment.CurrentDirectory)
                        .ToString())
                    .ToString())
                .ToString();
            
            //string outputDirectoryFile = "OutputDirectory.txt";
            var mediaOutputDirectory = File.ReadAllText(outputDirectoryFile + "/OutputDirectory.txt");
            return mediaOutputDirectory;
        }

        public static void ChangeOutputDirectory()
        {
            //Get new output path as user input
            Console.WriteLine(
                "|| Media Output Directory Menu ||\n\n" +
                "Choose from the following:\n" +
                "1) Change Directory \n" +
                "2) View Current Directory\n" + 
                "') Return to Menu (Apostrophe) \n");
            var menuInput = Console.ReadLine();

            switch (menuInput)
            {
                case "1":
                    //change directory logic
                    Console.WriteLine("Enter the New Media Output Directory (eg. C:/ExampleDirectory):\n");
                    string outputDirectoryFile =
                        Directory.GetParent(
                            Directory.GetParent(
                                Directory.GetParent(Environment.CurrentDirectory)
                                .ToString())
                            .ToString())
                        .ToString();
                    var newDirectory = Console.ReadLine();
                    //now write line in file
                    File.WriteAllText(outputDirectoryFile + "/OutputDirectory.txt", newDirectory);

                    Console.WriteLine("\nDirectory Successfully Changed!\n");
                    break;
                case "2":
                    var currentMediaOutputDirectory = DownloadService.GetOutputDirectory();
                    Console.WriteLine($"Your current media output directory is: {currentMediaOutputDirectory}\n");
                    ChangeOutputDirectory();
                    break;
                case "'":
                    DownloadService.MainMenu();
                    break;
            }
            //Back to main menu.
            MainMenu();
        }

        /* Creates the youtube request (changes directory first) and executes with cmd.exe
         * @params@ 
         * @directory         = "/c cd c:/ExampleDirectory &"
         * @AUDIO_MP3_REQUEST = "youtube-dl --extract-audio --audio-format mp3 <video URL>"
         * @youtubeUrl        = user input youtube URL.
         */
        public static void ExecuteAudioRequest()
        {
            //Get directory to output media
            var mediaOutputDirectory = GetOutputDirectory();

            //Get youtube url as user input
            Console.WriteLine("|| Audio Request Menu ||\n\nPlease enter a youtube URL or enter ' to go to menu:\n");
            var youtubeUrl = Console.ReadLine();

            //Will not continue unless it is a valid youtube url
            IsValidYoutubeUrl(youtubeUrl);

            ExecuteDownloadRequest(youtubeUrl, mediaOutputDirectory, CmdLineArg.AUDIO);
        }

        //See ExecuteAudioRequest comments for how this works.
        public static void GetVideoRequest()
        {
            //Get directory to output media
            var mediaOutputDirectory = GetOutputDirectory();

            //Get youtube url as user input
            Console.WriteLine("\n|| Video Request Menu ||\nPlease enter the youtube URL:\n");
            var youtubeUrl = Console.ReadLine();

            //Will not continue unless it is a valid youtube url
            IsValidYoutubeUrl(youtubeUrl);

            ExecuteDownloadRequest(youtubeUrl, mediaOutputDirectory, CmdLineArg.VIDEO);
        }

       public static void ExecuteDownloadRequest(string youtubeUrl, string mediaOutputDirectory, string mediaRequestType)
        {
            if (mediaRequestType == CmdLineArg.AUDIO)
            {
                //Build and execute audio request
                string youtubeDlRequest = "/c cd " + mediaOutputDirectory + MediaRequest.AUDIO_MP3_REQUEST + youtubeUrl;
                Console.WriteLine("Downloading your audio request.  Please wait...");
                Process.Start("CMD.exe", youtubeDlRequest).WaitForExit();
            }
            if (mediaRequestType == CmdLineArg.VIDEO)
            {
                //Build and execute video request
                string youtubeDlRequest = "/c cd " + mediaOutputDirectory + MediaRequest.VIDEO_REQUEST + youtubeUrl;
                Console.WriteLine("Downloading your audio request.  Please wait...");
                Process.Start("CMD.exe", youtubeDlRequest).WaitForExit();
            }
        }


        //Small method to check if a user input is a valid Youtube Url, returns to main menu if not.
        public static void IsValidYoutubeUrl(string youtubeUrl)
        {
            string validYoutubeUrl =
                !String.IsNullOrWhiteSpace(youtubeUrl) && youtubeUrl.Length >= 23
                ? youtubeUrl.Substring(0, 23)
                : youtubeUrl;

            //Return to menu if user enters anything but a url
            if (validYoutubeUrl != "https://www.youtube.com" || validYoutubeUrl != "www.youtube.com") //TAG: May have a bug here on the second condition.
            {
                Console.WriteLine("You either entered an invalid URL or requested to return to the main menu.\n");
                MainMenu();
            };
        }
    }
}
