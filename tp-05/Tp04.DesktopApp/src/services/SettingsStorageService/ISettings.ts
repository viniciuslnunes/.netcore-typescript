import { SwitchTermPair } from '../../pages/Settings/entities/SwitchTermPair'

interface ISettings {
  settingsVersion: number;
  defaultOwner: string;
  packagePrefix: string;
  switchTermPairs: SwitchTermPair[];
}

export default ISettings
