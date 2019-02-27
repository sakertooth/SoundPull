# SoundPull
<img src="logo.png" width="128" height="128"/>

![build](https://img.shields.io/appveyor/ci/sakertooth/soundpull.svg)
![issues](https://img.shields.io/github/issues/sakertooth/soundpull.svg)
[![nuget downloads](https://img.shields.io/nuget/dt/Saker.SoundPull.svg)](https://www.nuget.org/packages/Saker.SoundPull/)
\
![mit](https://img.shields.io/github/license/sakertooth/soundpull.svg)
[![Saker.SoundPull](https://img.shields.io/nuget/v/Saker.SoundPull.svg)](https://www.nuget.org/packages/Saker.SoundPull/)

With SoundPull, you are able to pull user information, track information, and playlist information, along with others from SoundCloud with the help of Json.NET.

## Background Info
In SoundCloud, most information that can be pulled is in Json format, meaning that SoundPull uses Json.Net in order to deserialize the information pulled from the API resolve endpoint into objects that SoundPull can read. 

## Installation

Installing SoundPull and Json.Net is very simple.

```
Install-Package Saker.SoundPull
Install-Package Newtonsoft.Json
```

## Basic Usage

Make a new instance of SoundPullSession like so:
```c#
SoundPullSession session = new SoundPullSession("[your_client_id]");
```

From there, you can pull almost anything from SoundCloud (more coming soon):
```c#
SoundCloudUser user = session.GetUser("user");
string userDisplayName = user.username; //gets the users display name
```

## What now?
* [Wiki documentation](https://github.com/sakertooth/SoundPull/wiki/1.-Getting-Started)

## Help
[![tweet](https://img.shields.io/twitter/url/https/sakertooth.svg?style=social)](https://twitter.com/SakerTooth) - Tweet me any bugs!
\
\
[![twitter_follow](https://img.shields.io/twitter/follow/sakertooth.svg?style=social)](https://twitter.com/SakerTooth)
\
\
![fork](https://img.shields.io/github/forks/sakertooth/SoundPull.svg?style=social)
\
\
![star](https://img.shields.io/github/stars/sakertooth/SoundPull.svg?style=social)
### License
SoundPull is licensed under the MIT license.
