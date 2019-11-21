FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build
WORKDIR /app/Source

COPY Source/*.sln ./
COPY Source/*/*.csproj ./
RUN for f in *.csproj; do mkdir ${f%%.*}; mv $f ${f%%.*}/$f; done
RUN dotnet restore

# TODO: Remove when 4.0.1 is published
RUN ln -s /usr/share/dotnet /usr/local/share/dotnet

COPY Source/ .
WORKDIR /app/Source/Core
RUN dotnet publish -c Release -o /app/Build

FROM node:12.4 as web
WORKDIR /web/Source
COPY Source/Web/package.json .
RUN yarn

COPY Source/Web .
RUN yarn webpack

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as runtime
WORKDIR /app
COPY --from=build /app/Build ./
COPY --from=web /web/Source/wwwroot ./wwwroot/default
COPY Source/Core/.dolittle ./.dolittle
COPY Source/bounded-context.json ./
ENTRYPOINT ["dotnet", "/app/Core.dll"]