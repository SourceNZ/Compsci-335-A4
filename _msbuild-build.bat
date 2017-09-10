REM MSBuild.exe _.sln /t:clean /m

REM pause

SET EnableNuGetPackageRestore=true

MSBuild.exe WebApplication1.sln /t:build /m

pause
