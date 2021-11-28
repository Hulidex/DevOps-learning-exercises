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
                sh 'dotnet build' // 3. Build solution (All projects)
            }
        }
        stage ('Test'){
            environment {
                // Save API token in a environment variable that will die
                // as soon as this stage ends
                OpenWeatherMapApiToken = credentials('OpenWeatherMapRapidAPI-token')
            }
            steps {
                sh 'dotnet test' // 4. Run tests
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