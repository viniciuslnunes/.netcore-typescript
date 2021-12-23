import IFile from '../../interfaces/IFile'

import { getDefinedProcedures } from './getDefinedProcedures'

interface ICheckPackagesInHeaderBodyAreTheSameParams {
  /** The files you are checking */
  files: IFile[];
}

/**
 * This function check if the packages have the same
 * procedure definitions for header and body
 */
export function checkPackagesInHeaderBodyAreTheSame ({
  files
}: ICheckPackagesInHeaderBodyAreTheSameParams): void {
  files.forEach((file) => {
    const [headerMath, bodyMatch, ...somethingElse] = Array.from(
      file.editableContent.matchAll(/CREATE/gi)
    ).map((match) => match)

    const packageHasHeaderAndBody = headerMath && bodyMatch
    const packageHasDuplicatedHeaderOrBody = !!somethingElse?.length

    !packageHasHeaderAndBody &&
      file.errors.push('❌ A Package não possui Header e/ou Body')

    packageHasDuplicatedHeaderOrBody &&
      file.errors.push(
        '❌ A Package aparenta ter o Header ou Body duplicados.'
      )

    const headerContent = file.editableContent.slice(
      headerMath.index,
      bodyMatch.index
    )

    const bodyContent = file.editableContent.slice(bodyMatch.index)

    const headerPackages = getDefinedProcedures({ content: headerContent })
    const bodyPackages = getDefinedProcedures({ content: bodyContent })

    const headerAndBodyHaveTheSameProcedures =
      headerPackages.join() === bodyPackages.join()

    !headerAndBodyHaveTheSameProcedures &&
      file.errors.push(
        '❌ Existem procedures definidas no header e não no body, e vice-versa.'
      )
  })
}
