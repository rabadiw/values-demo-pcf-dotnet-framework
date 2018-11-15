# msbuild values-demo-pcf-dotnet-framework.sln ^
#   /p:Configuration=Release ^ 
#   /p:DeployOnBuild=True ^
#   /p:DeployDefaultTarget=WebPublish ^
#   /p:WebPublishMethod=FileSystem ^
#   /p:DeleteExistingFiles=True ^
#   /p:publishUrl=@(echo $pwd/publish)

$env:Path+=";C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin"
$csproj = @(Get-ChildItem .\*.csproj)[0].FullName
$target = "$PWD\publish"

Write-Output "Publishing '$csproj' to '$target'"
MSBuild $csproj `
  /p:Configuration=Release `
  /p:Platform=AnyCPU `
  /t:WebPublish `
  /p:WebPublishMethod=FileSystem `
  /p:DeleteExistingFiles=True `
  /p:publishUrl=$target

cf push -f manifest.yml -p $target