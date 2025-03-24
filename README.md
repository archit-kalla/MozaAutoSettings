# MozaAutoSettings

## A Tool to automatically load wheelbase profiles per game/program for MOZA wheels

[currentSettings]: readme_images/CurrentSettings.png
[profilesPage]: readme_images/profilesPage.png

![currentSettings]
![profilesPage]

### Requirements:
- .NET 10 Desktop Runtime for x64 [https://dotnet.microsoft.com/en-us/download/dotnet/10.0]
- Visual Studio C++ Redist v17 for x64 [https://aka.ms/vs/17/release/vc_redist.x64.exe]



## KNOWN ISUES:
- ffb intensity set to 0 when applying, can be resolved by manually adjusting
- 100hz eq filter does not work?
- road sensitvity not enabled 
- speed depended damping minimum is 100 although API states 0 is possible
