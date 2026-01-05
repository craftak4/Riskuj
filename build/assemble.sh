#!/bin/bash
cd "$(dirname "$(realpath $0)")/../client/desktop/" && (
dotnet publish $PUBLISH_DESKTOP -r linux-x64 -c Release;
dotnet publish $PUBLISH_DESKTOP -r win-x64 -c Release
)
