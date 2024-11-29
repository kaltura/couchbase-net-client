@Library('ott-lib-jenkins@nuget-small-fix')_
node('Linux') {
    OUTPUT=""
    PROJECT_NAME = "Couchbase"
    DOTNET_SDK_IMAGE = "mcr.microsoft.com/dotnet/core/sdk:3.1-alpine"
    DOTNET_SDK_IMAGE_ENV = ['DOTNET_CLI_HOME=/tmp/DOTNET_CLI_HOME']

    quietPeriod(50)
    skipDefaultCheckout()
    properties([
        disableConcurrentBuilds(),
        parameters([
            string(name: 'nuget_server', defaultValue: 'https://nuget.rnd.ott.kaltura.com/v3/index.json', description: 'Nuget Server URL'),
            booleanParam(name: 'mark_alpha', defaultValue: 'true', description: 'mark nuget as alpha, uncheck when releasing OFFICIAL nuget')
        ])
    ])
    try {

        stage('Checkout') {
            deleteDir()
            checkout scm: [
                    $class: 'GitSCM',
                    branches: scm.branches,
                    submoduleCfg: [],
                    userRemoteConfigs: scm.userRemoteConfigs
            ]
        }

        OUTPUT = ""
        stage('Build And Publish Nuget') {
            specificVersion = sh(returnStdout: true, script: "git tag --sort version:refname | grep ^2 | tail -1").trim()
            dir ("Src") {
                OUTPUT = dotnetBuildAndPublishNuget(PROJECT_NAME,params.mark_alpha,params.nuget_server,specificVersion)
            }
        }
    } catch (err) {
            echo err.getMessage()
            ERR_MSG = err.getMessage()
            OUTPUT="Error:$ERR_MSG"
            currentBuild.result = 'FAILED'
   }
   kbot.notify(["OTT_DEV_BE_GO"],"$OUTPUT")
}

