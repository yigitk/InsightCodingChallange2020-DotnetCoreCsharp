sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1
dotnet test ./src/Task.Tests
dotnet ./src/Task.Main.dll

