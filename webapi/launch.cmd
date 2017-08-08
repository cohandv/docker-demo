@echo off
powershell "Get-Process | Where-Object {$_.Path -like \"*dotnet*\"} | Stop-Process"
dotnet webapi.dll
