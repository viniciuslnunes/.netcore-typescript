import IFile from '../../interfaces/IFile'

import { checkTermPairs } from './checkTermPairs'
import { fillIsOkAndReturnIt } from './fillIsOkAndReturnIt'
import { fillEditableContent } from '../fillEditableContent'
import { handleErrors } from '../../../../utils/handleErrors'
import { showValidationResultsInConsole } from './showValidationResultsInConsole'
import { checkOwnerIsMissingAtDefinition } from './checkOwnerIsMissingAtDefinition'
import { checkPackagesInHeaderBodyAreTheSame } from './checkPackagesInHeaderBodyAreTheSame'
import { checkOwnerAndPackageNameDefinitionsAreQuoted } from './checkOwnerAndPackageNameDefinitionsAreQuoted'

/**
 * This function groups the file validations
 *
 * @param filesToCheck The files you want to fix
 * @param termsToCheck Collection with terms to check if files contain
 * @param packagePrefix The prefix used for every Package
 *
 * @returns The result of check files
 */
async function checkFiles (
  filesToCheck: IFile[],
  termsToCheck: string[],
  packagePrefix: string
): Promise<IFile[]> {
  const files: IFile[] = filesToCheck.map((file) => {
    file.errors = []

    return file
  })

  await handleErrors(
    fillEditableContent,
    { files },
    { toastMessage: 'Erro ao ler arquivos.' }
  )

  handleErrors(
    checkTermPairs,
    { files, termsToCheck },
    {
      toastMessage: 'Erro ao checar termos de troca.'
    }
  )

  handleErrors(
    checkOwnerIsMissingAtDefinition,
    { files, packagePrefix },
    {
      toastMessage: 'Erro ao checar Owner ausente.'
    }
  )

  handleErrors(
    checkPackagesInHeaderBodyAreTheSame,
    { files },
    { toastMessage: 'Erro ao checar procedures definidas.' }
  )

  handleErrors(
    checkOwnerAndPackageNameDefinitionsAreQuoted,
    { files, packagePrefix },
    { toastMessage: 'Erro ao checar aspas no owner e package.' }
  )

  handleErrors(
    showValidationResultsInConsole,
    { files },
    { toastMessage: 'Erro ao logar erros no console.' }
  )

  return fillIsOkAndReturnIt({ files })
}

export default checkFiles
