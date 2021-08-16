# ZplDialogEditor
A patch for the DialogEditor.dll plugin to run on release builds of Zeus.

## Usage
- Somehow obtain `DialogEditor.dll`, hint: Linux IDE Red builds.
- Download Patcher.NET.zip (or compile it from source code)
- Install [Zeus Plugin Loader](https://github.com/ZeusPlugins/ZeusPluginLoader/releases/latest) if you haven't yet.
- on Windows, drag and drop the DialogEditor dll file onto the Patcher.
- on Linux, run `mono /path/to/Patcher/exe /path/to/DialogEditor/dll` from the terminal.
- you can also supply the version you want to use as the second optional argument.
- Default version: `2.3.3.574`
- Then copy DialogEditor.modified.dll into the IDE folder and enjoy!
- The `DialogEditor` plugin should appear in `Loading Plugins...` on IDE bootup:

![image](https://user-images.githubusercontent.com/33228822/129600778-53de520d-9747-4f03-9d83-6ca284c2dd5f.png)

- Press 'Control Shift E' to bring up the UI designer.
