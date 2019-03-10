using NAudio.Wave;
using SoundPull.SoundCloud;
using System;

namespace SoundPull.Tools
{
    /// <summary>
    /// Used to play tracks and playlists pulled from SoundPull using NAudio.
    /// It has play and pause functions, as well as a volume function.
    /// It can also retrieve the current position of your song (minutes:seconds) and the duration of your song (minutes:seconds)
    /// </summary>
    public class SoundCloudPlayer
    {
        private readonly WaveOutEvent player = new WaveOutEvent();
        private readonly string clientID;

        private SoundCloudTrack track;
        private SoundCloudPlaylist playlist;

        private int playlistTrackPosition;

        public enum Loop
        {
            Track,
            Playlist,
            None
        }
        public Loop loopType = Loop.None;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="track"></param>
        /// <param name="clientID"></param>
        public SoundCloudPlayer(SoundCloudTrack track, string clientID)
        {
            this.clientID = clientID;
            this.track = track;
            Init(track.stream_url + "?client_id=" + clientID);
            player.PlaybackStopped += Playback_Stopped;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="clientID"></param>
        public SoundCloudPlayer(SoundCloudPlaylist playlist, string clientID)
        {
            this.clientID = clientID;
            this.playlist = playlist;
            track = playlist.tracks[0];
            Init(track.stream_url + "?client_id=" + clientID);
            player.PlaybackStopped += Playback_Stopped;
        }

        /// <summary>
        /// Set a new track for the player.
        /// </summary>
        /// <param name="track"></param>
        public void SetTrack(SoundCloudTrack track, bool play)
        {
            if (playlist == null)
            {
                player.Dispose();
                this.track = track;
                Init(track.stream_url + "?client_id=" + clientID);

                if (play)
                {
                    player.Play();
                }
            }
        }

        /// <summary>
        /// Set a new playlist for the player.
        /// </summary>
        /// <param name="playlist"></param>
        public void SetPlaylist(SoundCloudPlaylist playlist, bool play)
        {
            if (track == null)
            {
                player.Dispose();
                this.playlist = playlist;
                track = playlist.tracks[0];
                Init(track.stream_url + "?client_id=" + clientID);

                if (play)
                {
                    player.Play();
                }
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
        /// Set the loop type.
        /// </summary>
        /// <param name="loop"></param>
        public void SetLoop(Loop loop)
        {
            loopType = loop;
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
        /// Get the current playing track.
        /// </summary>
        /// <returns></returns>
        public SoundCloudTrack GetPlayingTrack()
        {
            return track;
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

            if (GetState() == PlaybackState.Stopped)
            {
                return "0:00";
            }
            else
            {

                double currentMs = player.GetPosition() * 1000.0 / player.OutputWaveFormat.BitsPerSample / player.OutputWaveFormat.Channels * 8 / player.OutputWaveFormat.SampleRate;
                int currentSeconds = Convert.ToInt32(currentMs / 1000);
                TimeSpan timeSpan = TimeSpan.FromSeconds(currentSeconds);

                return timeSpan.ToString(@"m\:ss");
            }
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

        /// <summary>
        /// Gets the current loop type.
        /// </summary>
        /// <returns></returns>
        public Loop GetLoop()
        {
            return loopType;
        }

        private void Playback_Stopped(object sender, StoppedEventArgs e)
        {
            if (loopType == Loop.Track)
            {
                player.Dispose();
                Init(track.stream_url + "?client_id=" + clientID);
                player.Play();
            }
            else if (loopType == Loop.Playlist && playlist != null)
            {
                Next(true);
            }
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
