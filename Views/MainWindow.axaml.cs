using Avalonia.Controls;
using Avalonia.Media;
using BW_Launcher.ViewModels;
using BW_Launcher.Helpers;
using static BW_Launcher.Helpers.L;

namespace BW_Launcher.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Info("Initialized");

        //TODO: daaaayum
        //VersionsList.Items = MainWindowViewModel.versions_;
        //VersionsList.SelectedIndex = 0;
    }
}