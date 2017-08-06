[System.IO.Compression.ZipFile]::ExtractToDirectory("c:\consul_0.9.0.zip", "c:\")
c:\consul.exe agent -ui -dev -bind 0.0.0.0 -client 0.0.0.0
