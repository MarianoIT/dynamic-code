$file = Get-Content "Program.cs"

$base64 = [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes($file))

"public class Code { public static string value = """ + $base64 + """; }" | out-file ..\DynamicCodeApp\Code.cs