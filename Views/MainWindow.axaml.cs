using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Interactivity;
using BW_Launcher.Models;
using BW_Launcher.ViewModels;
using BW_Launcher.Helpers;
using static BW_Launcher.Helpers.L;
using System.Threading.Tasks;
using System;

namespace BW_Launcher.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Log("Initialized");
        RunButton.Click += RunButtonClickHandler;
        InstallButton.Click += InstallButtonClickHandler;
        //Log($"Avalonia ItemSource variabble elements count is {MainWindowViewModel.verDisplayNames.Count}", "DEBUG");
    }

    public void InstallButtonClickHandler(object? sender, RoutedEventArgs e)
    {
        Log("Install button clicked");

        var result = Task.Run(async () => await BW_Launcher.Models.VersionsInstaller.DownloadAsync(
            BW_Launcher.ViewModels.MainWindowViewModel.Link,
            BW_Launcher.ViewModels.MainWindowViewModel.ID
        )).GetAwaiter().GetResult();
    }

    public void RunButtonClickHandler(object? sender, RoutedEventArgs e) 
    {
        Log("Play button clicked");

        ProcessRunner.RunGame(MainWindowViewModel.ID);
    }

    /*protected void OnClosed()
    {
        _mediaPlayer.Dispose();
            _libVLC.Dispose();
            //base.OnClosed();
    }*/
}