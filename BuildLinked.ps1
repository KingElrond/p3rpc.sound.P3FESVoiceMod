# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/p3rpc.sound.P3FESVoiceMod/*" -Force -Recurse
dotnet publish "./p3rpc.sound.P3FESVoiceMod.csproj" -c Release -o "$env:RELOADEDIIMODS/p3rpc.sound.P3FESVoiceMod" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location