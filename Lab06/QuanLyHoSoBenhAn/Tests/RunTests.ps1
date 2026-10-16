$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path $PSScriptRoot
$testDatabase = 'Bai05_AppTest_' + [guid]::NewGuid().ToString('N').Substring(0, 12)
[xml]$appConfig = Get-Content -LiteralPath (Join-Path $projectRoot 'App.config')
$connection = [System.Data.SqlClient.SqlConnectionStringBuilder]::new([string]$appConfig.configuration.connectionStrings.add.connectionString)
$server = $connection.DataSource
$testFolder = Join-Path $PSScriptRoot 'Output'
New-Item -ItemType Directory -Path $testFolder -Force | Out-Null
$sqlFile = Join-Path $testFolder 'SchemaTest.sql'
$schema = [IO.File]::ReadAllText((Join-Path $projectRoot 'Database\Schema.sql')).Replace('QuanLyHoSoBenhAn_Bai05', $testDatabase)
[IO.File]::WriteAllText($sqlFile, $schema, (New-Object Text.UTF8Encoding($true)))
try {
    & sqlcmd -S $server -E -C -I -b -f 65001 -i $sqlFile
    if ($LASTEXITCODE -ne 0) { throw 'Không tạo được database kiểm thử.' }
    1..2 | ForEach-Object {
        & sqlcmd -S $server -d $testDatabase -E -C -I -b -f 65001 -i (Join-Path $projectRoot 'Database\SeedDemo.sql')
        if ($LASTEXITCODE -ne 0) { throw 'Seed kiểm thử thất bại.' }
    }
    $testAssembly = Join-Path $testFolder 'IntegrationTests.exe'
    $appAssembly = Join-Path $projectRoot 'bin\Debug\QuanLyHoSoBenhAn.exe'
    & "$env:WINDIR\Microsoft.NET\Framework64\v4.0.30319\csc.exe" /nologo "/out:$testAssembly" "/r:$appAssembly" /r:System.Data.dll /r:System.Data.DataSetExtensions.dll (Join-Path $PSScriptRoot 'IntegrationTests.cs')
    if ($LASTEXITCODE -ne 0) { throw 'Biên dịch test thất bại.' }
    Copy-Item -LiteralPath $appAssembly -Destination $testFolder -Force
    $connection.set_InitialCatalog($testDatabase)
    $appConfig.configuration.connectionStrings.add.connectionString = $connection.ToString()
    $appConfig.Save($testAssembly + '.config')
    & $testAssembly
    if ($LASTEXITCODE -ne 0) { throw 'Kiểm thử tích hợp thất bại.' }
}
finally {
    # Chỉ xóa tên database kiểm thử do chính lượt chạy này tạo, không dùng tên từ App.config.
    if ($testDatabase -notmatch '^Bai05_AppTest_[0-9a-f]{12}$') { throw 'Tên database kiểm thử không hợp lệ.' }
    & sqlcmd -S $server -d master -E -C -b -Q "IF DB_ID(N'$testDatabase') IS NOT NULL BEGIN ALTER DATABASE [$testDatabase] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$testDatabase]; END"
    if ($LASTEXITCODE -ne 0) { throw 'Không dọn được database kiểm thử.' }
}
