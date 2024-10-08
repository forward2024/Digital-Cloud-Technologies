﻿namespace WPF;

public partial class MainWindow : Window
{
    public static event EventHandler<ApplicationTheme?> ThemeChanged;

    public MainWindow()
    {
        InitializeComponent();
        ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
        MainFrame.Navigate(new Home());
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    private void Window_ActualThemeChanged(object sender, RoutedEventArgs e) => Debug.WriteLine(ThemeManager.GetActualTheme(this));
    private void MainFrame_Navigated(object sender, NavigationEventArgs e) => BackButton.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;

    private void ToggleThemeButton_Click(object sender, RoutedEventArgs e)
    {
        if (ThemeManager.Current.ApplicationTheme == ApplicationTheme.Light)
        {
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
        }
        else
        {
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
        }

        ThemeChanged?.Invoke(this, ThemeManager.Current.ApplicationTheme);
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (MainFrame.CanGoBack)
        {
            MainFrame.GoBack();
            MainFrame.RemoveBackEntry();
        }
    }

    private void HomeButton_Click(object sender, RoutedEventArgs e)
    {
        if (MainFrame.Content is not Home)
        {
            MainFrame.Navigate(new Home());

            MainFrame.Navigated -= ClearBackStackAfterNavigation;
            MainFrame.Navigated += ClearBackStackAfterNavigation;
        }

        void ClearBackStackAfterNavigation(object sender, NavigationEventArgs e)
        {
            while (MainFrame.CanGoBack)
            {
                MainFrame.RemoveBackEntry();
            }

            BackButton.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;
            MainFrame.Navigated -= ClearBackStackAfterNavigation;
        }
    }

    private void TestButton_Click(object sender, RoutedEventArgs e)
    {
        if (MainFrame.Content is not TestNavigation)
        {
            MainFrame.Navigate(new TestNavigation());
        }
    }
}