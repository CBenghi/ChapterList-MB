using System;
using System.Drawing;
using System.Windows.Forms;
using ChapterListMB;
using Timer = System.Timers.Timer;
using System.IO;
using SyncView;
using System.Diagnostics;
using System.Threading;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        private MusicBeeApiInterface mbApiInterface;
        private PluginInfo _about = new PluginInfo();
        private MainForm _mainForm;
        private Track _track;
        private Timer _timer;
        private Chapter _currentChapter;

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);
            _about.PluginInfoVersion = PluginInfoVersion;
            _about.Name = "SyncView";
            _about.Description = "Allows to show images in sync with audio.";
            _about.Author = "Claudio Benghi, on a base from Eincrou";
            _about.TargetApplication = "";   // current only applies to artwork, lyrics or instant messenger name that appears in the provider drop down selector or target Instant Messenger
            _about.Type = PluginType.General;
            _about.VersionMajor = 1;  // your plugin version
            _about.VersionMinor = 0;
            _about.Revision = 1;
            _about.MinInterfaceVersion = MinInterfaceVersion;
            _about.MinApiRevision = MinApiRevision;
            _about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents);
            _about.ConfigurationPanelHeight = 40;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function

            CreateMenuItem();
            return _about;
        }

        public bool Configure(IntPtr panelHandle)
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
            // panelHandle will only be set if you set about.ConfigurationPanelHeight to a non-zero value
            // keep in mind the panel width is scaled according to the font the user has selected
            // if about.ConfigurationPanelHeight is set to 0, you can display your own popup window
            if (panelHandle != IntPtr.Zero)
            {
                Panel configPanel = (Panel)Panel.FromHandle(panelHandle);
                Label lblLaunchStartup = new Label();
                lblLaunchStartup.AutoSize = true;
                lblLaunchStartup.Location = new Point(0, 0);
                lblLaunchStartup.Text = "launch on startup:";
                CheckBox cbLaunchStartup = new CheckBox();
                cbLaunchStartup.AutoSize = true;
                cbLaunchStartup.Location = new Point(0, 0);
                cbLaunchStartup.Text = "Launch on Startup";
                //TextBox textBox = new TextBox();
                //textBox.Bounds = new Rectangle(60, 0, 100, textBox.Height);
                configPanel.Controls.AddRange(new Control[] { cbLaunchStartup });
            }
            return false;
        }

        // called by MusicBee when the user clicks Apply or Save in the MusicBee Preferences screen.
        // its up to you to figure out whether anything has changed and needs updating
        public void SaveSettings()
        {
            // save any persistent settings in a sub-folder of this path
            string dataPath = mbApiInterface.Setting_GetPersistentStoragePath();
        }

        // MusicBee is closing the plugin (plugin is being disabled by user or MusicBee is shutting down)
        public void Close(PluginCloseReason reason)
        {
            _timer.Enabled = false;
        }

        // uninstall this plugin - clean up any persisted files
        public void Uninstall()
        {
        }

        // receive event notifications from MusicBee
        // you need to set about.ReceiveNotificationFlags = PlayerEvents to receive all notifications, and not just the startup event
        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // Debug.WriteLine($"Processing msg:{type}, timer enabled at start: {_timer.Enabled}");
            // perform some action depending on the notification type
            switch (type)
            {
                case NotificationType.PluginStartup:
                    // perform startup initialisation
                    _timer = new Timer(250);
                    _timer.Elapsed += _timer_Elapsed;
                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                            _timer.Start();
                            break;
                    }
                    //if (ChapterListMB.Properties.Settings.Default.StartWithMusicBee)
                    //    OnMenuClicked(null, null);
                    // ShowPluginWindow();
                    break;
                case NotificationType.TrackChanged:
                    SetTrack();
                    break;
                case NotificationType.PlayingTracksChanged: // copied from trackchanged
                    SetTrack();
                    break;
                case NotificationType.TrackChanging:
                    if (!_timer.Enabled) 
                        _timer.Stop();
                    break;
                case NotificationType.MusicBeeStarted:
                    break;
                case NotificationType.PlayStateChanged:
                    if (_track == null)
                        return;
                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                            // _mainForm.SetPlaying(true);
                            if (!_timer.Enabled) 
                                _timer.Start();
                            break;
                        default:
                            // _mainForm.SetPlaying(false);
                            if (_timer.Enabled) 
                                _timer.Stop();
                            break;
                    }
                    break;
            }
            // Debug.WriteLine($"Processed msg:{type}, timer enabled: at end {_timer.Enabled}");
        }

        private void SetTrack()
        {
            _currentChapter = null;
            _track = GetTrack();
            if (_mainForm == null || _mainForm.IsDisposed)
                return;
            RepeatSection.Clear();
            if (_timer.Enabled)
                _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_mainForm == null || _mainForm.IsDisposed)
                return;
            int playerPosition = mbApiInterface.Player_GetPosition();
            if (nextTimerEventPositionRequest > 0)
            {
                int jumpTo = nextTimerEventPositionRequest;
                nextTimerEventPositionRequest = 0; // setting it to 0 here to allow for possible setting if need to be postponed again
                SetTrackPosition(this, jumpTo);
                return;
            }
            try
            {
                if (_mainForm?.SetTimeDelegate != null)
                    _mainForm?.Invoke(_mainForm?.SetTimeDelegate, playerPosition);
            }
            catch (Exception)
            {

            }
        }

        private Track GetTrack()
        {
            var trackInfo = new NowPlayingTrackInfo(
                mbApiInterface.NowPlaying_GetFileTag(MetaDataType.TrackTitle),
                mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Artist),
                mbApiInterface.NowPlaying_GetFileTag(MetaDataType.Album),
                new TimeSpan(0, 0, 0, 0,
                    mbApiInterface.NowPlaying_GetDuration()),
                new Uri(mbApiInterface.NowPlaying_GetFileProperty(
                    FilePropertyType.Url), UriKind.Absolute)
            );
            Track track = new Track(trackInfo);
            return track;
        }

        private void CreateMenuItem()
        {
            mbApiInterface.MB_AddMenuItem("mnuTools/" + @"SyncView", "Hotkey for SyncView", OnMenuClicked);
        }

        private void OnMenuClicked(object sender, EventArgs args)
        {
            ShowPluginWindow();
        }

        private void ShowPluginWindow()
        {
            if (_mainForm != null && !_mainForm.IsDisposed)
            {
                _mainForm.Show();
                _mainForm.Focus();
                return;
            }
            
            _mainForm = new MainForm();
            //mbApiInterface.MB_AddPanel(_mainForm.DataGridView, PluginPanelDock.);
            _mainForm.Show();
            SubscribeToEvents();
            if (mbApiInterface.Player_GetPlayState() != PlayState.Undefined)
            {
                _track = GetTrack();
                _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
            }
        }


        /* * * * * *
         *  Events *
         * * * * * */
        private void SubscribeToEvents()
        {
            _mainForm.RequestPositionEvent += SetTrackPosition;
            _mainForm.RequestPlayFileEvent += PlayTrack;
            _mainForm.RequestPlayToggleEvent += TogglePlayPause;
        }

        DateTime lastRequestedTrackTime = DateTime.Now.Date;
        int nextTimerEventPositionRequest = -1;

        private void PlayTrack(object sender, FileInfo file)
        {
            var uri = new System.Uri(file.FullName);
            var converted = uri.AbsoluteUri;
            lastRequestedTrackTime = DateTime.Now;
            mbApiInterface.Player_Stop();
            mbApiInterface.NowPlayingList_Clear();
            mbApiInterface.NowPlayingList_QueueNext(uri.LocalPath); // mbApiInterface.now
            mbApiInterface.Player_PlayNextTrack();            
        }


        private void SetTrackPosition(object sender, int position)
        {
            // if we have just changed the track wait for the next timer event to change the track time
            if (DateTime.Now - lastRequestedTrackTime < new TimeSpan(0, 0, 0, 0, 600))
            {
                nextTimerEventPositionRequest = position;
            }
            else
            {
                mbApiInterface.Player_SetPosition(position);
                var state = mbApiInterface.Player_GetPlayState();
                if (state != PlayState.Playing)
                {
                    mbApiInterface.Player_PlayPause();
                }
            }
        }

        private void TogglePlayPause(object sender, int position)
        {
            var ret = mbApiInterface.Player_PlayPause();
        }
    }
}