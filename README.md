# FlatRedBall-TexturePackerExporter

Allows TexturePacker spritesheet integration for the FlatRedBall game engine

* ~FlatRedBall custom exporter setup for TexturePacker~ : now it only uses generic XML data format from TexturePacker to support free version
* File build tool for Glue 

Usage:

#1 Choose the Generic XML data file format when publishing spritesheet.

    If you want to have animations on the final achx, just use the following naming convention for each animation frame:

    <spriteprefix><animation frame id>

    i.e. animationFrame1, animationFrame2, animationFrame3, etc..



#2 Check the option for "Trim sprite names"

#3 Build the solution and add the TexturePackerToSpritesheetAcx.exe as a file build tool on Glue.

    Settings -> FileBuildTools -> Add new build tool

    SourceFileType: xml

    DestinationFileType: achx

    IncludeDestination: True


    More info on:

    http://flatredball.com/documentation/tools/glue-reference/menu/settings/glue-reference-menu-settings-file-build-tools/

#4 Add the spritesheet and xml generated by TexturePacker to Glue. On adding the xml, select the file build tool you've just setup.

#5 ???

#6 Profit!
