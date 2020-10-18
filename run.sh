apt-get update -qq ; \
apt-get install -y -qq apt-transport-https && \
apt-get update -qq && \
apt-get install -y -qq dotnet-sdk-3.1 && \
dotnet ./src/Task.Main.dll

