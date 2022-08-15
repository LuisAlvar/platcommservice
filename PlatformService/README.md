# Platform Service 
[MainSource: .NET Microservices - Full Course](https://www.youtube.com/watch?v=DgVjEo3OGBI&t=2595)

A .NET service development walkthought with Les Jackson. This is a template project for upcoming projects. Its alwasy good to learn what are some of the best practices when it comes to developing core services within the .NET ecosystem. 

|![Platform Service Architecture - by Les Jackson](./Images/Platform%20Service%20Architecture%20-%20By%20Les%20Jackson.png "Platform Service Architecture Diagram - By Les Jackson") |

|:--:|

|<b>Platform Service Architecture Diagram - By Les Jackson</b>|

## Project Dependencies 
Rabbitmq dockcer container run 
```bash 
  docker run -d --hostname my-rabbit --name some-rabbit -p 5672:15672 rabbitmq:3-management
```
how you access it http://localhost:5672/#/ <--- username is guest and password is guest by default

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

|![Kubernetes Architecture - by Les Jackson](./Images/KubernetesArchitecture%20-%20By%20Les%20Jackson.PNG "Kubernetes Architecture - By Les Jackson") |

## Kubernetes
The kubernetes architecture for this entire walkthrough is linear where eveything will be housed in one cluster and within one node. Also,
where the ratio between pod and container is 1:1. Its possible for a pod to host multiple containers. However, for this project example its one container per pod. 

platforms-depl.yam is the main file where 