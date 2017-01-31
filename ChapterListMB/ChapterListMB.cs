﻿using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using ChapterListMB;
using Timer = System.Timers.Timer;

namespace MusicBeePlugin
{
    public partial class Plugin
    {
        private MusicBeeApiInterface mbApiInterface;
        private PluginInfo about = new PluginInfo();
        private MainForm _mainForm;
        private Track _track;

        public PluginInfo Initialise(IntPtr apiInterfacePtr)
        {
            mbApiInterface = new MusicBeeApiInterface();
            mbApiInterface.Initialise(apiInterfacePtr);
            about.PluginInfoVersion = PluginInfoVersion;
            about.Name = "Chapter List | MB";
            about.Description = "Creates chapters to jump to a position in a track.";
            about.Author = "Eincrou";
            about.TargetApplication = "";   // current only applies to artwork, lyrics or instant messenger name that appears in the provider drop down selector or target Instant Messenger
            about.Type = PluginType.General;
            about.VersionMajor = 1;  // your plugin version
            about.VersionMinor = 0;
            about.Revision = 1;
            about.MinInterfaceVersion = MinInterfaceVersion;
            about.MinApiRevision = MinApiRevision;
            about.ReceiveNotifications = (ReceiveNotificationFlags.PlayerEvents);
            about.ConfigurationPanelHeight = 0;   // height in pixels that musicbee should reserve in a panel for config settings. When set, a handle to an empty panel will be passed to the Configure function

            CreateMenuItem();
            return about;
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
                Label prompt = new Label();
                prompt.AutoSize = true;
                prompt.Location = new Point(0, 0);
                prompt.Text = "prompt:";
                TextBox textBox = new TextBox();
                textBox.Bounds = new Rectangle(60, 0, 100, textBox.Height);
                configPanel.Controls.AddRange(new Control[] { prompt, textBox });
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
        }

        // uninstall this plugin - clean up any persisted files
        public void Uninstall()
        {
        }

        // receive event notifications from MusicBee
        // you need to set about.ReceiveNotificationFlags = PlayerEvents to receive all notifications, and not just the startup event
        public void ReceiveNotification(string sourceFileUrl, NotificationType type)
        {
            // perform some action depending on the notification type
            switch (type)
            {
                case NotificationType.PluginStartup:
                    // perform startup initialisation
                    if (_mainForm != null) return;
                   // _mainForm = new MainForm();
                   // _mainForm.Show();
                    switch (mbApiInterface.Player_GetPlayState())
                    {
                        case PlayState.Playing:
                        case PlayState.Paused:
                        case PlayState.Loading:
                            // ...
                            break;
                    }
                    break;
                case NotificationType.TrackChanged:
                    if (_mainForm == null) return;
                    _track = GetTrack();
                    _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
                    break;
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
            var track = new Track(trackInfo);
            return track;
        }

        // return an array of lyric or artwork provider names this plugin supports
        // the providers will be iterated through one by one and passed to the RetrieveLyrics/ RetrieveArtwork function in order set by the user in the MusicBee Tags(2) preferences screen until a match is found
        public string[] GetProviders()
        {
            return null;
        }

        // return lyrics for the requested artist/title from the requested provider
        // only required if PluginType = LyricsRetrieval
        // return null if no lyrics are found
        public string RetrieveLyrics(string sourceFileUrl, string artist, string trackTitle, string album, bool synchronisedPreferred, string provider)
        {
            return null;
        }

        // return Base64 string representation of the artwork binary data from the requested provider
        // only required if PluginType = ArtworkRetrieval
        // return null if no artwork is found
        public string RetrieveArtwork(string sourceFileUrl, string albumArtist, string album, string provider)
        {
            //Return Convert.ToBase64String(artworkBinaryData)
            return null;
        }

        private void CreateMenuItem()
        {
            mbApiInterface.MB_AddMenuItem("mnuTools/" + @"Chapter List | MB", "Hotkey for CLMB", OnMenuClicked);
        }

        private void OnMenuClicked(object sender, EventArgs args)
        {
            _mainForm = new MainForm();
            _mainForm.Show();
            SubscribeToEvents();
            if (mbApiInterface.Player_GetPlayState() != PlayState.Undefined)
            {
                _track = GetTrack();
                _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
            }
        }

        private void SubscribeToEvents()
        {
            _mainForm.SelectedItemDoubleClickedRouted += MainFormOnSelectedItemDoubleClickedRouted;
            _mainForm.AddChapterButtonClickedRouted += MainFormOnAddChapterButtonClickedRouted;
            _mainForm.RemoveChapterButtonClickedRouted += MainFormOnRemoveChapterButtonClickedRouted;
            _mainForm.ChangeChapterRequested += MainFormOnChangeChapterRequested;
        }


        private void MainFormOnSelectedItemDoubleClickedRouted(object sender, Chapter chapter)
        {
            mbApiInterface.Player_SetPosition(chapter.Position);
        }
        private void MainFormOnAddChapterButtonClickedRouted(object sender, string newChapterTitle)
        {
            var currentPosition = mbApiInterface.Player_GetPosition();
            if (newChapterTitle == string.Empty)
                _track.ChapterList.CreateNewChapter(currentPosition);
            else
            _track.ChapterList.CreateNewChapter(newChapterTitle, currentPosition);
            _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
        }
        private void MainFormOnRemoveChapterButtonClickedRouted(object sender, Chapter e)
        {
            _track.ChapterList.RemoveChapter(e);
            _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
        }
        private void MainFormOnChangeChapterRequested(object sender, ChapterChangeEventArgs e)
        {
            _track.ChapterList.ChangeChapter(e.ChapterToChange, new Chapter(e.Position, e.Title));
           _mainForm.Invoke(_mainForm.UpdateTrackDelegate, _track);
        }
    }
}