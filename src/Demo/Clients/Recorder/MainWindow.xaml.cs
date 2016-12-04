using System.Windows;
using Recorder.Services.Recording;
using Recorder.Services.Testing;
using TestBuilder;

namespace Recorder
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRecordingService _recordingService;
        private readonly ITestService _testService;

        public MainWindow()
        {
            InitializeComponent();

            _recordingService = new RecordingService(new TestBuildManager());
            _testService = new TestService();

        }

        private void btnLaunchNotepad_Click(object sender, RoutedEventArgs e)
        {
            _recordingService.LaunchNotepad();
        }

        private void btnWriteContent_Click(object sender, RoutedEventArgs e)
        {
            var content = tbContent.Text;
            if (string.IsNullOrEmpty(content) || string.IsNullOrWhiteSpace(content))
            {
                MessageBox.Show("Please set content");
                return;
            }
            _recordingService.WriteText(content);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _recordingService.SaveDocument();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            _testService.Create(_recordingService.CurrentTest);
        }
    }
}
 