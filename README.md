# SoundPull
<img src="logo.png"/>

[![build](https://img.shields.io/appveyor/ci/sakertooth/soundpull.svg)](https://ci.appveyor.com/project/sakertooth/soundpull)
[![issues](https://img.shields.io/github/issues/sakertooth/soundpull.svg)](https://github.com/sakertooth/SoundPull/issues)
[![nuget downloads](https://img.shields.io/nuget/dt/Saker.SoundPull.svg)](https://www.nuget.org/packages/Saker.SoundPull/)
\
[![Saker.SoundPull](https://img.shields.io/nuget/v/Saker.SoundPull.svg)](https://www.nuget.org/packages/Saker.SoundPull/)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/2116a4e0e7544663bb8c397fa2505d37)](https://www.codacy.com/app/sakertooth/SoundPull?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=sakertooth/SoundPull&amp;utm_campaign=Badge_Grade)

![](https://github.com/sakertooth/SoundPull/blob/master/previews/pulltrackgif)

## Getting Started
* With SoundPull, you are able to pull user information, track information, and playlist information, along with others from SoundCloud with the help of Json.NET.

* In addition, you can also play tracks and playlists from SoundPull using its player class. This is made possible by the use of NAudio.

* In SoundCloud, most information that can be pulled is in JSON format, meaning that SoundPull uses Json.Net in order to deserialize the information pulled from the API resolve endpoint into objects that SoundPull can read. 

## Installation

* Installing SoundPull and Json.Net is very simple.

```
Install-Package Saker.SoundPull
Install-Package Newtonsoft.Json
```
* To use the Player class, you must have NAudio as well.

```
Install-Package NAudio
```

## Basic Usage

* Make a new instance of a SoundPullSession like so:
```c#
SoundPullSession session = new SoundPullSession("[your_client_id]");
```

* From there, you can pull almost anything from SoundCloud (more coming soon):
```c#
SoundCloudUser user = session.GetUser("user");
string userDisplayName = user.username; //gets the users display name
```
## Snippets
![tracksnippet](https://github.com/sakertooth/SoundPull/blob/master/snippets/SoundPull/tracksnippet.PNG)
\
\
\
![usersnippet](https://github.com/sakertooth/SoundPull/blob/master/snippets/SoundPull/usersnippet.PNG)
\
\
\
![playlistsnippet](https://github.com/sakertooth/SoundPull/blob/master/snippets/SoundPull/playlistsnippet.PNG)
\
\
\
![querysnippet](https://github.com/sakertooth/SoundPull/blob/master/snippets/SoundPull/querysnippet.PNG)

## Features

* Uses 'sessions' that can pull users, tracks, and playlists, seamlessly
* A functional Player class that can be used to play tracks and playlists with simple methods
* No Client Secret required, only a Client ID is needed
* Uses Json.NET, one of the fastest JSON serializers known.
* Utilizing NAudio, providing a seamless player class to be used at your convenience.
* Simple volume function
* Can get the current position of your song in the format m:ss, it also allows support for your own format.
* Loop functions, allowing you to loop songs and playlists. (available in the pre release >= 1.32)
* Can search for tracks, playlists, and users.
* Can do 'all-in-one' searches

## Roadmap

* To add support for Subresources 
\
![subresourcesnippet](https://github.com/sakertooth/SoundPull/blob/master/snippets/SoundPull/subresourceroadmap.PNG)

* Generating streams, based on what accounts the user follows (https://soundcloud.com/stream)

## What now?
* [Wiki documentation](https://github.com/sakertooth/SoundPull/wiki/1.-Getting-Started)

## Socials
[![tweet](https://img.shields.io/twitter/url/https/sakertooth.svg?style=social)](https://twitter.com/intent/tweet?via=SakerTooth)
\
[![twitter_follow](https://img.shields.io/twitter/follow/sakertooth.svg?style=social)](https://twitter.com/intent/follow?screen_name=SakerTooth)
\
[![fork](https://img.shields.io/github/forks/sakertooth/SoundPull.svg?style=social)](https://github.com/sakertooth/SoundPull/fork)
\
[![star](https://img.shields.io/github/stars/sakertooth/SoundPull.svg?style=social)](https://github.com/sakertooth/SoundPull)

## Built With
* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json)
* [NAudio](https://github.com/naudio/NAudio)

## License
SoundPull is licensed under the MIT license.
\
[![mit2](https://img.shields.io/github/license/sakertooth/SoundPull.svg)](https://github.com/sakertooth/SoundPull/blob/master/LICENSE)
