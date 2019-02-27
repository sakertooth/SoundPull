using NAudio.Wave;
using SoundPull.SoundCloud;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoundPull.Player
{
    /// <summary>
    /// Used to play tracks and playlists pulled from SoundPull using NAudio.
    /// It has play and pause functions, as well as a volume function.
    /// It can also retrieve the current position of your song (minutes:seconds) and the duration of your song (minutes:seconds)
    /// </summary>
    public class SoundCloudPlayer
    {
        private WaveOutEvent player = new WaveOutEvent();
        public string clientID;

        private SoundCloudTrack track;
        private SoundCloudPlaylist playlist;

        public int playlistTrackPosition = 0;

        public SoundCloudPlayer(SoundCloudTrack track, string clientID)
        {
            this.clientID = clientID;
            this.track = track;
            Init(track.stream_url + "?client_id=" + clientID);
        }

        public SoundCloudPlayer(SoundCloudPlaylist playlist, string clientID)
        {
            this.clientID = clientID;
            this.playlist = playlist;
            track = playlist.tracks[0];
            Init(track.stream_url + "?client_id=" + clientID);
        }

        /// <summary>
        /// Set a new track for the player.
        /// </summary>
        /// <param name="track"></param>
        public void SetTrack(SoundCloudTrack track)
        {
            if (playlist == null)
            {
                player.Dispose();
                this.track = track;
                Init(track.stream_url + "?client_id=" + clientID);
            }
        }

        /// <summary>
        /// Set a new playlist for the player.
        /// </summary>
        /// <param name="playlist"></param>
        public void SetPlaylist(SoundCloudPlaylist playlist)
        {
            if (track == null)
            {
                player.Dispose();
                this.playlist = playlist;
                track = playlist.tracks[0];
                Init(track.stream_url + "?client_id=" + clientID);
            }
        }

        /// <summary>
        /// Change the track in the playlist if specified.
        /// </summary>
        /// <param name="trackIndex"></param>
        public void SetTrackInPlaylist(int trackIndex)
        {
            if (playlist != null)
            {
                player.Dispose();
                track = playlist.tracks[trackIndex];
                Init(track.stream_url + "?client_id=" + clientID);
            }
        }

        /// <summary>
        /// Starts playing the track.
        /// </summary>
        public void Play()
        {
            player.Play();
        }

        /// <summary>
        /// Stops the track.
        /// </summary>
        public void Stop()
        {
            player.Stop();
        }

        /// <summary>
        /// Pauses the track.
        /// </summary>
        public void Pause()
        {
            player.Pause();
        }

        /// <summary>
        /// If a playlist is set, move to the next track.
        /// <paramref name="play"/>
        /// </summary>
        public void Next(bool play)
        {
            if (playlist != null)
            {
                if (playlistTrackPosition < playlist.track_count)
                {
                    playlistTrackPosition++;

                    player.Dispose();
                    track = playlist.tracks[playlistTrackPosition];
                    Init(track.stream_url + "?client_id=" + clientID);
                }
                else if (playlistTrackPosition >= playlist.track_count)
                {
                    playlistTrackPosition = 0;

                    player.Dispose();
                    track = playlist.tracks[playlistTrackPosition];
                    Init(track.stream_url + "?client_id=" + clientID);
                }

                if (play)
                {
                    player.Play();
                }
            }
        }

        /// <summary>
        /// If a playlist is set, go back to the previous track.
        /// <paramref name="play"/>
        /// </summary>
        public void Previous(bool play)
        {
            if (playlist != null)
            {
                if (playlistTrackPosition > 0)
                {
                    playlistTrackPosition--;

                    player.Dispose();
                    track = playlist.tracks[playlistTrackPosition];
                    Init(track.stream_url + "?client_id=" + clientID);
                }
                else if (playlistTrackPosition <= 0)
                {
                    playlistTrackPosition = playlist.track_count - 1;

                    player.Dispose();
                    track = playlist.tracks[playlistTrackPosition];
                    Init(track.stream_url + "?client_id=" + clientID);
                }

                if (play)
                {
                    player.Play();
                }
            }
        }

        /// <summary>
        /// Set the volume.
        /// </summary>
        /// <param name="volume"></param>
        public void SetVolume(float volume)
        {
            player.Volume = volume;
        }

        /// <summary>
        /// Get the volume.
        /// </summary>
        /// <returns></returns>
        public float GetVolume()
        {
            return player.Volume;
        }

        /// <summary>
        /// Get the current state of the player.
        /// </summary>
        /// <returns></returns>
        public PlaybackState GetState()
        {
            return player.PlaybackState;
        }

        /// <summary>
        /// Gets the current position of the track and puts it in the format (m:ss)
        /// </summary>
        /// <returns></returns>
        public string GetCurrentPosition()
        {
            double currentMs = player.GetPosition() * 1000.0 / player.OutputWaveFormat.BitsPerSample / player.OutputWaveFormat.Channels * 8 / player.OutputWaveFormat.SampleRate;
            int currentSeconds = Convert.ToInt32(currentMs / 1000);
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentSeconds);
            return timeSpan.ToString(@"m\:ss");         
        }

        /// <summary>
        /// Gets the current position of the track and puts it in the format specified.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string GetCurrentPosition(string format)
        {
            double currentMs = player.GetPosition() * 1000.0 / player.OutputWaveFormat.BitsPerSample / player.OutputWaveFormat.Channels * 8 / player.OutputWaveFormat.SampleRate;
            int currentSeconds = Convert.ToInt32(currentMs / 1000);
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentSeconds);
            return timeSpan.ToString(format);
        }

        private void Init(string url)
        {
            using (var mf = new MediaFoundationReader(url))
            {
                player.Init(mf);
            }
        }
    }
}
