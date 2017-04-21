using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Trainer_Trainee.SIP
{
   public class Config
    {
        Config::Config()
{
  fileName = "config.json";

  load();
    }

    ///
    /// \brief Config::load
    ///
    void Config::load()
    {

        load(&sipServer, &sipPort, &sipDomain, &port, &sipAccount, &logLevel, &sipRetry, &sipTimeOut, &ecaServer, &ecaPort);
    }

    void Config::load(QString* _sipServer, int* _sipPort, QString* _sipDomain, int* _port, QString* _sipAccount, int* _logLevel, int* _sipRetry, int* _sipTimeOut, QString* _ecaServer, int* _ecaPort)
    {

        // Check if file exists otherwise use defaults
        QFile file(fileName);

        if (!file.exists())
        {

            qDebug() << "No config file found, loading defaults...";

            loadDefaults(_sipServer, _sipPort, _sipDomain, _port, _sipAccount, _logLevel, _sipRetry, _sipTimeOut, _ecaServer, _ecaPort);

            return;
        }

        // Read file contents and load into config
        QJsonDocument json;

        if (file.open(QFile::ReadOnly))
        {

            QJsonParseError error;

            json = QJsonDocument().fromJson(file.readAll(), &error);

            // Check if JSON was correctly parsed
            if (!error.error == QJsonParseError::NoError)
            {

                qDebug() << "JSON Parse error" << error.errorString();
                qDebug() << "Loading defaults instead...";

                // Load default config
                loadDefaults(_sipServer, _sipPort, _sipDomain, _port, _sipAccount, _logLevel, _sipRetry, _sipTimeOut, _ecaServer, _ecaPort);

                return;
            }

            // Read JSON values
            QJsonObject object  = json.object();
            *_sipServer = (QString)object.value("sipServer").toString();
            *_sipPort = object.value("sipPort").toInt();
            *_sipDomain = (QString)object.value("sipDomain").toString();
            *_port = object.value("port").toInt();
            *_sipAccount = (QString)object.value("sipAccount").toString();
            *_logLevel = object.value("logLevel").toInt();
            *_sipRetry = object.value("sipRetry").toInt();
            *_sipTimeOut = object.value("sipTimeOut").toInt();
            *_ecaServer = (QString)object.value("ecaServer").toString();
            *_ecaPort = object.value("ecaPort").toInt();

            maxPlatformDistance = object.value("maxPlatformDistance").toInt();
            maxPlatformVolume = object.value("maxPlatformVolume").toInt();
            minPlatformVolume = object.value("minPlatformVolume").toInt();

            maxVoiceVolume = object.value("maxVoiceVolume").toInt();
            minVoiceVolume = object.value("minVoiceVolume").toInt();
            voiceVolumeSweep = object.value("voiceVolumeSweep").toInt();

            systemUserName = object.value("systemUserName").toString();
        }
    }

    ///
    /// \brief Config::loadDefaults
    ///
    void Config::loadDefaults(QString* _sipServer, int* _sipPort,
                              QString* _sipDomain, int* _port,
                              QString* _sipAccount, int* _logLevel,
                              int* _sipRetry, int* _sipTimeOut,
                              QString* _ecaServer, int* _ecaPort)
    {

        *_sipServer = "10.0.1.101";
        *_sipPort = 5060;
        *_sipDomain = "uwt";
        *_port = 5060;
        *_sipAccount = "UWT";

        *_logLevel = 2;

        *_sipRetry = 30;
        *_sipTimeOut = 60;

        *_ecaServer = "127.0.0.1";
        *_ecaPort = 2868;

        maxPlatformDistance = 4000;
        maxPlatformVolume = 70;
        minPlatformVolume = 1;

        maxVoiceVolume = 100;
        minVoiceVolume = 10;
        voiceVolumeSweep = 60;

        systemUserName = "udooer";

        // Save the new config
        save();
    }

    //!
    //! \brief Config::save
    //!
    void Config::save()
    {

        // Create JSON object from current configuration
        QJsonObject object;

        object.insert("sipServer", sipServer);
        object.insert("sipPort", sipPort);
        object.insert("sipDomain", sipDomain);
        object.insert("port", port);
        object.insert("sipAccount", sipAccount);
        object.insert("logLevel", logLevel);

        object.insert("sipRetry", sipRetry);
        object.insert("sipTimeOut", sipTimeOut);

        object.insert("ecaServer", ecaServer);
        object.insert("ecaPort", ecaPort);

        object.insert("maxPlatformDistance", maxPlatformDistance);
        object.insert("maxPlatformVolume", maxPlatformVolume);
        object.insert("minPlatformVolume", minPlatformVolume);

        object.insert("maxVoiceVolume", maxVoiceVolume);
        object.insert("minVoiceVolume", minVoiceVolume);
        object.insert("voiceVolumeSweep", voiceVolumeSweep);

        object.insert("systemUserName", systemUserName);

        // Create a JSON document from the newly created JSON object
        QJsonDocument document(object);

        // Save the JSON document to file
        QFile file(fileName);
        file.open(QFile::WriteOnly);
        file.write(document.toJson(QJsonDocument::Indented));

        file.close();
    }

    //-------------------------------------------------
    //
    // Getters & Setters
    //
    //-------------------------------------------------

    int Config::getLogLevel() const                 { return logLevel; }
int Config::getSipRetry() const                 { return sipRetry; }
void Config::setSipRetry(int value) { sipRetry = value; }
int Config::getSipTimeOut() const               { return sipTimeOut; }
void Config::setSipTimeOut(int value) { sipTimeOut = value; }
void Config::setSipServer(const QString &value) { sipServer = value; }
void Config::setSipPort(int value) { sipPort = value; }
void Config::setSipDomain(const QString &value) { sipDomain = value; }
void Config::setPort(int value) { port = value; }
QString Config::getSipAccount() const               { return sipAccount; }
void Config::setSipAccount(const QString &value) { sipAccount = value; }
void Config::setLogLevel(int value) { logLevel = value; }
int Config::getEcaPort() const                  { return ecaPort; }
void Config::setEcaPort(int value) { ecaPort = value; }
QString Config::getEcaServer() const                { return ecaServer; }
void Config::setEcaServer(const QString &value) { ecaServer = value; }
QString Config::getSipServer() const                { return sipServer; }
int Config::getSipPort() const                  { return sipPort; }
QString Config::getSipDomain() const                { return sipDomain; }
int Config::getPort() const                     { return port; }
int Config::getMaxPlatformDistance() const      { return maxPlatformDistance; }
void Config::setMaxPlatformDistance(int value) { maxPlatformDistance = value; }
int Config::getMaxPlatformVolume() const        { return maxPlatformVolume; }
void Config::setMaxPlatformVolume(int value) { maxPlatformVolume = value; }
int Config::getMinPlatformVolume() const        { return minPlatformVolume; }
void Config::setMinPlatformVolume(int value) { minPlatformVolume = value; }
int Config::getMaxVoiceVolume() const           { return maxVoiceVolume; }
void Config::setMaxVoiceVolume(int value) { maxVoiceVolume = value; }
int Config::getMinVoiceVolume() const           { return minVoiceVolume; }
void Config::setMinVoiceVolume(int value) { minVoiceVolume = value; }
int Config::getVoiceVolumeSweep() const         { return voiceVolumeSweep;}
void Config::setVoiceVolumeSweep(int value) { voiceVolumeSweep = value; }
QString Config::getSystemUserName() const           { return systemUserName; }
void Config::setSystemUserName(const QString &value) { systemUserName = value; }



    }
}
