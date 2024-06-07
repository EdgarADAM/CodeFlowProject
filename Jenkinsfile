node {
  checkout scm
  
  def app
  def dockerTag = "adam-saas/incidence-management"
  def nexus = "nexus3.adam.local"
  def scannerHome = tool 'sonar4.8';
  def teams_webhook = 'https://ceridian.webhook.office.com/webhookb2/1cc9cae3-ec2b-4f49-a118-210f330dda3d@289321e0-9db6-4644-b371-956e6056d9eb/IncomingWebhook/3763315664a04cd493a8e3b0ea71020c/a6a88662-e59b-429b-99d0-3e5328685e9a'

  //TODO: Sonarqube 

  stage('Build') {
    try {
      app = docker.build(dockerTag, "-f Dockerfile .")
    } catch (exc) {
        office365ConnectorSend webhookUrl: teams_webhook,
        message:'Exception occurred: ' + exc.toString(),
        color: '#FF0000'
        error "Build failed"
    }
  }
  
  if (env.BRANCH_NAME == 'dev') {
      stage('Veracode Static Scan') {
      try { 
          app.withRun() { c ->
              sh "if [ -d '{WORKSPACE}/veracode' ]; then rm -Rf {WORKSPACE}/veracode; fi"
              sh "mkdir -p ${WORKSPACE}/veracode"
              sh "docker cp ${c.id}:/app/. ${WORKSPACE}/veracode/"
              sh "cd ${WORKSPACE}/veracode && tar -cvf incident-management-api.tar --exclude='*.tar' ."
              sh "mv ${WORKSPACE}/veracode/incident-management-api.tar ${WORKSPACE}/"
              
              echo 'Veracode scanning'
              withCredentials([ usernamePassword (
                  credentialsId: 'Veracode_ID', usernameVariable: 'VERACODE_API_ID', passwordVariable: 'VERACODE_API_KEY') ]) {
                  // fire-and-forget
                  veracode applicationName: "ADAM-SaaS-Incidence-Management-API", criticality: 'VeryHigh', debug: true, fileNamePattern: '', pHost: '', pPassword: '', pUser: '', replacementPattern: '', sandboxName: '', scanExcludesPattern: '', scanIncludesPattern: '', scanName: "${BUILD_TAG}", uploadExcludesPattern: '', uploadIncludesPattern: 'incident-management-api.tar', vid: VERACODE_API_ID, vkey: VERACODE_API_KEY
              }
          }
      } catch (exec) {
          office365ConnectorSend webhookUrl: teams_webhook,
          message:'Exception occurred: ' + exc.toString(),
          color: '#FF0000'
          error "Veracode Static Scan Failed"
      }
    }
  } 
  
  // Push to Nexus
  if (env.BRANCH_NAME == 'dev') {
    stage('Push to nexus DEV') {
      docker.withRegistry("https://${nexus}", 'nexus-credentials') {
        app.push('dev')
      }
      office365ConnectorSend webhookUrl: teams_webhook,
      message:"""
        Push to nexus DEV
      """,
      color: '#00FF00'
    }
  }

  if (env.BRANCH_NAME == 'qa') {
    stage('Push to nexus QA') {
      docker.withRegistry("https://${nexus}", 'nexus-credentials') {
        app.push('qa')
      }
      office365ConnectorSend webhookUrl: teams_webhook,
      message:"""
        Push to nexus QA
      """,
      color: '#00FF00'
    }
  }

  if (env.BRANCH_NAME.count(".") == 2) {
    stage('Push to nexus RELEASE') {
      docker.withRegistry("https://${nexus}", 'nexus-credentials') {
        app.push(env.BRANCH_NAME)
      }
      office365ConnectorSend webhookUrl: teams_webhook,
      message:"""
        Push to nexus RELEASE
      """,
      color: '#00FF00'
    }
  }

  stage('Cleanup') {
      deleteDir()
  }

  stage('Notify Dev') {
    office365ConnectorSend webhookUrl: teams_webhook,
    message:"""
      Pipeline completed Successfully
    """,
    color: '#00FF00'
  }
}