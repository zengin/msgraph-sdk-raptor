$content = Get-Content build.gradle -Raw;
$content = $content -replace "api 'com\.microsoft\.graph:microsoft-graph-core:\d\.\d\.\d'", "implementation name: 'msgraph-sdk-java-core'";
$flatDirRef = "mavenCentral()`r`n    flatDir {`r`n        dirs '$Env:CORE_PATH/build/libs'`r`n    }"
$content = $content -replace "mavenCentral\(\)", $flatDirRef
Set-Content -Value $content -Path build.gradle