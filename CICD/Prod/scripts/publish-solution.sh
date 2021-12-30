#!/bin/bash
set -e

. ~/.bashrc

dotnet publish WebUI.csproj -o bin/WebUI -r linux-x64 --self-contained true --configuration Release
