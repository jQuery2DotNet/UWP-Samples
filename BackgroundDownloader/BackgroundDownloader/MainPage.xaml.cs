using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BackgroundDownloader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DownloadOperation downloadOperation;
        CancellationTokenSource cancellationToken;
        Windows.Networking.BackgroundTransfer.BackgroundDownloader backgroundDownloader = new Windows.Networking.BackgroundTransfer.BackgroundDownloader();
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ButtonDownload_Click(object sender, RoutedEventArgs e)
        {
            Download();        }

        public async void Download()
        {
            FolderPicker folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Downloads;
            folderPicker.ViewMode = PickerViewMode.Thumbnail;
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                StorageFile file = await folder.CreateFileAsync("NewImage.jpg", CreationCollisionOption.GenerateUniqueName);
                Uri downloadUrl = new Uri(LinkBox.Text.ToString());
                downloadOperation = backgroundDownloader.CreateDownload(downloadUrl, file);
                Progress<DownloadOperation> progress = new Progress<DownloadOperation>(x => ProgressChanged(downloadOperation));
                cancellationToken = new CancellationTokenSource();

                try
                {
                    TextBlockStatus.Text = "Initializing...";
                    await downloadOperation.StartAsync().AsTask(cancellationToken.Token, progress);
                }
                catch (TaskCanceledException)
                {
                    TextBlockStatus.Text = "Download canceled.";
                    await downloadOperation.ResultFile.DeleteAsync();
                    downloadOperation = null;
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            cancellationToken.Cancel();
            cancellationToken.Dispose();
        }

        private void ProgressChanged(DownloadOperation downloadOperation)
        {
            int progress = (int)(100 * ((double)downloadOperation.Progress.BytesReceived / (double)downloadOperation.Progress.TotalBytesToReceive));

            switch (downloadOperation.Progress.Status)
            {
                case BackgroundTransferStatus.Running:
                case BackgroundTransferStatus.Completed:
                    {
                        TextBlockStatus.Text = string.Format("{0} of {1} kb. downloaded - {2}% complete.", downloadOperation.Progress.BytesReceived / 1024, downloadOperation.Progress.TotalBytesToReceive / 1024, progress);
                        //TextBlockStatus.Text = "Downloading...";
                        break;
                    }
                case BackgroundTransferStatus.PausedByApplication:
                    {
                        TextBlockStatus.Text = "Download paused.";
                        break;
                    }
                case BackgroundTransferStatus.PausedCostedNetwork:
                    {
                        TextBlockStatus.Text = "Download paused because of metered connection.";
                        break;
                    }
                case BackgroundTransferStatus.PausedNoNetwork:
                    {
                        TextBlockStatus.Text = "No network detected. Please check your internet connection.";
                        break;
                    }
                case BackgroundTransferStatus.Error:
                    {
                        TextBlockStatus.Text = "An error occured while downloading.";
                        break;
                    }
                case BackgroundTransferStatus.Canceled:
                    {
                        TextBlockStatus.Text = "Download canceled.";
                        break;
                    }
            }

            if (progress >= 100)
            {
                downloadOperation = null;
            }
        }
    }
}
