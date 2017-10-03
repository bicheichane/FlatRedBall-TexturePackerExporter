# FlatRedBall-TexturePackerExporter

Allows TexturePacker spritesheet integration for the FlatRedBall game engine

* FlatRedBall custom exporter setup for TexturePacker 
* File build tool for Glue 

Usage:
#1 Set the TexturePackerCustomExporters folder as your TexturePacker's Exporter Directory

File -> Preferences -> Exporter Directory (don't forget to restart the application after applying that change)

#2 Choose the FlatRedBall data file format when publishing spritesheet.

#3 Add the TexturePackerToSpritesheetAcx.exe as a file build tool on Glue.

Settings -> FileBuildTools -> Add new build tool

SourceFileType: xml

DestinationFileType: achx

IncludeDestination: True


More info on:

http://flatredball.com/documentation/tools/glue-reference/menu/settings/glue-reference-menu-settings-file-build-tools/

#4 Add the spritesheet and xml generated by TexturePacker to Glue. On adding the xml, select the file build tool you've just setup.

#5 ???

#6 Profit!
