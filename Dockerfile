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

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as runtime
WORKDIR /app
COPY --from=build /app/Build ./
COPY Source/Core/spa.html ./
COPY Source/Core/.dolittle ./.dolittle
COPY Source/bounded-context.json ./
ENTRYPOINT ["dotnet", "/app/Core.dll"]