# Platform Service 
[MainSource: .NET Microservices - Full Course](https://www.youtube.com/watch?v=DgVjEo3OGBI&t=2595)

A .NET service development walkthought with Les Jackson. This is a template project for incoming projects. Its alwasy good to learn what are some of the best practices when it comes to developing core services within the .NET ecosystem. 

|![Platform Service Architecture - by Les Jackson](./Images/Platform%20Service%20Architecture%20-%20By%20Les%20Jackson.png "Platform Service Architecture Diagram - By Les Jackson") |

|:--:|

|<b>Platform Service Architecture Diagram - By Les Jackson</b>|


## Local Run 

```bash
  dotnet restore 
  dotnet build 
  dotnet run 
```

## Building Image 

```bash
  docker build -t luisenalvar/platformservice .
```

## Running a Container based on an Image 
8080 is the external port 

```bash
  docker run -p 8080:80 -d luisenalvar/platformservice
```

## Uploading the Image to the Docker Hub

```bash 
  docker push luisenalvar/platformservice 
```