using Microsoft.Web.WebView2.Wpf;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace AutoBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>  
    public partial class MainWindow : Window
    {
        public FileSystemWatcher Watcher { get; set; }

        private ToggleButton monitoringToggleButton;
        private TextBox addressTextBox;

        public MainWindow()
        {
            InitializeComponent();

            Watcher = new FileSystemWatcher()
            {
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime
            };

            Watcher.Filters.Add("*.html");
            Watcher.Filters.Add("*.htm");
            Watcher.Filters.Add("*.css");
            Watcher.Filters.Add("*.js");

            Watcher.Changed += Watcher_Changed;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => webBrowser.Reload());
        }

        private void Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string givenAddress = ((TextBox)sender).Text;
                if (!string.IsNullOrEmpty(givenAddress)
                    && Uri.IsWellFormedUriString(givenAddress, UriKind.RelativeOrAbsolute) && Uri.TryCreate(givenAddress, UriKind.RelativeOrAbsolute, out Uri? address))
                {
                    try
                    {
                        if (address?.IsAbsoluteUri == true)
                        {
                            if (address.IsFile)
                            {
                                string? path = Path.GetDirectoryName(address.LocalPath);

                                if (!string.IsNullOrEmpty(path))
                                {
                                    Watcher.Path = path;
                                    Watcher.EnableRaisingEvents = true;
                                }
                            }
                            else
                                Watcher.EnableRaisingEvents = false;
                        }
                        else
                        {
                            address = new Uri($"http://{address?.OriginalString}");
                            Watcher.EnableRaisingEvents = false;
                        }

                        webBrowser.Source = address;
                    }
                    catch (ArgumentException)
                    {
                        Watcher.EnableRaisingEvents = false;
                    }
                }
            }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Hypertext files (*.html, *.htm)|*.html;*.htm|Stylesheet files (*.css)|*.css|JavaScript codefiles (*.js)|*.js|All files (*.*)|*.*"
            };
            if (fileDialog.ShowDialog() == true)
            {
                string? path = Path.GetDirectoryName(fileDialog.FileName);
                if (!string.IsNullOrEmpty(path))
                {
                    Watcher.Path = path;
                    Watcher.EnableRaisingEvents = true;
                }
                else
                {
                    Watcher.EnableRaisingEvents = false;
                    MessageBox.Show("Cannot activate file monitoring: path is empty or invalid.", null, MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                webBrowser.Source = new Uri($"file:///{fileDialog.FileName}");
            }
        }

        private void DeveloperToolsButton_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.CoreWebView2?.OpenDevToolsWindow();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.CoreWebView2?.GoBack();
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.CoreWebView2?.GoForward();
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.CoreWebView2?.Reload();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            webBrowser.CoreWebView2?.Stop();
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileButton_Click(sender, new RoutedEventArgs());
        }

        private void MonitorToggleButton_Loaded(object sender, RoutedEventArgs e)
        {
            monitoringToggleButton = (ToggleButton)sender;
        }

        private void AddressTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            addressTextBox = (TextBox)sender;
        }

        private void WebBrowser_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            monitoringToggleButton.GetBindingExpression(ToggleButton.IsCheckedProperty).UpdateTarget();
            addressTextBox.Text = ((WebView2)sender).Source.ToString();
        }
    }
}