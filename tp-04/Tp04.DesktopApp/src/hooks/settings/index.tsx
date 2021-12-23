import { toast } from 'react-toastify'
import React, {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState
} from 'react'

import ISettings from '../../services/SettingsStorageService/ISettings'

import { SettingsStorageService } from '../../services/SettingsStorageService'
import { SwitchTermPair } from '../../pages/Settings/entities/SwitchTermPair'

interface ISettingContext extends ISettings {
  refreshSettings: VoidFunction;
  saveSettings: VoidFunction;
  updateTermPairs: (termPairs: SwitchTermPair[]) => void;
  updateDefaultOwner: (owner: string) => void;
  updatePackagePrefix: (prefix: string) => void;
}

const settingsStorageService = new SettingsStorageService()

const SettingsContext = createContext<ISettingContext>({} as ISettingContext)

/**
 * This is the AppSettings context, to add a new prop, follow this steps:
 *
 * 1. Add the property at `ISettings`
 * 1. Create a new state variable here, at `SettingsProvider`
 * 1. Create a function to update this information as state
 * 1. Serve this function adding it to `ISettingContext` and to `value` property to provider
 * 1. Update the function `refreshSettings` to include your property to refresh
 * 1. Don't forget to also create a initialValue for your property at `SettingsStorageService` constructor
 */
const SettingsProvider: React.FC = ({ children }) => {
  const [settings, setSettings] = useState<ISettings>({} as ISettings)
  const [termPairs, setTermPairs] = useState<SwitchTermPair[]>([])
  const [defaultOwner, setDefaultOwner] = useState<string>('')
  const [packagePrefix, setPackagePrefix] = useState<string>('')

  const updateTermPairs = useCallback(
    (newTermPairs: SwitchTermPair[]) => {
      setSettings({
        ...settings,
        switchTermPairs: newTermPairs
      })

      setTermPairs(newTermPairs)
    },
    [settings]
  )

  const updateDefaultOwner = useCallback(
    (owner) => {
      setSettings({
        ...settings,
        defaultOwner: owner
      })

      setDefaultOwner(owner)
    },
    [settings]
  )

  const updatePackagePrefix = useCallback(
    (prefix) => {
      setSettings({
        ...settings,
        packagePrefix: prefix
      })

      setPackagePrefix(prefix)
    },
    [settings]
  )

  const refreshSettings = useCallback(() => {
    const loadedSettings = settingsStorageService.loadSettings()

    setTermPairs(loadedSettings.switchTermPairs)
    setDefaultOwner(loadedSettings.defaultOwner)
    setPackagePrefix(loadedSettings.packagePrefix)
    setSettings(loadedSettings)
  }, [])

  const saveSettings = useCallback(() => {
    try {
      settingsStorageService.saveSettings(settings)
      toast.success('Alterações salvas com sucesso.')
    } catch (error) {
      toast.error('Falha ao salvar alterações.')
      console.error('Erro ao salvar: ', error.message)
    }
  }, [settings])

  useEffect(refreshSettings, [])

  return (
    <SettingsContext.Provider
      value={{
        defaultOwner,
        packagePrefix,
        switchTermPairs: termPairs,
        settingsVersion: settings.settingsVersion,

        saveSettings,
        updateTermPairs,
        refreshSettings,
        updateDefaultOwner,
        updatePackagePrefix
      }}
    >
      {children}
    </SettingsContext.Provider>
  )
}

const useSettings = (): ISettingContext => useContext(SettingsContext)

export { SettingsProvider, useSettings }
