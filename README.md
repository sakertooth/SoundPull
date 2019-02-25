# SoundPull
With SoundPull, you are able to pull user information, track information, and playlist information, along with others from SoundCloud.

# To Begin
To bein using SoundPull, you would want to have a client id from the SoundCloud API, in other words, make an app on SoundCloud and get its client id.

From there, you want to start a new session with a new seed and your client id.

```c#

int seed = 0;
SoundPull soundPull = new SoundPull(seed, [client_id]);

```


