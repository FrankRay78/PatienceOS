
:: x64 Visual Studio 2022 Developer Command Prompt
@call "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat"

:: Custom toolpaths
@call "Y:\build\setpath.cmd"

:: Change directory
Y:
cd build