XCOPY "..\Client\bin\Debug\net9.0\%ProjectName%.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Client\bin\Debug\net9.0\%ProjectName%.Client.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Server\bin\Debug\net9.0\%ProjectName%.Server.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Server\bin\Debug\net9.0\%ProjectName%.Server.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Shared\bin\Debug\net9.0\%ProjectName%.Shared.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Shared\bin\Debug\net9.0\Trifoia.Module.VideoPlayer.Shared.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y
XCOPY "..\Server\wwwroot\*" "..\..\oqtane.framework\Oqtane.Server\wwwroot\" /Y /S /I
XCOPY "..\Client\bin\Debug\net9.0\MudBlazor.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net9.0\" /Y

