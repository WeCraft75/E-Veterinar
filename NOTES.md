# NOTES
Create container with
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=StrongPassw0rd!" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU13-ubuntu-20.04
```

Then the login credentials should be:
username = sa
password = StrongPassw0rd!

