pipeline{
    agent any
    stages {
        stage('Build'){
            steps{
                sh 'dotnet restore --force'
                sh 'dotnet build'
            }
        }
        stage ('Test'){
            environment {
                OpenWeatherMapApiToken = credentials('OpenWeatherMapRapidAPI-token')
            }
            steps {
                sh 'dotnet test'
            }
        }
        stage ('Deploy'){
            steps {
                sh 'echo Pending'
            }
        }
    }
}