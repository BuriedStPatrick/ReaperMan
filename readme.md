# ReaperMan

A commandline REAPER installation and version manager, mostly for use with portable installs.

## Motivation

I needed something to quickly switch between and manage multiple REAPER installs as well as custom scripts and configs. If you just need a package manger solution, on Windows I recommend simply using `winget`. The project is still in the early stages. And, you know, might never be finished.

## Example

> NOTE: Only basic installation is implemented so far.

Here are some examples of the aim for the project. Use `--help` in any commands scope to display available commands. Such as:

```bash
reaperman install --help
```

Examples:

```bash
# Install REAPER as portable using a silent installer
reaperman install --arch x64 --edition 645 --destination C:\temp\my-custom-install --portable --silent

# Update REAPER (not supported yet)
reaperman update

# List managed REAPER installs (not supported yet)
reaperman list

# Install SWS (not supported yet)
reaperman extension install sws

# Scan for existing installs, add to management (not supported yet)
reaperman scan
```

## Installation

> NOTE: This has only been tested on Windows as of now. It assumes you want to download the .exe installer from the website and run it as part of  the installation process. This obviously doesn't work on Linux/MacOS. I am, however, also a Linux user, so I'll maybe look into this at some point.

There's no real installation flow yet, so you'll have to compile it and add the executable to your PATH.

```bash
# Compile CLI project
cd ./ReaperMan.Cli && dotnet build -c Release

# Add to PATH (bash)
export PATH=/path/to/repo/ReaperMan.Cli/bin/Release/net6.0:$PATH

# Add to PATH (PowerShell)
$env:Path += ";C:\path\to\repo\ReaperMan.Cli\bin\Release\net6.0"
```

You can also just run it via the dotnet-runner in inside the project:

```bash
# Example, run installer, passing arguments after `--` from dotnet-runner to ReaperMan
cd ./ReaperMan.Cli && dotnet run -- install --arch x64 --edition 645 --destination C:\temp\my-custom-install --portable --silent
```

## Configuration

You can configure the logging level as well as a custom download path inside `ReaperMan.Cli/reaper.yaml`. (Under the `bin/(Debug|Release)/net6.0` folder if you're using the compiled version, though).

### Downloads

By default, downloads are placed in the `%USERPROFILE%/Downloads` folder on Windows. You can change this by adding the following to `reaper.yaml`.

```yaml
DownloadsPath: C:\my\custom\downloads-folder
```