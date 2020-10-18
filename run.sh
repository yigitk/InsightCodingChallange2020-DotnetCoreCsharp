apt-get update; \
apt-get install -y apt-transport-https && \
apt-get update && \
apt-get install -y dotnet-sdk-3.1
dotnet test ./src/Task.Tests
dotnet ./src/Task.Main.dll

