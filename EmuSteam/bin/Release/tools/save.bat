echo off
megamkdir.exe /Root/EmuSteam_Backups -u %1 -p %2
megarm.exe "/Root/EmuSteam_Backups/%3" -u %1 -p %2
megaput.exe --path /Root/EmuSteam_Backups "%3" -u %1 -p %2