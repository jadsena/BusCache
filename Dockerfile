FROM mcr.microsoft.com/dotnet/core/sdk:3.1
COPY BusCache/bin/Release/netcoreapp3.1/publish/ BusCache/

ENTRYPOINT ["dotnet", "BusCache/BusCache.dll", "--console"]