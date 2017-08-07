FROM microsoft/aspnetcore
WORKDIR /app
COPY ./publish .
COPY ./consul-template .
COPY launch.cmd .
#ENTRYPOINT ["dotnet", "webapi.dll"]
ENTRYPOINT c:\app\consul-template.exe -config "config.hcl"
