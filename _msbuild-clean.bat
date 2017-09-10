REM MSBuild.exe _.sln /t:clean /m

REM pause

SET EnableNuGetPackageRestore=true

MSBuild.exe WebApplication1.sln /t:clean

rmdir /s /q WebApplication1\bin
rmdir /s /q WebApplication1\obj

pause
