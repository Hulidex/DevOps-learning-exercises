# Jenkins multibranch pipeline

## Formulation

Create a multibranch pipeline in Jenkins to execute a job that performs the following tasks:

- Pull code from a git repository
- Define environment variables within the Pipeline. To parametrize the pipeline. i.e: Configure deploy options through these variables.
- Build the code
- Run Unit Tests
- Package the code into an executable file
for external execution.

## Requirements

- Configured and ran Jenkins instance.
- Within that instance, you must log in with a user with enough permissions. (At least for creating and managing projects, multibranch pipelines and credentials).
- Some knowledge of building, testing, running and deploying .NET Core applications. (The basic commands were covered in the master branch)

## Suggestions

If you don't have a Jenkins instance configured and running. I recommend you to install VirtualBox on your personal computer or any other general-purpose full virtualizer for x86 and x64 hardware, you're familiar with. Create a virtual machine that holds a Linux distribution such as Debian, which, in my opinion, is very stable. This way, you'll have a virtual environment where you can install the needed software for these exercises without worrying too much about making mistakes. VirtualBox allows you to create snapshots at any point (before installing Jenkins or any other software). With snapshots, you can restore, remove or clone your environment. If anything goes wrong, you can restore the environment to the point the snapshot was taken and start from scratch at that point, using just a few clicks and in no time.

Plus, your host operating system won't get polluted with these packages, dependencies, services, and software that you won't use anymore. In conclusion, with virtualization, you can create an isolated environment for your learning, that you can manipulate at your will quickly and that won't conflict with your host operating system. Additionally, it will save you time and headaches.

Summarizing:

