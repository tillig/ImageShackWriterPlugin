#ImageShack Upload Plugin for Windows Live Writer

##Description

[ImageShack](http://www.imageshack.us) is a great free service that will host images for you. [Windows Live Writer](http://windowslivewriter.spaces.live.com/) is a great free tool for authoring blog posts. Say you're writing a blog entry and you want to put an image in your blog that will be hosted by ImageShack. What do you do?


If you're using the ImageShack Upload Plugin for Windows Live Writer, it's as simple as selecting the image from your disk. The upload plugin will automatically post the image to ImageShack and insert it into your blog post.

##License

Copyright 2009 ImageShackWriterPlugin Contributors

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

[http://www.apache.org/licenses/LICENSE-2.0](http://www.apache.org/licenses/LICENSE-2.0)

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

##Requirements

This product requires [Windows Live Writer](http://windowslivewriter.spaces.live.com/) and an account on [ImageShack](http://www.imageshack.us).

##Installation

1. Make sure all instances of Windows Live Writer are closed.
1. Copy the ImageShackWriterPlugin.dll assembly into your Windows Live Writer plug-ins folder.  This is typically something like: `C:\Program Files\Windows Live\Writer\Plugins`
1. Start Windows Live Writer.
1. From the "Tools" menu, select "Options."
1. On the left side of the options menu, select "Plug-ins."
1. From the list of plugins, select "ImageShack Upload" and click the "Options" button in the "Plug-in details" panel.
1. The options dialog that appears has a link to get your ImageShack registration code. Click that link.
1. If you are not logged into ImageShack, you will be prompted to log in.
1. You will be taken to the ImageShack user preferences page. Scroll down about 1/3 of the way and find the section marked "My ImageShack Registration Code." Copy this value and paste it in the ImageShack Uploader Options dialog.
1. Click OK to save your options and close the ImageShack Uploader Options dialog.
1. Click OK on the main Windows Live Writer options dialog to close it.

##Usage

1. Open an instance of Windows Live Writer.
1. When working on a blog post, open the "Insert" menu and select "ImageShack Upload..."
1. Select the image file to upload. The plugin will upload it and insert the appropriate HTML into your blog post.

##Additional Info

A full feature list, version history, known issues, etc. are all available on the [ImageShackWriterPlugin project wiki](https://github.com/tillig/ImageShackWriterPlugin/wiki).
