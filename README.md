# BusCache
A Service bus Open Source write in .Net Core C#

# Using
BusCache is a Windows service that responds on port 7289.

# Commands
1. Register
  ```
     rg <Service Name>
  ```
2. Send Message
```
   sm <Destination Service Name> "Message"
```
3. List of Conected Services
```
   ls
```

# Teste using nc client
1. Install and start service BusCache

2. In a linux console write:
```
    $ nc 127.0.0.1 7289
    rg Service1
```
the response for this command 
```
    Nome trocado com sucesso
```

3. In another linux console:
```
    $ nc 127.0.0.1 7289
    rg Service2
```
the response for this command 
```
    Nome trocado com sucesso
```
4. List of conected services
```
    ls
```
the response for this command 
```
    Service1
    Service2
```
5. Send message from Service2 to Service1
```
    sm Service1 "Test message"
```
Response in Service1 window 
```
    Test message
```

