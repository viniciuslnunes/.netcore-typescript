import { SwitchTermPair } from '../../../Settings/entities/SwitchTermPair'

import IFile from '../../interfaces/IFile'

import { replaceTerms } from './replaceTerms'
import { saveFixedFiles } from './saveFixedFiles'
import { fillEditableContent } from '../fillEditableContent'
import { handleErrors } from '../../../../utils/handleErrors'
import { fixOwnerInDefinition } from './fixOwnerInDefinition'
import { checkOwnerIsMissingAtDefinition } from '../CheckFiles/checkOwnerIsMissingAtDefinition'

/**
 * This function groups the correction functions and corrects errors in the files
 *
 * @param files The files you want to fix
 * @param defaultOwner The default owner to use if it is not in package definitions
 * @param packagePrefix The prefix used for every Package
 * @param switchTermPairs Collection with terms substitution mapping
 */
export async function fixFiles (
  files: IFile[],
  defaultOwner: string,
  packagePrefix: string,
  switchTermPairs: SwitchTermPair[]
): Promise<void> {
  await handleErrors(
    fillEditableContent,
    { files },
    {
      toastMessage: 'Erro ao ler os arquivos.'
    }
  )

  handleErrors(
    replaceTerms,
    { files, switchTermPairs },
    {
      toastMessage: 'Erro ao substituir termos.'
    }
  )

  handleErrors(
    () => {
      checkOwnerIsMissingAtDefinition({ files, packagePrefix }) &&
        fixOwnerInDefinition({ files, defaultOwner, packagePrefix })
    },
    undefined,
    {
      toastMessage: 'Erro ao corrigir owner nas definições.'
    }
  )

  await handleErrors(saveFixedFiles, { files })
}
