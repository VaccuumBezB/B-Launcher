using System;
using BW_Launcher.Models;
using System.Collections.Generic;
using BW_Launcher.Helpers;
using Avalonia.Media.Imaging;

namespace BW_Launcher.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    static sbyte osId = MainWindowModel.GetOS();

    public Bitmap? OSLogo { get; } = ImageHelper.LoadFromResource(new Uri((osId == 0) ? "avares://BW-Launcher/Assets/windows.png" : "avares://BW-Launcher/Assets/linux.png"));
    public string BWorldFolderLocation { get; } = (osId == 0) ? @"C:\ProgramFiles\B-World\" : @"~/.b-world/";
    
    const string URL = "https://127.0.0.0/";
    //public static List<BW_Launcher.Models.Version> versions_ = BW_Launcher.Models.MainWindowModel.GetVersionsList();
}
