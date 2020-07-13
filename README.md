# computer-remote
A simple prototype project with a goal of making it easy to remotely control a windows machine (simple commands). This project is a multi-module project containing a windows console server, and a Xamarin client for Android. There is also a command line client for testing.


## Usage
This is purely a prototype, so you will have to build the project yourself using Visual Studio. The server is installed on your windows machine, and the Xamarin client is installed on your phone. Once installed, you pair the phone with your computer using the displayed QR code.

## Features
### Audio Control
1. Change Volume of primary audio device
2. Mute/Unmute primary audio device
3. Play Next Track (Compatible with Spotify and other media players)
4. Play Previous Track (Compatible with Spotify and other media players)
5. Play/Pause Current Track (Compatible with Spotify and other media players)
### System Control
1. Restart computer
2. Shutdown computer
### Remote Control (WIP)
Remote control is work in progress. Currently, the remote control feature simply captures the screen and streams it to the android device. It's laggy as work is needed to implement proper video transcoding.

## Future Work
I am unlikely to continue working on this actively (or at all) as I like making throwaway prototypes.
