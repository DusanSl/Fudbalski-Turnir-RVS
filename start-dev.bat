@echo off
start "Servis" cmd /k "cd "izvorni kod\SlojServisa" && dotnet watch run"
start "Prezentacija" cmd /k "cd "izvorni kod\PrezentacioniSloj" && dotnet watch run"