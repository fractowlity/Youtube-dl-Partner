using System;
using System.Collections.Generic;
using System.Text;


public class MainMenuOptions
{
    public const string AUDIO = "1";
    public const string VIDEO = "2";
    public const string EXIT = "'";
    public const string CHANGE_DIRECTORY = "/";
}

public class CmdLineArg
{
    public const string AUDIO = "a";
    public const string VIDEO = "v";
    public const string CHANGE_DIRECTORY = "/";
    public const string MAIN_MENU = "h";
}
public class MediaRequest
{
    public const string AUDIO_MP3_REQUEST = " & youtube-dl --extract-audio --audio-format mp3 ";
    public const string VIDEO_REQUEST = " & youtube-dl  ";
}