1. [Install VirtualBox](https://www.virtualbox.org/wiki/Downloads).
2. [Create an x64 virtual machine based on Debian 11](https://kifarunix.com/install-debian-11-on-virtualbox/)
3. Once you have installed Debian, (create a snapshot)[https://www.techrepublic.com/article/how-to-use-snapshots-in-virtualbox/] at that point.
4. [Install and configure Jenkins](https://www.howtoforge.com/how-to-install-jenkins-on-debian-11/).
5. Optionally, [Create a host-only network](https://condor.depaul.edu/glancast/443class/docs/vbox_host-only_setup.html) to expose the Jenkins service to your host machine. This way, you can access your Jenkins instance using your host machine browser.
6. If anything goes wrong, you can restore the snapshot and start all over again.
7. You can clone this environment for other tutorials.

## Solution

> This is exercise is resolved in the branch **exercise/CICD/01-Jenkins-Multi-branch-pipeline**


There is no unique solution to this problem, I decided to use [my console application](https://github.com/Hulidex/dotnet-console-app-weather-map-api/tree/exercise/CICD%2F01-Jenkins-Multi-branch-pipeline) as the base software for the multibranch pipeline. But you can use other software that is familiar to you. Just, keep in mind that you have to use software that at least has a framework that allows you to build, run unit tests and deploy it, using commands.

### Characteristics

- It will build and run a .NET Core 6.0 console app.
- The source files will be hosted in GitHub. Therefore, the captures and configuration steps are based on that version control system (VCS) hosting Platform. Nevertheless, for any other platform such as GitLab or Bitbucket, the steps should be very similar.
- The kind of project configured into Jenkins will be a multibranch pipeline.
- I'm running the pipeline jobs directly on the Jenkins' master node to keep this task simpler (Also no containerization is performed). My Jenkins' master node is based on Debian 11 OS (Using the suggested virtual environment, explained above). Hence, the links provided in this guide are valid only for Linux based systems. 

### Install the application's software dependencies within execution nodes

First, before configuring anything in Jenkins, we have to make sure that the node that Jenkins uses for running the pipeline is compatible with the application we want to automate. In this first approach, and to make things simpler, I will use directly the Jenkins' master node as the execution node (which is the default Jenkins configuration settings when you install it). However, this configuration [is discouraged for production environments](https://devops.stackexchange.com/questions/2105/is-it-ok-to-have-a-jenkins-server-without-slave-node). You should configure slave nodes to run the Jenkins'jobs and limit the access to the master node.  

Finally, to make our node compatible with the application, we should install all the dependencies that the application needs. In my case, I have to install [.NET Core SDK](https://docs.microsoft.com/en-us/dotnet/core/install/linux). You can follow the Microsoft official guide which is quite straightforward.

### Create Jenkins credentials for pulling code from the hosted repository.

In my case, I'm using a private repo within GitHub. Therefore, I need to generate RSA private/public key pairs to be able to authenticate. I assume that you have this knowledge. Thus, I'll skip the technical steps. However, if you don't know what I meant, you can check the following links for Debian based systems (Nevertheless, It should work for the majority of Linux distributions with the open-ssh package installed):

- [Generate SSH Keys](https://phoenixnap.com/kb/generate-ssh-key-debian-10) Only steps 1 to 2.
- [Explanation of RSA Encryption](https://komodoplatform.com/en/academy/rsa-encryption/). (Optionally and to know why we're generating these keys)
- [Add the generated RSA public key](https://docs.github.com/en/authentication/connecting-to-github-with-ssh/adding-a-new-ssh-key-to-your-github-account) into your GitHub account.
- Add to Jenkins the generated RSA private key:

**Security tip**: instead of using the command cat to display the private key. Is safer if you directly copy the key to the system clipboard:

![Copy private key to clipboard](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/add-credentials03.png)

To add the private key to Jenkins you have to click in the sections: ```Dashboard > Manage Jenkins > Manage credentials```:

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/add-credentials01.png)

Click in ```Jenkins```:

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/add-credentials02-01.png)

Click in ```Global credentials (unrestricted)```.

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/add-credentials02-02.png)

Finally click in ```Add credentials```, select the **kind**: ```SSH Username with private key```, fill the empty gaps and press ```OK```.

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/add-credentials02.png)

1. **Scope**: Global
2. **ID**: Internal unique ID by which these credentials are identified from jobs. You can type any ID you want. I used a name that is descriptive to me.
3. **Description**: Optionally type a description for better identifying these credentials.
4. **Username**: Type the username for this private key. As far as I am concerned, if you are using GitHub like me, this field is not used, however, I've still written my GitHub username.
5. **Key**: Paste the copied **private** key here. (Mark the option 'Enter directly' and click button add)
6. **Passphrase** if your private key has a passphrase, you should enter it at this point.

### Create multibranch pipeline in Jenkins.

First click in ```Dashboard > New Item```:

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/configure-multibranchpipe06.png)

Select the option ```Multibranch pipeline```:

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/configure-multibranchpipe01.png)

When configuring the pipeline we have to take into account the following sections:


![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/configure-multibranchpipe02.png)

1. **Display name**: The multibranch project's name.
2. **Description**: Describe the project.
3. **Project Repository**: Insert the URL to access the project source code, in my case I used a private repository hosted in GitHub.
4. **Credentials**: Select the credentials configured in the previous section.

Here there is a screenshot of the repository URL from GitHub:

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/configure-multibranchpipe03.png)

Now we should fill the build configuration section. You can either create the pipeline within this section or create a Jenkins file in the source code and indicate where to fetch it. This last approach is the one I consider the best:

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/configure-multibranchpipe04.png)

1. **Mode**: As I said a Jenkins file that will be hosted in the source code
2. **Script path**: The path within the repository where the Jenkins file is. For keeping things simpler I created the Jenkins file in the root of the repository. Hence, I only have to type its name. However, to keep things organized and separate the application source code, from the CI/CD configuration files is a good practice to create a dedicated folder.

Finally, the pipeline is created:

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/configure-multibranchpipe05.png)

### Create Jenkins file.

Create a Jenkins file with three stages: **Build, Test and Deploy**. If you're using my [application.](https://github.com/Hulidex/dotnet-console-app-weather-map-api/tree/exercise/CICD%2F01-Jenkins-Multi-branch-pipeline), you need to additionally add extra credentials to Jenkins of **kind** ```secret text```.

For my application I configured the following pipeline:

```
pipeline{
    agent any
    environment {
        // Global variables
        MainProjectName = 'OpenWeatherMap'
        MainProjectDir = "OpenWeatherMap/${MainProjectName}.csproj"
        DotnetRuntime = 'linux-x64'
        DotnetFramework = 'net6.0'
        PublishPath = 'bin/csharp-console-app/'
    }
    stages {
        stage('Build'){
            steps{
                sh 'dotnet clean' // 1. Clean solution
                sh 'dotnet restore --force' // 2. Restore solution's dependencies
                sh 'dotnet build --no-restore' // 3. Build solution (All projects)
            }
        }
        stage ('Test'){
            environment {
                // Save API token in a environment variable that will die
                // as soon as this stage ends
                OpenWeatherMapApiToken = credentials('OpenWeatherMapRapidAPI-token')
            }
            steps {
                sh 'dotnet test --no-build --verbosity normal' // 4. Run tests
            }
        }
        stage ('Deploy'){
            steps {
                // Remove publish path if it is not already there
                sh '[ -d "${PublishPath}" ] && rm -rf ${PublishPath} || echo "${PublishPath} does not exist, skipping its deletion..."'
                // 5. Generate an executable file for the production environment
                sh 'dotnet publish ${MainProjectDir} -r ${DotnetRuntime} -f ${DotnetFramework} -o ${PublishPath} --self-contained true'
            }
        }
    }
    post { // This chunk is executed at the end of the pipeline
        success { // Save published executable if all the stages are succesful
            archiveArtifacts artifacts: "${PublishPath}", fingerprint: true
        }
    }
}
```

The pipeline consist of three phases:

- **Build phase**: where all the application dependencies are downloaded and installed within the environment and the source code is compiled and assembled to run it afterwards.
- **Test phase**: After the application is built, the application is tested in this phase. **Important**: To run the application, an API token is needed to authenticate into an external API service. The application expects that token in an environment variable, defined with the name *OpenWeatherMapApiToken*. Thus, you'll need to define that environment variable within the pipeline to make it available to the application. There are multiple ways of performing this step. However, **never put raw passwords under source code**, neither within the application source code, nor in the pipelines files, nor in a configuration file or any place where someone can just read the secret in plain text. You should use a secret provider of your liking or store the secret within Jenkins as we did when configuring the access to GitHub. I used the last approach: Storing the API token inside Jenkins as a credential of **kind** ```secret text``` and finally creating an environment variable, available only during the test scope. The sensitive content of this variable is injected using the helper ```credentials``` which retrieves any available credentials within Jenkins (if permissions are enough) by just indicating the credential's ID.
- **Deploy phase**: If the two previous phases run without errors, that means that the application is suitable to be deployed in a production environment. Hence, within this phase, the application's source code is compiled again. However, this time, the compilation is performed for a specific production environment and only the main project is compiled into the final executable files. Optionally, some companies perform additional steps, depending on their business requirements. For example: Performing some binary obfuscation to protect their products for reverse engineering.

Finally, if all the phases run successfully, the deployed artifact is stored.

Screenshots of executions:

### Run the pipeline

You should now run your pipeline to see that everything is working as expected:

Under ```Dashboard``` you should find your multibranch pipeline project. Click in its name:

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/run-pipeline01.png)

