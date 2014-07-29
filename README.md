EditorConfig for Visual Studio
============

[![Build status](https://ci.appveyor.com/api/projects/status/27hvb1s209u0a7xp)](https://ci.appveyor.com/project/madskristensen/editorconfig)

**Note: This extension is unpublished on the VS Gallery.**

Gives syntax highlighting and Intellisense for .editorconfig files  

This is meant as a pull request to https://github.com/editorconfig/editorconfig-visualstudio  


## Screenshot:  

![screenshot](https://raw.githubusercontent.com/madskristensen/EditorConfig/master/art/screenshot.png)

I can't compile your project, since I don't have VS 2010 installed.

This code should be Visual Studio 2010+ compatible though. All you have to do 
is to copy over the file `EditorConfigTextViewCreationListener.cs` and the folders `Classify`, `Completion` and `ContentType` to 
the .editorconfig VS extension project and then add the missing references.

Sorry for such a lame pull request
