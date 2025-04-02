# L3WebAPI

## Migration dotnet
```
~/.dotnet/dotnet --list-sdks
~/.dotnet/dotnet tool install --global dotnet-ef 
export PATH="$PATH:/Users/tom/.dotnet"
export DOTNET_ROOT="/Users/tom/.dotnet"
dotnet-ef -v
```
### Migration :
Dans le DataAccess :
```
dotnet-ef migrations add Initial -s ../L3WebAPI.WebAPI
```