Then click on one of the branches that contain the Jenkins' file (in my case, and at the moment of writing this tutorial, I only have one branch with a Jenkins' file. Concretely, the branch ```exercise/CICD/01-Jenkins-Multi-branch-pipeline```):

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/run-pipeline02.png)

When you select a branch you will see a matrix that contains a history of previous pipeline's runs, with information about each state, elapsed times and colors for each pipeline stage.

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/run-pipeline03.png)

1. If you click on ```Build now```, you can build the pipeline.
2. Here, you can see less detailed information about a performed build.

After clicking in ```Build now``` you will see something like this:

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/run-pipeline04.png)

When the build is performed successfully, you can see that an artifact was produced containing the required binaries for running the application in a certain environment. (This behavior is not Jenkins's default, because of using the archive plugin along with the pipeline I defined in the previous section):

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/run-pipeline05.png)

To download the artifact click on the build number and the in the folder ```Build Artifacts```

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/run-pipeline06.png)

Finally, click on ```all files zip```

![](https://github.com/Hulidex/dotnet-console-app-weather-map-api/blob/exercise/CICD/01-Jenkins-Multi-branch-pipeline/docs/images/run-pipeline07.png)

## Further reading

For further information, check:

[Jenkins official docs](https://www.jenkins.io/doc/)

