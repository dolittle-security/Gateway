#!/bin/bash
VERSION=$(git tag --sort=-version:refname | head -1)
docker build -t "dolittle/security-gateway:$VERSION" .