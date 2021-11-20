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