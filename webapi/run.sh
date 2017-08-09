#!/bin/bash
cd /app && dotnet restore && dotnet publish -o /app/publish && cd /app/publish && /app/consul-template/consul-template -config "/app/consul-template/config.hcl"  && sleep infinity
