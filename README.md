Welcome to the dotnet-console-app-weather-map-api wiki!

# Introduction

This project is a console application written in C# (using .NET Core 6.0 Framework). I didn't design it to use it in a production environment, just as a code base for playing around with DevOps tools, such as Jenkins, Docker, Kubernetes...

Therefore, I make this project with the aim of learning the basic concepts of DevOps technologies and principles. Thus, if you have the same goal, this repository might help you by challenging you with some practice exercises.

Finally, I would like to make it clear: English is not my native language. If you find anything that could be fixed in any way, please make a Pull Request with the suggestions. Thank you!

# How to run the Project

**IMPORTANT**: The instructions contained in this section assume that you're running a Linux based system.

## Description

The repository consists basically of three projects:

- **OpenWeatherMap**: Directory that contains the main project.
- **OpenWeatherMap.Tests**: Directory that contains a Test project for testing the main project.
- **WebUI**: Directory that contains a Web based project which combines Angular.js with ASP .Net core

### OpenWeatherMap Project

It is the code related to the console application, and It's just an HTTP client wrapper to make HTTP calls to a remote API. Concretely, the API OpenWeatherMap, which expose a REST endpoint that retrieves Weather info.

Explaining the different HTTP calls that the API can handle and how to perform them, is out of the scope of this project. You just have to know that the application, abstracts you from all that techie stuff: **It is capable of making requests using an intermediate API to gather weather information about a specific point in the world and finally, it displays the gathered information in the console**.

### OpenWeatherMap.Tests Project.

The project contains some behavior tests.

### WebUI

At the moment this project contains the Angular + ASP Net core template that can be generated with the command:

```bash
dotnet new angular -n WebUI --no-https -f net6.0 --force
```
I applied some modifications to the original template:
    - I updated the embedded Angular project to a new version
    - I changed some configurations related to the application's port mapping.
    - (PENDING) Create a Web User Interface for the OpenWeatherMap console app and persist some
    information in a database. This will emulate a more complex application

## Running the application

The steps to run the application are the following:

### Clone the repository

You can download or clone this repository. In order to clone, please open a git console in the desired location and clone it with the command:

```bash
git clone https://github.com/Hulidex/dotnet-console-app-weather-map-api.git
```

This will create the directory **dotnet-console-app-weather-map-api** I will refer to this path in your computer as ```$repo```

### Build the repository

Because this project is based on the .NET Core framework, for building the project we have to: Firstly, access into the ```$repo``` path, secondly, download the project dependencies and finally build the project.

Within a terminal located at ```$repo```, run the given commands:

For downloading project dependencies:

```bash
cd $repo
```

```bash
dotnet restore 
```

For building the project

```bash
dotnet build
```

### Run the solution

Once the repository is built, you can run it using a dotnet command or running directly by executing an executable file (it needs you to publish the solution)

Running the solution with just a command:

```bash
dotnet run --project OpenWeatherMap/OpenWeatherMap.csproj
```

Or, Generating the executable file:

```bash
# Publish main project
dotnet publish OpenWeatherMap/OpenWeatherMap.csproj -f net6.0 -o bin/csharp-console-app -r linux-x64 --self-contained tru
e

# change directory to the published folder
cd bin/csharp-console-app

# Run application
./OpenWeatherMap
```

IMPORTANT: In order to run the project correctly, you need to register into the platform RapidAPI and create a new APP (See images below), then you'll need to copy the Application key and store it in an environment variable with the name: **OpenWeatherMapApiToken**.

For example:

1. Register into RapidApi:

![Register](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/master/docs/images/rapidapi01.png)

2. Create a new App:

![Create new App](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/master/docs/images/rapidapi02.png)

3. Consult the token:

![Consult token](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/master/docs/images/rapidapi03.png)

For creating a local environment variable in Bash (which is normally the shell that use by default the majority of Linux distribution), you can run the following commands:

```bash
# Create LOCAL (only accessible in this bash session and its children) environment variable
export OpenWeatherMapApiToken="YOUR TOKEN HERE"

# Run the project using dotnet
dotnet run --project OpenWeatherMap/OpenWeatherMap.csproj

# ------------ OR ---------------

# Run the project from the published solution (see
# steps above)
bin/csharp-console-app/OpenWeatherMap
```

This additional step is required in order to be able to make requests to the OpenWeatherMap API.

Finally, if you don't specify any parameters to the application, then the default parameters will be:

```
Longitude: 100.4927, Latitude 13.7521.
```

Which correspond to the weather in Bangkok, Thailand. You can specify any other locations, as follows:

```bash
bin/csharp-console-app/OpenWeatherMap --Lon 100.4927 --Lat 13.7521
```

Example of a successful execution (Weather in Malaga, Spain):

![Successful execution](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/master/docs/images/success-execution.png)

## How Am I supposed to use this repository

First, I'll recommend you to do the practice exercises in the appearance order, because their difficulty can increase in that way. Hence, if you begin by doing the last exercises, probably you'll need to know concepts that were covered in the previous exercises.

At first, just read the exercise formulation, avoid knowing anything about the solution, Do not peek!. And try to do the exercise by yourself, looking for information online or in books or whatever source you can. If you don't have any clue about what you have to do, and you're stuck, then, you can read the solution partially and try another time or completely, it's up to you!

### Requirements

Here, I list the basic requirements that in my opinion could be handy to know to do the exercises. I will not cover anything related to these topics. However, I'll try to attach links that I find interesting, that can help you to catch up.

- Git knowledge (cloning repositories)
- Medium knowledge about coding. Within DevOps, one key concept is 'Infrastructure as code'. Thus, we will code scripts and many other things frequently. 
- Basic knowledge about using a shell, running scripts and console applications.
- Virtualization knowledge.
- Medium Linux knowledge. We will install services and tools that will require you to know some medium/basic concepts about Linux, for example: how to install/update packages using a package manager, how to enable and start services (i.e: using systemd utility), creating environment variables (We already did within this installation), copy and moving files, obtaining information.
- Some security topics like RSA key pairs, managing secrets in an application, and many others.
- Basic knowledge about SSH, HTTP and other Internet protocols could be beneficial.

### Working space example

Particularly I'm doing this exercise using my personal computer which has installed Arch Linux (a rolling release Linux distribution). Within it, I use VirtualBox for running virtual machines where I can install and configure the required software. In those virtual machines, I'm using Debian 11 because In my opinion is a stable Linux distribution. However, you can use whatever configuration suits you.

# Exercises

At the moment the exercises that are available are:

1. Jenkins multibranch pipeline:
    ```bash
    git switch exercise/CICD/01-Jenkins-Multi-branch-pipeline 
    ```

