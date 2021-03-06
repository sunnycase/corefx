@if "%_echo%" neq "on" echo off

:: To run tests outside of MSBuild.exe
:: %1 is the path to the tests\<OSConfig> folder
:: %2 is the path to the packages folder

pushd %1

FOR /D %%F IN (*.Tests) DO (
	IF EXIST %%F\netcoreapp1.0 (
		pushd %%F\netcoreapp1.0
		CALL RunTests.cmd %2
		popd
	)
)

popd
