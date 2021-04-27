# Wipe Clipper
An ACT plugin for FF14 that takes Twitch clips of raid wipes and posts them on Discord.

## Features
- Automatic clipping of multiple Twitch streams on raid wipes.
- Automatic posting of said clips to user-designated Discord channels, along with a pull time and pull number.
- Summaries of a raid that include some statistics and a plot that can be sent to a Discord channel.
- Add breaks to the plot that indicate where you took a break, in order to see how that impacts pull times.
- Choose which zone the plugin takes clips in using Regex.
- Manual stream clipping via FF14's chat (that anyone, be it from your party, FC or linkshells can use, so don't set it to common words) with a custom keyword using Regex.
- Ability to differentiate between good / bad pulls based on pull time using a red / green message color.

Examples:

![](https://i.imgur.com/sFC7Jnm.png)
![](https://i.imgur.com/0CUHhbo.png)

## Download
For the latest version please see [releases.](https://github.com/Mor3x/Wipe-Clipper/releases)

## Installation instructions
After downloading the .dll, place it in the plugin folder of ACT. This is usually under %appdata%/Advanced Combat Tracker/Plugins.
It's now ready to run. Please note that ACT might prompt you to unblock the dll, simply click "Yes".


## Setup 
For the setup guide please refer to the [wiki.](https://github.com/Mor3x/Wipe-Clipper/wiki)

## Things to keep in mind
- The plugin only considers a pull valid if it was started with a countdown, "Engage" being the start of the pull.
- No entry for "Allowed zone" means the plugin will work during any raid.
- If you have 2 sessions of raids without an ACT restart in between, you need to click Reset Pulls, otherwise 
the pulls from the last session will still count towards the pull number and on the summary.
- Currently only english, german and french game languages are supported.

## Questions and feature suggestions
If you have any questions, bug reports and/or feature requests, please submit them [here](https://github.com/Mor3x/Wipe-Clipper/issues),
or message me on Discord - Morex#9341
