# Tomb Editor
## Introduction
In the year 2000, Core Design released the Tomb Raider Level Editor for Tomb Raider IV: The Last Revelation. This came as a second disc for the PC Version of Tomb Raider: Chronicles.  It was a great gift to fans and nothing of what exists now in the community would exist had the original level editor not been released. Unfortunately, the official level editor package standalone was severely limited. It did not include tools for editing the objects, animations or sounds  It also had no way to customize the engine due to omittance of the source code. But a fan community was born.

## Why build a new editor?
Because techonology moves on. Newer versions of Windows, DirectX and hardware results in problems when running the old 1999 editor on newer PCs. As a result, I've decided to create a full clone of the original Tomb Raider level editor in C#. The objective is to replicate behavior of the original editor as accurately as possible. There have been cases where I've improved/expanded upon the original editor mechanics. This includes but is not limited to a completely new UI (User interface) that can robustly scale from 1280x1024 screen resolutions to 1920x1080 and beyond without issues.

#The Toolchain.

The Tomb Editor is part of a wider set of tools:

- Tomb Editor : The modern heart of the editor where users can build new levels for Lara Croft.
- Wadtool: A robust tool used to add/remove objects from a level's working files; add animations and edit the meshes and properties of objects inside a new "WAD2" format.
- Soundtool : Previously a command prompt process: a new program that allows the user to build new sounds into their levels.
- Tomb IDE : The launcher and setup utility for new projects and editing the game's scripttable elements.

## Disclaimer
I don't work for/have ever worked for Core Design, Eidos Interactive or Square Enix. This is a hobby project. I don't want this code to be sold since Tomb Raider is a registered trademark of Square Enix. The code is currently open source to allow more people to improve the editor and for study purposes. I'm not responsible for illegal use of this source code. This source code is released as-is and it's maintained by me and by other (not paid) contributors in our free time.

## Thanks to...
I would like to to thank all the following people that contributed to the development of this new Tomb Raider level editor. (not in order of importance :)) They helped me with tips, knowledge, bug reporting, suggestions, direct development.

* Banderi
* Caesum
* Dustie
* Gancian
* Gemini
* Gh0stBlade
* JMN
* Joey79100
* leveldesigner1
* Lore
* Lwmte
* Monsieur Z
* Nickelony
* Raildex
* stohrendorf
* Stranger1992
* teme9
* TeslaRus
* Titak
* TRTombLevBauer
* XProger

## External libraries
[A list can be found here.](ExternalResources.md)
