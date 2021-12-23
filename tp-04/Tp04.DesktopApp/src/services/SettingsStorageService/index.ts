import fs from 'fs'
import path from 'path'

import storageConfig from '../../config/storage'

import { SwitchTermPair } from '../../pages/Settings/entities/SwitchTermPair'

import ISettings from './ISettings'

class SettingsStorageService {
  private readonly _initialSettings: ISettings;

  constructor () {
    this._initialSettings = {
      defaultOwner: '',
      packagePrefix: '',
      switchTermPairs: [new SwitchTermPair()],
      settingsVersion: storageConfig.settings.version
    }

    this._checkSettingsFileExists(
      storageConfig.storagePath,
      storageConfig.settings.fileName
    )
  }

  private _checkSettingsFileExists (filePath: string, fileName: string): string {
    const fullPath = path.join(filePath, fileName)

    !fs.existsSync(filePath) && fs.mkdirSync(filePath)

    !fs.existsSync(fullPath) && this._createSettingsFile(fullPath)

    return fullPath
  }

  private _createSettingsFile (fullPath: string) {
    try {
      fs.writeFileSync(fullPath, JSON.stringify(this._initialSettings))

      console.info(`Settings file create successfully at ${fullPath}`)
    } catch (err) {
      console.error(err)
    }
  }

  public loadSettings (): ISettings {
    const fullPath = this._checkSettingsFileExists(
      storageConfig.storagePath,
      storageConfig.settings.fileName
    )

    const rawSettings = fs.readFileSync(fullPath).toString()

    const settings = JSON.parse(rawSettings) as ISettings

    return settings
  }

  public saveSettings (newSettings: ISettings): ISettings {
    const fullPath = this._checkSettingsFileExists(
      storageConfig.storagePath,
      storageConfig.settings.fileName
    )

    const rawSettings = fs.readFileSync(fullPath)

    const oldSettings = JSON.parse(rawSettings.toString())

    const settings: ISettings = {
      ...oldSettings,
      ...newSettings
    }

    fs.writeFileSync(fullPath, JSON.stringify(settings))

    return settings
  }
}

export { SettingsStorageService }
