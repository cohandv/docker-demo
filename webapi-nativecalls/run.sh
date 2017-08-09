#!/bin/bash
cd /app && dotnet restore && dotnet publish -o /app/publish && cd /app/publish && dotnet webapi.dll --urls http://0.0.0.0:5000 && sleep infinity
