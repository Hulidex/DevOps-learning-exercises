#!/bin/bash
set -e

. ~/.bashrc

# This command will
#   - Download dotnet WebAPI dependencies
#   - Build the dotnet WebAPI project
#   - Start dotnet kestrel (API REST service)
#   - Download NodeJS dependencies (Angular)
#   - Compile Angular project
#   - Start angular project (Web UI service)
dotnet watch
